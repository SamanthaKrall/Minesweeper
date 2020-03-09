using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Minesweeper.Services.Data
{
    public class SecurityDAO
    {
        string connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = Test;"
            + "Integrated Security = True";

        public bool FindByUser(Models.UserModel user)
        {
            bool authenticate = false;
            string queryString = "SELECT * FROM dbo.Users WHERE USERNAME = @Username AND PASSWORD = @Password";
            using (System.Data.SqlClient.SqlConnection conn = new SqlConnection(connection))
            {
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 50).Value = user.Username;
                command.Parameters.Add("@PASSWORD", System.Data.SqlDbType.VarChar, 50).Value = user.Password;
                try
                {
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                        authenticate = true;
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return authenticate;
        }

        public bool NewUser(Models.UserModel user)
        {
            //set outcome to false because return statement should default to false not true
            bool outcome = false;
            string queryString = "INSERT INTO dbo.Users(USERNAME, PASSWORD) VALUES(@Username, @Password)";
            using (System.Data.SqlClient.SqlConnection conn = new SqlConnection(connection))
            {
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.Add("@USERNAME", System.Data.SqlDbType.VarChar, 50).Value = user.Username;
                command.Parameters.Add("@PASSWORD", System.Data.SqlDbType.VarChar, 50).Value = user.Password;
                try
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.InsertCommand = (command);
                    adapter.InsertCommand.ExecuteNonQuery();
                    command.Dispose();
                    conn.Close();
                    outcome = true;
                    return outcome;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return outcome;
        }
    }


}