using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Overstag.Models;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Overstag.Authorization;
using DinkToPdf.Contracts;
using Overstag.Services;
using DinkToPdf;

namespace Overstag.Controllers.Users
{
    public class FilesController : Controller
    {
        private IConverter _converter;

        public FilesController(IConverter converter)
            => _converter = converter;

        /*
        [OverstagAuthorize(-1)]
        public async Task<IActionResult> InvoicePdf([FromQuery]string token, [FromQuery]bool download, [FromQuery]string name)
        {
            if (token != null && System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Uploads", Uri.UnescapeDataString(token))))
            {
                var res = new FileContentResult(await System.IO.File.ReadAllBytesAsync(Path.Combine(Directory.GetCurrentDirectory(), "Uploads", Uri.UnescapeDataString(token))), "application/pdf");
                if (download) res.FileDownloadName =  (name == null) ? "overstag-factuur.pdf" : Uri.UnescapeDataString(name)+".pdf";
                return res;
            }
            else
            {
                string[] error = { "Bestand niet gevonden", "Voor deze factuur is geen PDF-bestand gegenereerd" };
                return View("~/Views/Error/Custom.cshtml", error);
            }
        }

        [OverstagAuthorize(-1)]
        public IActionResult CheckOrCreatePDF([FromQuery]string token)
        {
            if(token == null)
                return Json(new { status = "error", error = "Geen token meegegeven" });

            token = Uri.UnescapeDataString(token);
            using var context = new OverstagContext();
            var i = context.Invoices.FirstOrDefault(x => x.InvoiceID == token);

            if(i == null)
                return Json(new { status = "error", error = "Factuur niet gevonden" });

            var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", i.InvoiceID);
            if (System.IO.File.Exists(path))
                return Json(new { status = "success", type = "exist" });

            try
            {
                var invoice = Invoices.GetXInvoice(i.Id);

                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = "Overstag factuur #" + invoice.GetInvoiceNumber()
                };

                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = Pdf.GenerateInvoiceHtml(invoice, HttpContext),
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "css", "overstag-materialize.min.css") },
                    HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                    FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Stichting Overstag" }
                };

                var pdf = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings }
                };

                var file = _converter.Convert(pdf);
                System.IO.File.WriteAllBytes(path, file);
                return Json(new { status = "success", type = "generated" });
            }
            catch (Exception e)
            {
                return Json(new { status = "error", error = "Printbare PDF genereren mislukt", debuginfo = e.ToString() });
            }
        }

        /// <summary>
        /// Serve an anonymous file from the database by its token
        /// </summary>
        /// <param name="token">The file's token</param>
        /// <returns>File or JSON</returns>
        [HttpGet]
        [OverstagAuthorize]
        [Route("Files/Serve/{token}")]
        public async Task<IActionResult> Serve(string token)
        {
            await using var context = new OverstagContext();
            var file = await context.Files.FirstOrDefaultAsync(f => f.Token == token);
            if (file == null)
                return Json(new { status = "error", error = "Bestand niet gevonden in database" });

            string name = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", file.Token);

            if (!System.IO.File.Exists(name))
                return Json(new { status = "error", error = "Bestand niet gevonden op server" });

            try
            {
                return new FileContentResult(await System.IO.File.ReadAllBytesAsync(name), file.Mimetype) { FileDownloadName = file.Name };
            }
            catch (Exception e)
            {
                return Json(new { status = "error", error = "Interne fout", debuginfo = e.ToString() });
            }
        }

        /// <summary>
        /// Upload file(s) to the server
        /// </summary>
        /// <param name="files">One or multiple form-encoded files</param>
        /// <returns>JSON</returns>
        [HttpPost("Files/UploadFiles")]
        [OverstagAuthorize]
        [RequestSizeLimit(1000000000)] //1GB MAX
        public async Task<IActionResult> UploadFiles(IList<IFormFile> files)
        {
            List<string> fileIDS = new List<string>();
            if (Request.ContentLength > 1000000000)
                return Json(new { status = "error", error = "Bestand is te groot" });

            if (!files.Any())
                return Json(new { status = "error", error = "Geen bestanden" });

            await using var context = new OverstagContext();
            foreach (var file in files)
            {
                if (file.Length > 0 && file != null)
                {
                    string filehash = Encryption.Random.rHash(file.FileName + file.Name);
                    Console.WriteLine($"{file.Name} - {file.FileName} - {file.Length} - {file.ContentType}");
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", filehash);
                    await using (var stream = new FileStream(path, FileMode.Create))
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

            
                    //string contentType;
                    //new FileExtensionContentTypeProvider().TryGetContentType(FileName, out contentType);
                    //return contentType ?? "application/octet-stream";
                 
        }*/
    }
}
