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
    }
}