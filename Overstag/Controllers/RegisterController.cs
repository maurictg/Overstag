using System;
using System.Collections.Generic;
using System.Linq;
using Overstag.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Overstag.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View("Login");
        }

        public IActionResult Login()
        {
            return View("Login");
        }

        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult postRegister(Account account)
        {
            return Json(account);
        }
    }
}