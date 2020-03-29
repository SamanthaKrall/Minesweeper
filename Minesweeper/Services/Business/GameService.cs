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

		public List<HighscoreModel> getAllHighscores()
		{
			GameDAO service = new GameDAO();
			List<HighscoreModel> highscores = new List<HighscoreModel>();
			highscores = service.getHighscores();
			return highscores;
		}

		public List<HighscoreModel> getUserHighscore(string userName)
		{
			GameDAO service = new GameDAO();
			List<HighscoreModel> highscore = new List<HighscoreModel>();
			highscore = service.getUserHighscore(userName);
			return highscore;
		}

		public List<HighscoreModel> getTopThreeHighscores()
		{
			GameDAO service = new GameDAO();
			List<HighscoreModel> highscores = new List<HighscoreModel>();
			highscores = service.getTopThreeHighscores();
			return highscores;
		}
	}
}