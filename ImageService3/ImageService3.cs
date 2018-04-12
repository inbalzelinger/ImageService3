﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using ImageService.Logging;
using System.Configuration;
using ImageService.Logging.Modal;
using ImageService.Server;
using ImageService.Controller;
using ImageService.Modal;


namespace ImageService3
{
    public partial class ImageService3 : ServiceBase
    {

        private int eventId = 1;
        private ImageServer m_imageServer;          // The Image Server
        private IImageServiceModal  m_modal;
        private IImageController m_controller;
        private ILoggingService m_logger;

        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public int dwServiceType;
            public ServiceState dwCurrentState;
            public int dwControlsAccepted;
            public int dwWin32ExitCode;
            public int dwServiceSpecificExitCode;
            public int dwCheckPoint;
            public int dwWaitHint;
        };

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

        public ImageService3()
        {
            try
            {
                InitializeComponent();
                string eventSourceName = ConfigurationManager.AppSettings.Get("SourceName");
                string loggerName = ConfigurationManager.AppSettings.Get("LogName");
                eventLog1 = new System.Diagnostics.EventLog();
                if (!System.Diagnostics.EventLog.SourceExists("MySource"))
                {
                    System.Diagnostics.EventLog.CreateEventSource(
                        "MySource", "MyNewLog");
                }
                eventLog1.Source = eventSourceName;
                eventLog1.Log = loggerName;
            } catch(Exception e)
            {
                ;
            }

        }

        private void onMessage(object sender, MessageRecievedEventArgs e)
        {
            eventLog1.WriteEntry(e.Status + ":" + e.Message);
        }


        protected override void OnStart(string[] args)
        {
            this.m_logger = new LoggingService();

            m_logger.Log("In OnStart", MessageTypeEnum.INFO);

            // Update the service state to Start Pending.  
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            // Set up a timer to trigger every minute.  
            m_logger.MessageRecieved += onMessage;
            //this.logging.MessageRecieved += WriteMessage;
            string OutputFolder = ConfigurationManager.AppSettings.Get("OutputDir");
            int    ThumbnailSize = Int32.Parse(ConfigurationManager.AppSettings.Get("ThumbnailSize"));

            this.m_modal = new ImageServiceModal(OutputFolder, ThumbnailSize);

            this.m_controller = new ImageController(this.m_modal);
            this.m_imageServer = new ImageServer(this.m_controller, this.m_logger);


            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 60000; // 60 seconds  
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();
            // Update the service state to Running.  
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }


        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.  
            m_logger.Log("In OnTimer", MessageTypeEnum.INFO);
        }

        protected override void OnStop()
        {
            m_logger.Log("In In onStop", MessageTypeEnum.INFO);
        }

        public void onDebug()
        {
            OnStart(null);
        }
    }
}
