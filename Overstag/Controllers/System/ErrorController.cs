using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Overstag.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{code}")]
        public IActionResult Default(int code)
        {
            ViewBag.Code = code;
            ViewBag.Message = ReasonPhrases.GetReasonPhrase(code);
            ViewBag.English = ReasonPhrases.GetReasonPhrase(code);
            ViewBag.Description = "Het schip lijkt op de klippen te zijn gelopen";
            this.HttpContext.Response.StatusCode = code;

            if (ViewBag.Message == "" || ViewBag.Message == null)
                ViewBag.Message = "Er is iets fout gegaan";

            if (code == 400)
            {
                ViewBag.Message = "Bad Request";
                ViewBag.Description = "We staan versteld van hoe jij dit doet!";
                ViewBag.showHome = true;
            }
            else
            if (code == 401)
            {
                ViewBag.Message = "Niet ingelogd";
                ViewBag.Description = "De kaptitein was er niet van op de hoogte dat u mee zou gaan";
                ViewBag.showLogin = true;
                ViewBag.showHome = true;
            }
            else
            if (code == 403)
            {
                ViewBag.Message = "Onvoldoende rechten";
                ViewBag.Description = "De kaptitein verbiedt een matroos om hier te komen";
                ViewBag.showLogin = true;
            }
            else
            if (code == 404)
            {
                ViewBag.Message = "Pagina niet gevonden";
                ViewBag.Description = "<i>Oh, the captain was drunk...</i>?<br>Het schip heeft een verkeerde route gevaren";
                ViewBag.showHome = true;
            }
            else
            if (code == 418)
            {
                ViewBag.Message = "Ik ben een theepot!";
                ViewBag.Description = "<i>Thee is best lekker als je ver moet varen</i>";
                ViewBag.showHome = true;
            }
            else
            if (code == 500)
            {
                ViewBag.Message = "Interne serverfout";
                ViewBag.Description = "<i>Er is iets flink fout gegaan...</i><br>Het schip is tegen een ijsberg gevaren en gezonken";
                ViewBag.showHome = true;
            }

            return View();
        }

        public IActionResult Test()
        {
            return Ok();
        }
    }
}
