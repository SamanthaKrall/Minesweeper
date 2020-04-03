using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Minesweeper.Models;
using System.Data;

namespace Minesweeper.Services.Data
{
    public class SecurityDAO
    {
        string connectionString = string.Empty;

        public bool FindByUser(UserModel user)
        {
            bool result = false;
            try
            {
				string query = "SELECT * FROM dbo.Users WHERE USERNAME=@Username COLLATE SQL_Latin1_General_CP1_CS_AS AND PASSWORD=@Password COLLATE SQL_Latin1_General_CP1_CS_AS";

				using (SqlConnection cn = new SqlConnection(connectionString))
				using (SqlCommand cmd = new SqlCommand(query, cn))
				{
					cmd.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = user.Username;
					cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = user.Password;

					cn.Open();
					SqlDataReader reader = cmd.ExecuteReader();
					if (reader.HasRows)
					{
						result = true;
					}
					else
					{
						result = false;
					}
					cn.Close();
				}
            }
            catch (SqlException e)
            {
                throw e;
            }
            return result;
        }

		public string Create(RegisterModel user)
		{
			bool result = false;
			int userId = 1;
			try
			{
				string query = "SELECT * FROM dbo.Users WHERE USERNAME=@Username OR PASSWORD=@Password";
				using (SqlConnection cn = new SqlConnection(connectionString))
				using (SqlCommand cmd = new SqlCommand(query, cn))
				{
					cmd.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = user.Username;
					cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = user.Password;
					cn.Open();
					SqlDataReader reader = cmd.ExecuteReader();
					if (reader.HasRows)
					{
						result = false;
					}
					else
					{
						result = true;
					}
					cn.Close();
				}
				if (result == false)
				{
					return "duplicate";
				}
				query = "INSERT INTO dbo.Users (USERNAME, PASSWORD, FIRSTNAME, LASTNAME, SEX, AGE, STATE, EMAIL) VALUES (@Username, @Password, @Firstname, @Lastname, @Sex, @Age, @State, @Email); SELECT SCOPE_IDENTITY () As UserID";
				using (SqlConnection cn = new SqlConnection(connectionString))
				using (SqlCommand cmd = new SqlCommand(query, cn))
				{
					cmd.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = user.Username;
					cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = user.Password;
					cmd.Parameters.Add("@Firstname", SqlDbType.VarChar, 50).Value = user.Firstname;
					cmd.Parameters.Add("@Lastname", SqlDbType.VarChar, 50).Value = user.Lastname;
					cmd.Parameters.Add("@Sex", SqlDbType.VarChar, 50).Value = user.Sex;
					cmd.Parameters.Add("@Age", SqlDbType.Int).Value = user.Age;
					cmd.Parameters.Add("@State", SqlDbType.VarChar, 50).Value = user.State;
					cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = user.Email;
					cn.Open();
					SqlDataReader dataReader = cmd.ExecuteReader();
					if (dataReader.HasRows)
					{
						dataReader.Read();
						userId = Convert.ToInt32(dataReader["UserID"]);
					}
					cn.Close();
				}
				query = "INSERT INTO dbo.GameBoards (USER_ID, USERNAME, GAMEBOARD, TIME) VALUES (@Userid, @Username, @Gameboard, @Time)";
				using (SqlConnection cn = new SqlConnection(connectionString))
				using (SqlCommand cmd = new SqlCommand(query, cn))
				{
					cmd.Parameters.Add("@Userid", SqlDbType.Int).Value = userId;
					cmd.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = user.Username;
					cmd.Parameters.Add("@Gameboard", SqlDbType.VarChar).Value = "0";
					cmd.Parameters.Add("@Time", SqlDbType.Int).Value = -1;
					cn.Open();
					int rows = cmd.ExecuteNonQuery();
					if (rows == 1)
					{
						result = true;
					}
					else
					{
						result = false;
					}
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