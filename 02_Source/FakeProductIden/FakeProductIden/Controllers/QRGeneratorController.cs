using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FakeProductIden.Controllers
{
    public class QRGeneratorController : Controller
    {
        // GET: QRGenerator
        public ActionResult Index()
        {
            if (!HomeController.isLoaded)
            {
                HomeController.LoadRecord();
                HomeController.isLoaded = true;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(string QRText)
        {
            if (!HomeController.isLoaded)
            {
                HomeController.LoadRecord();
                HomeController.isLoaded = true;
            }

            string Validate_Url= "https://localhost:44330/QRValidate/Index/";
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(Validate_Url + QRText, QRCodeGenerator.ECCLevel.Q);
            QRCode qRCode = new QRCode(qRCodeData);
            Bitmap qRCodeBitmap = qRCode.GetGraphic(20);
            return View(BitmapToBytes(qRCodeBitmap));
        }

        private static Byte[] BitmapToBytes(Bitmap qRCodeBitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                qRCodeBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}