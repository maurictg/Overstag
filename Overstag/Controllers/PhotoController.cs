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
            using (MemoryStream stream = new MemoryStream())
            {
                Bitmap img = new QRCode(new QRCodeGenerator().CreateQrCode(Uri.UnescapeDataString(input), QRCodeGenerator.ECCLevel.Q)).GetGraphic(20);
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                var bytes = stream.ToArray();
                return File(bytes, "image/jpeg");
            }
        }
    }
}
