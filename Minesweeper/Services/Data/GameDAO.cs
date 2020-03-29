using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Minesweeper.Models;


namespace Minesweeper.Services.Data
{
    public class GameDAO
    {
        string connectionString = string.Empty;
        public Button[] getGame(string userName)
        {
            bool result = false;
            Button[] savedGame = new Button[225];
            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                string query = "SELECT * FROM dbo.GameBoards WHERE USERNAME=@Username";
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = userName;
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    if ((string)reader["GAMEBOARD"] != "0")
                    {
                        savedGame = js.Deserialize<Button[]>((string)reader["GAMEBOARD"]);
                        result = true;
                    }
                    else
                    {
                        result = false;
                        cn.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            if (result)
            {
                return savedGame;
            }
            else
            {
                return null;
            }
        }

        public string saveGame(Button[,] game, string userName, int time)
        {
            bool result = false;
            JavaScriptSerializer js = new JavaScriptSerializer();
            string JSONBoard = js.Serialize(game);
            try
            {
                string query = "UPDATE dbo.GameBoards SET GAMEBOARD=@Gameboard, TIME=@Time WHERE USERNAME=@Username";
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@Gameboard", SqlDbType.VarChar).Value = JSONBoard;
                    cmd.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = userName;
                    cmd.Parameters.Add("@Time", SqlDbType.Int).Value = time;
                    cn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 1)
                        result = true;
                    else
                        result = false;
                    cn.Close();
                }
            }
            catch(SqlException e)
            {
                throw e;
            }
            if (result == false)
                return "fail";
            else
                return "success";
        }

        public int getTime(string userName)
        {
            int time;
            try
            {
                string query = "SELECT * FROM dbo.GameBoards WHERE USERNAME=@Username";
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = userName;
                    Trace.WriteLine(userName);
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    time = (int)reader["TIME"];
                    cn.Close();
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            return time;
        }

        public string saveTime(string userName, int time)
        {
            bool result;
            int userId = 0;
            try
            {
                string query = "SELECT * FROM dbo.Users WHERE USERNAME=@Username";
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = userName;
                    cn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    dataReader.Read();
                    userId = (int)dataReader["ID"];
                    cn.Close();
                }
                query = "INSERT INTO dbo.Highscores (USER_ID, USERNAME, TIME) VALUES (@Userid, @Username, @Time)";
                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@Userid", SqlDbType.Int).Value = userId;
                    cmd.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = userName;
                    cmd.Parameters.Add("@Time", SqlDbType.Int).Value = time;
                    cn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows == 1)
                        result = true;
                    else
                        result = false;
                    cn.Close();
                }
            }
            catch (SqlException e)
            {
                throw e;
            }

            if (result == false)
            {
                return "fail";
            }
            else
            {
                return "success";
            }
        }

        public List<HighscoreModel> getHighscores()
        {
            List<HighscoreModel> highscores = new List<HighscoreModel>();

            try
            {
                string query = "SELECT * FROM dbo.Highscores ORDER BY TIME ASC";

                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = (int)reader["ID"];
                        string userName = (string)reader["USERNAME"];
                        int time = (int)reader["TIME"];

                        HighscoreModel hs = new HighscoreModel();
                        hs.Id = id;
                        hs.Username = userName;
                        hs.Time = time;

                        highscores.Add(hs);
                    }
                    cn.Close();
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            return highscores;
        }

        public List<HighscoreModel> getUserHighscore(string username)
        {
            List<HighscoreModel> highscore = new List<HighscoreModel>();

            try
            {
                string query = "SELECT TOP 1 * FROM dbo.Highscores WHERE USERNAME=@Username ORDER BY TIME ASC";

                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cmd.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = username;
                    
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = (int)reader["ID"];
                        string userName = (string)reader["USERNAME"];
                        int time = (int)reader["TIME"];

                        HighscoreModel hs = new HighscoreModel();
                        hs.Id = id;
                        hs.Username = userName;
                        hs.Time = time;

                        highscore.Add(hs);
                    }
                    cn.Close();
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            return highscore;
        }

        public List<HighscoreModel> getTopThreeHighscores()
        {
            List<HighscoreModel> highscores = new List<HighscoreModel>();

            try
            {
                string query = "SELECT TOP 3 * FROM dbo.Highscores ORDER BY TIME ASC";

                using (SqlConnection cn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = (int)reader["ID"];
                        string userName = (string)reader["USERNAME"];
                        int time = (int)reader["TIME"];

                        HighscoreModel hs = new HighscoreModel();
                        hs.Id = id;
                        hs.Username = userName;
                        hs.Time = time;

                        highscores.Add(hs);
                    }
                    cn.Close();
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            return highscores;
        }
    }
}