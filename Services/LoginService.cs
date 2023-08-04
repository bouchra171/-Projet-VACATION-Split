using System;
using VacationSplit.Models;
using VacationSplit.Data;
using Microsoft.EntityFrameworkCore;
using VacationSplit.IServices;

namespace VacationSplit.Services
{
    public class LoginService : ILoginService
    {
        private readonly VacationSplitContext _context;
        private readonly ISecurityService _securityService;
        public LoginService(VacationSplitContext context, ISecurityService securityService) 
        {
            _context = context;
            _securityService = securityService;
        }
        public bool FindUserByEmailAndPassword(User user)
        {

            bool success = false;



            try
            {             
                string encryptedPassword = _securityService.Encrypt(user.Password);

                     success = _context.Users.Any(p => p.Password == encryptedPassword && p.Email == user.Email );                                  
                

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

                    success = _context.Users.Any(p => p.Email == email);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return success;

        }
    }
}
