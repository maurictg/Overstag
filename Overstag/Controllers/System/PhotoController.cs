using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.IO;
using QRCoder;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace Overstag.Controllers
{
    public class PhotoController : Controller
    {
        private readonly IWebHostEnvironment _env;
        public PhotoController(IWebHostEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// Creates a QR-code of a from a user's secret and username
        /// </summary>
        /// <param name="secret">The user's secret</param>
        /// <param name="username">The user's username</param>
        /// <returns>Image file</returns>
        [HttpGet]
        [Route("/Photo/GetQR/{secret}/{username}")]
        public IActionResult GetQR(string secret, string username)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                string segment = $"otpauth://totp/Overstag:{username}?secret={Uri.UnescapeDataString(secret)}&issuer=Overstag";
                Bitmap img = new QRCode(new QRCodeGenerator().CreateQrCode(segment, QRCodeGenerator.ECCLevel.Q)).GetGraphic(20);
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                var bytes = stream.ToArray();
                return File(bytes, "image/jpeg");
            }
        }

        /// <summary>
        /// Return a random wallpaper. Not used right now
        /// </summary>
        /// <returns></returns>
        [HttpGet("/Photo/RandomWallpaper")]
        public IActionResult Wallpaper()
        {
            var files = Directory.GetFiles(Path.Combine(_env.WebRootPath, "img", "wallpapers"), "*.jpg");
            return File(System.IO.File.ReadAllBytes(files[new Random().Next(files.Length)]), "image/jpeg");
        }
        
    }
}
