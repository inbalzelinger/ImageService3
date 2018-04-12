using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageService.Modal;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int thumb = 120;
            bool result;
            string outputDir = "C:\\Users\\inbal\\Desktop\\dstTest";
            IImageServiceModal imageModal = new ImageServiceModal(outputDir, thumb);

            string s = imageModal.AddFile("C:\\Users\\inbal\\Desktop\\test1\\la.jpg", out result);
            Assert.AreEqual(result, true, s);
        }
    }
}
