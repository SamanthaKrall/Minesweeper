﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Minesweeper.Services.Business;

namespace HighscoreRESTService
{
    /// <summary>
    /// Summary description for HighscoreService
    /// </summary>

    public class HighscoreService : IHighscoreService
    {
        public DTO GetHighscores()
        {
            try
            {
                GameService gs = new GameService();
                DTO dto = new DTO(200, "All Highscores", gs.getAllHighscores());
                return dto;
            }
            catch (Exception e)
            {
                DTO dto = new DTO(500, "Internal Server Error", null);
                return dto;
            }
        }

        public DTO GetTopThree()
        {
            try
            {
                GameService gs = new GameService();
                DTO dto = new DTO(200, "Top Three Highscores", gs.getTopThreeHighscores());
                return dto;
            }
            catch (Exception e)
            {
                DTO dto = new DTO(500, "Internal Server Error", null);
                return dto;
            }
        }

        public DTO GetUserHighscore(string userName)
        {
            try
            {
                DTO dto;
                GameService gs = new GameService();

                if (!gs.getUserHighscore(userName).Any())
                {
                    dto = new DTO(404, "User '" + userName + "' doesn't exist.", gs.getUserHighscore(userName));
                }
                else
                {
                    dto = new DTO(200, userName + "'s Highscore", gs.getUserHighscore(userName));

                }
                return dto;
            }
            catch (Exception e)
            {
                DTO dto = new DTO(500, "Internal Server Error", null);
                return dto;
            }
        }
    }
}
