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

        [HttpGet]
        [Route("Files/serve/{token}/{type}")]
        public IActionResult Serve(string token, string type)
        {
            string file = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", Uri.UnescapeDataString(token));
            if (!System.IO.File.Exists(file))
                return Json(new { status = "error", error = "Bestand niet gevonden" });

            try
            {
                return File(System.IO.File.ReadAllBytes(file), Uri.UnescapeDataString(type)); //example: "image%2Fjpeg"
            }
            catch(Exception e)
            {
                return Json(new { status = "error", error = "Interne fout", debuginfo = e.ToString() });
            }
        }

        [HttpPost("Files/uploadFiles")]
        [RequestSizeLimit(1000000000)] //1GB MAX
        public async Task<IActionResult> UploadFiles(IList<IFormFile> files)
        {
            List<string> fileIDS = new List<string>();
            if (Request.ContentLength > 1000000000)
                return Json(new { status = "error", error = "Bestand is te groot" });

            if (files.Count() <= 0)
                return Json(new { status = "error", error = "Geen bestanden" });

            foreach (var file in files)
            {
                if (file.Length > 0 && file != null)
                {
                    string fileID = Encryption.Random.rHash(file.FileName + file.Name);
                    Console.WriteLine($"{file.Name} - {file.FileName} - {file.Length} - {file.ContentType}");
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", fileID);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    fileIDS.Add(fileID);
                }
                else
                    return Json(new { status = "error", error = "Interne fout", debuginfo = "Een van de bestanden is NULL" });
            }
            return Json(new { status = "success", fileIDS });

        }
    }
}
