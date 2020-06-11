using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Overstag.Models;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Overstag.Authorization;

namespace Overstag.Controllers.Users
{
    [OverstagAuthorize]
    public class FilesController : Controller
    {
       
        [HttpGet]
        [Route("Files/Serve/{token}")]
        public IActionResult Serve(string token)
        {
            using(var context = new OverstagContext())
            {
                var file = context.Files.FirstOrDefault(f => f.Token == token);
                if (file == null)
                    return Json(new { status = "error", error = "Bestand niet gevonden in database" });

                string name = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", file.Token);

                if (!System.IO.File.Exists(name))
                    return Json(new { status = "error", error = "Bestand niet gevonden op server" });

                try
                {
                    return new FileContentResult(System.IO.File.ReadAllBytes(name), file.Mimetype) { FileDownloadName = file.Name };
                }
                catch (Exception e)
                {
                    return Json(new { status = "error", error = "Interne fout", debuginfo = e.ToString() });
                }
            }
        }

        [HttpPost("Files/UploadFiles")]
        [RequestSizeLimit(1000000000)] //1GB MAX
        public async Task<IActionResult> UploadFiles(IList<IFormFile> files)
        {
            List<string> fileIDS = new List<string>();
            if (Request.ContentLength > 1000000000)
                return Json(new { status = "error", error = "Bestand is te groot" });

            if (files.Count() <= 0)
                return Json(new { status = "error", error = "Geen bestanden" });

            using(var context = new OverstagContext())
            {
                foreach (var file in files)
                {
                    if (file.Length > 0 && file != null)
                    {
                        string filehash = Encryption.Random.rHash(file.FileName + file.Name);
                        Console.WriteLine($"{file.Name} - {file.FileName} - {file.Length} - {file.ContentType}");
                        var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", filehash);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        context.Files.Add(new Models.File()
                        {
                            Mimetype = file.ContentType,
                            Name = file.FileName,
                            Token = filehash
                        });
                        
                        fileIDS.Add(filehash);
                    }
                    else
                        return Json(new { status = "error", error = "Interne fout", debuginfo = "Een van de bestanden is NULL" });
                }

                await context.SaveChangesAsync();

                return Json(new { status = "success", fileIDS });

                /*
                    string contentType;
                    new FileExtensionContentTypeProvider().TryGetContentType(FileName, out contentType);
                    return contentType ?? "application/octet-stream";
                 */
            }
        }
    }
}
