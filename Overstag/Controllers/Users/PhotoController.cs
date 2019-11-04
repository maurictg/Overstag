using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.IO;
using QRCoder;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

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

        [HttpPost("Files/uploadFile")]
        [RequestSizeLimit(1000000000)] //1GB
        public async Task<IActionResult> UploadFile(IList<IFormFile> files)
        {
            if (Request.ContentLength > 1000000000)
                return Json(new { status = "error", error = "Bestand is te groot" });

            if (files.Count() <= 0)
                return Json(new { status = "error", error = "Geen bestanden" });

            foreach (var file in files)
            {
                if (file.Length > 0 && file != null)
                {
                    Console.WriteLine($"{file.Name} - {file.FileName} - {file.Length} - {file.ContentType}");
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                else
                    return Json(new { status = "error", error = "Interne fout", debuginfo = "Een van de bestanden is NULL" });
            }
            return Json(new { status = "success" });

        }
    }
}
