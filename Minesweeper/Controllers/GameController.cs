using Minesweeper.Models;
using Minesweeper.Services.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Minesweeper.Controllers
{
    public class GameController : Controller
    {
        private MinesweeperEngine me = new MinesweeperEngine();

        [CustomAuthorization]
        public ActionResult PlayAgain()
        {
            MinesweeperEngine newMe = (MinesweeperEngine)HttpContext.Session["ME"];

            if (HttpContext.Session["Username"] == null)
            {
                return View("~/Views/User/Login.cshtml");
            }
            else
            {
                HttpContext.Session["Time"] = -1;
                return View("Minesweeper", newMe.createBoard());
            }
        }

        [CustomAuthorization]
        public ActionResult PlayMinesweeper()
        {
            if (HttpContext.Session["Username"] == null)
            {
                return View("~/Views/User/Login.cshtml");
            }
            else
            {
                HttpContext.Session["ME"] = me;
                MinesweeperEngine newMe = (MinesweeperEngine)HttpContext.Session["Username"];
                string userName = (string)HttpContext.Session["Username"];
                GameService gs = new GameService();
                HttpContext.Session["Time"] = gs.getTime(userName);
                if (gs.getGame(userName) != null)
                {
                    newMe.createSavedGame((gs.getGame(userName)));
                    return View("Minesweeper", newMe.getGrid());
                }
                else
                {
                    return View("Minesweeper", newMe.createBoard());
                }
            }
        }

        public ActionResult OnButtonClick(string mine)
        {
            MinesweeperEngine newMe = (MinesweeperEngine)HttpContext.Session["ME"];
            foreach(Button c in newMe.getGrid())
            {
                if (mine.Equals(c.Id))
                {
                    newMe.onClick(c);
                }
            }
            return View("Minesweeper", newMe.getGrid());
        }

        [HttpPost]
        public PartialViewResult OnClick(string mine)
        {
            MinesweeperEngine newMe = (MinesweeperEngine)HttpContext.Session["ME"];
            foreach(Button c in newMe.getGrid())
            {
                if (mine.Equals(c.Id))
                {
                    newMe.onClick(c);
                    if (c.Win)
                    {
                        GameService gs = new GameService();
                        gs.saveTime((string)HttpContext.Session["Username"], (int)HttpContext.Session["Time"]);
                    }
                }
            }
            return PartialView("MinesweeperBoard", newMe.getGrid());
        }

        [HttpGet]
        public ActionResult Logout()
        {
            MinesweeperEngine newMe = (MinesweeperEngine)HttpContext.Session["ME"];
            GameService gs = new GameService();
            gs.saveGame(newMe.getGrid(), (string)HttpContext.Session["Username"], (int)HttpContext.Session["Time"]);
            HttpContext.Session["Username"] = null;
            return View("~/Views/User/Login.cshtml");
        }

        public ActionResult SaveGame()
        {
            MinesweeperEngine newMe = (MinesweeperEngine)HttpContext.Session["ME"];
            string userName = (string)HttpContext.Session["Username"];
            GameService gs = new GameService();
            gs.saveGame(newMe.getGrid(), userName, (int)HttpContext.Session["Time"]);
            HttpContext.Session["Time"] = gs.getTime(userName);
            newMe.createSavedGame((gs.getGame(userName)));
            return View("Minesweeper", newMe.getGrid());
        }

        [OutputCache(NoStore = true, Location = OutputCacheLocation.Client, Duration = 1)]
        public PartialViewResult Time()
        {
            MinesweeperEngine newMe = (MinesweeperEngine)HttpContext.Session["ME"];
            if (newMe.getGrid()[0, 0].Win)
            {
                return PartialView("Time", (int)HttpContext.Session["Time"]);
            }
            else
            {
                HttpContext.Session["Time"] = (int)HttpContext.Session["Time"] + 1;
                return PartialView("Time", (int)HttpContext.Session["Time"]);
            }
        }
        // GET: Game
        public ActionResult Index()
        {
            return View();
        }
    }
}