using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Minesweeper.Models;
using System.Diagnostics;
using Minesweeper.Services.Utility;
using Minesweeper.Services.Business;
using System.Web.Script.Serialization;

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
            logger.Info("Entering UserController.LoginUser()");
            logger.Info("Parameters are: {0}", new JavaScriptSerializer().Serialize(user));

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
                    logger.Info("Exiting UserController.LoginUser() with login passed");
                    HttpContext.Session["Username"] = user.Username;
                    return View("Home", user);
                }
                else
                {
                    logger.Info("Exiting UserController.LoginUser() with login failed");
                    return View("LoginFailed");
                }
            }catch(Exception e)
            {
                logger.Error("Exception UserController.LoginUser()", e.Message);
                return View("LoginError");
            }
        }

        [HttpPost]
        public ActionResult RegisterUser(RegisterModel user)
        {
            logger.Info("Entering UserController.RegisterUser()");
            logger.Info("Parameters are: {0}", new JavaScriptSerializer().Serialize(user));

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
                    logger.Info("Exiting UserController.RegisterUser() with register success");
                    return View("Login");
                }
                else if(result.Equals("fail"))
                {
                    logger.Info("Exiting UserController.RegisterUser() with register fail");
                    return View("RegisterFailed");
                }
                else
                {
                    logger.Info("Exiting UserController.RegisterUser() with attempt to register duplicate user");
                    return View("DuplicateUser");
                }
            } 
            catch(Exception e)
            {
                logger.Error("Exception UserController.RegisterUser()", e.Message);
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