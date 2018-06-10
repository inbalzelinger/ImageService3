using System;
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
using communication.server;
using ImageService3.ImageService.Server;
using communication;
using ImageService.Infrastructure.Enums;

namespace ImageService3
{
    public partial class ImageService3 : ServiceBase
    {

        private int eventId = 1;
        private ImageServer m_server;          // The Image Server
        private IImageServiceModal m_modal;
        private IImageController m_controller;
        private ILoggingService m_logger;
        private GUIServer m_guiServer;



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
        private static extern bool SetServiceStatus(IntPtr handle, ref
        ServiceStatus serviceStatus);


        ///<summary>
        ///ImageService constructor.
        ///</summary>
        public ImageService3()
        {
            try
            {
                InitializeComponent();
                string eventSourceName = ConfigurationManager.AppSettings.Get("SourceName");
                string loggerName = ConfigurationManager.AppSettings.Get("LogName");
                eventLog1 = new EventLog();
                if (!EventLog.SourceExists("MySource"))
                {
                    EventLog.CreateEventSource(
                        "MySource", "MyNewLog");
                }
                eventLog1.Source = eventSourceName;
                eventLog1.Log = loggerName;
                this.m_logger = new LoggingService();
                m_logger.MessageRecieved += OnMessage;
                m_logger.MessageRecieved += M_logger_MessageRecieved;
                m_logger.Log("end of ImageService3 constructor, thelogger event was added", MessageTypeEnum.INFO);


            }
            catch (Exception e)
            {
                m_logger.Log("exception in ImageService constructor", MessageTypeEnum.FAIL);
            }

        }

        private void M_logger_MessageRecieved(object sender, MessageRecievedEventArgs e)
        {
            string[] arguments = new string[1];
            arguments[0] = e.ToJson();
            CommandRecievedEventArgs c = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, arguments, null);
            GUIServer.Instance.Write(c.ToJson());
        }


        ///<summary>
        ///write masseges to the service logger
        ///</summary>
        private void OnMessage(object sender, MessageRecievedEventArgs e)
        {
            eventLog1.WriteEntry(e.Status + ":" + e.Message);
        }



        ///<summary>
        ///start the service
        ///</summary>
        protected override void OnStart(string[] args)
        {
            m_logger.Log("In OnStart", MessageTypeEnum.INFO);
            // Update the service state to Start Pending.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            // reads from the app config the parameters.
            string OutputFolder =
        ConfigurationManager.AppSettings.Get("OutputDir");
            int ThumbnailSize =
        Int32.Parse(ConfigurationManager.AppSettings.Get("ThumbnailSize"));

            // create the members.
            this.m_modal = new ImageServiceModal(OutputFolder, ThumbnailSize);
            this.m_controller = new ImageController(this.m_modal, this.m_logger);

            // create the server which will start listening.
            this.m_server = new ImageServer(this.m_controller, this.m_logger);

            //create server for the gui.

            GUIServer.Instance.OnMessageRecived += M_server_OnMessageRecived;
            //this.m_guiServer = new GUIServer(this.m_controller);


            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 60000; // 60 seconds
            timer.Elapsed += new
        System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();
            // Update the service state to Running.
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }


        ///<summary>
        ///OnTimer
        ///</summary>
        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.
            // m_logger.Log("In OnTimer", MessageTypeEnum.INFO);
        }


        ///<summary>
        ///while stoping the service, will close the server.
        ///</summary>
        protected override void OnStop()
        {
            m_logger.Log("In on onStop", MessageTypeEnum.INFO);
            this.m_server.OnCloseSevice();
        }


        ///<summary>
        ///for service debuging.
        ///</summary>
        public void OnDebug()
        {
            OnStart(null);
        }

        private void M_server_OnMessageRecived(object sender, string e)
        {
            try
            {
                CommandRecievedEventArgs crea = CommandRecievedEventArgs.FromJson(e);
                bool result;
                if (crea.CommandID == (int)CommandEnum.CloseCommand)
                {
                    m_server.SendCommand(new
        CommandRecievedEventArgs((int)CommandEnum.CloseCommand, null, crea.Args[0]));
                    GUIServer.Instance.Write("Close " + crea.Args[0]);
                    return;
                }
                string res = this.m_controller.ExecuteCommand(crea.CommandID, crea.Args, out result);
                IClientHandler clientHandler = (IClientHandler)sender;
                clientHandler.Write(this, res);
            }
            catch
            {
                Debug.Write("we are on GUIServer InM_server_OnMessageRecived and it isnt good!");
            }
        }
    }
}
