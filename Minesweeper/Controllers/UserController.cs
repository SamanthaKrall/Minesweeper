using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Minesweeper.Models;
using System.Diagnostics;
using Minesweeper.Services.Utility;
using Minesweeper.Services.Business;

namespace Minesweeper.Controllers
{
    [CustomAction]
    public class UserController : Controller
    {
        private readonly ILogger logger;

        public UserController(ILogger service)
        {
            this.logger = service;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("Login");
        }
        
        [HttpGet]
        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.Session["Username"] = null;
            return View("Login");
        }

        [HttpPost]
        public ActionResult LoginUser(UserModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Login");
                }
                SecurityService ss = new SecurityService();
                bool result = ss.Authenticate(user);
                if (result == true)
                {
                    HttpContext.Session["Username"] = user.Username;
                    return View("Home", user);
                }
                else
                {
                    return View("LoginFailed");
                }
            }catch(Exception e)
            {
                return View("LoginError");
            }
        }

        [HttpPost]
        public ActionResult RegisterUser(RegisterModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Register");
                }
                SecurityService ss = new SecurityService();
                string result = ss.Register(user);
                if (result.Equals("success"))
                {
                    return View("Login");
                }
                else if(result.Equals("fail"))
                {
                    return View("RegisterFailed");
                }
                else
                {
                    return View("DuplicateUser");
                }
            } 
            catch(Exception e)
            {
                Trace.WriteLine(e.Message);
                return View("RegisterError");
            }
        }

        [HttpGet]
        [CustomAuthorization]
        public string Protected()
        {
            return "I am a protected method";
        }
    }
}