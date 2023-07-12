using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using System;
using VacationSplit.Models;
using Microsoft.Data.SqlClient;

namespace VacationSplit.Services
{
    public class UsersDAO
    {
        string connectionString = @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = Utilisateurs; Integrated Security = True; Connect Timeout = 30; 
                                    Encrypt=False;Trust Server Certificate=False;Application Intent = ReadWrite; Multi Subnet Failover=False";

                                   

        public bool FindUserByEmailAndPassword(User user)
        {
            // return true if found in db

            bool success = false;

            string sqlStatement = "SELECT * FROM dbo.Users WHERE email = @email AND password = @password  ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlStatement, connection);

                command.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 40).Value = user.Email;
                command.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 40).Value = user.Password;

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows) 
                    {
                        success = true;
                    }

                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            return success;
            // return true if found in DB.
        }
    }
}
