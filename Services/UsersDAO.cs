using System;
using VacationSplit.Models;
using VacationSplit.Repositories;


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
           
        }
        public bool FindUserByEmail(string email)
        {

            bool success = false;



            try
            {
                SecurityService securityService = new SecurityService();                
                using (var context = new VacationSplitContext())
                {
                    success = context.Users.Any(p => p.Email == email);
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return success;

        }
    }
}
