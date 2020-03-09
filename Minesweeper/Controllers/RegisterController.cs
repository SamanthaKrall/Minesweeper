using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Minesweeper.Models;

namespace Minesweeper.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        [HttpGet]
        public ActionResult Index()
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(UserModel user)
        {
            Services.Business.SecurityService ss = new Services.Business.SecurityService();
            Boolean outcome = ss.Insert(user);

            if (outcome)
            {
                return View("RegisterPassed", user);
            }
            else
            {
                return View("RegisterFailed");
            }
        }
    }
}