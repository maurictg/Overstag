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
        /// <summary>
        /// Creates a QR-code
        /// </summary>
        /// <param name="input">String to create a QR with (URI-encoded)</param>
        /// <returns>Image file</returns>
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
