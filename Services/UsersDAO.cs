using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using System;
using VacationSplit.Models;
using Microsoft.Data.SqlClient;
using VacationSplit.Repositories;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace VacationSplit.Services
{
    public class UsersDAO
    {        
        public bool FindUserByEmailAndPassword(User user)
        {

            bool success = false;



            try
            {
                SecurityService securityService = new SecurityService();               
                string encryptedPassword = securityService.Encrypt(user.Password);
                using (var context = new VacationSplitContext())
                {
                     success = context.Users.Any(p => p.Password == encryptedPassword && p.Email == user.Email );                    
                }               
                

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return success;

            // return true if found in DB.
        }
    }
}
