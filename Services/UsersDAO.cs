using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing;
using System;
using VacationSplit.Models;
using Microsoft.Data.SqlClient;
using VacationSplit.Repositories;
using System.Linq;

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
                    IQueryable<User> query = context.Users.Where(p => p.Password == encryptedPassword);
                    //query = query.Where(p=>p.Password == encryptedPassword);
                    User userA = query.First();
                    if (userA != null)
                    {
                        success = true;
                    }
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
