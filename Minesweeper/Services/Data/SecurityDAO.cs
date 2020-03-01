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
            bool authenticate = true;
            string queryString = "SELECT * FROM dbo.Users FROM USERNAME = @Username AND PASSWORD = @Password";
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
    }
}