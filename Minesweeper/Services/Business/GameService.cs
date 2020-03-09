using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Minesweeper.Models;
using Minesweeper.Services.Data;

namespace Minesweeper.Services.Business
{
    public class GameService
    {
        public string saveGame(Button[,] game, string userName, int time)
        {
            GameDAO service = new GameDAO();
            return service.saveGame(game, userName, time);
        }

        public Button[] getGame(string userName)
        {
            GameDAO service = new GameDAO();
            return service.getGame(userName);
        }

        public int getTime(string userName)
        {
            GameDAO service = new GameDAO();
            return service.getTime(userName);
        }

        public void saveTime(string userName, int time)
        {
            GameDAO service = new GameDAO();
            service.saveTime(userName, time);
        }
    }
}