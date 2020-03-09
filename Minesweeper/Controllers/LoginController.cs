using System;
using System.Web.Mvc;

namespace Minesweeper.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(Models.UserModel user)
        {
            Services.Business.SecurityService ss = new Services.Business.SecurityService();
            Boolean authenticate = ss.Authenticate(user);

            if (authenticate)
            {
                return View("Minesweeper", user);
            }
            else
            {
                return View("LoginFailed");
            }
        }
    }
}