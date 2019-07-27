using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.IO;
using QRCoder;

namespace Overstag.Controllers
{
    public class PhotoController : Controller
    {
        [HttpGet]
        [Route("/Photo/GetQR/{input}")]
        public IActionResult GetQR(string input)
        {
            input = Uri.UnescapeDataString(input);
            QRCodeGenerator gen = new QRCodeGenerator();
            QRCodeData data = gen.CreateQrCode(input, QRCodeGenerator.ECCLevel.Q);
            QRCode code = new QRCode(data);
            Bitmap img = code.GetGraphic(20);
            byte[] bytes = { };

            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                bytes = stream.ToArray();
            }

            return File(bytes, "image/jpeg");
        }
    }
}
