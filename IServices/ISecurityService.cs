using VacationSplit.Models;
using VacationSplit.Services;

namespace VacationSplit.IServices
{
    public interface ISecurityService 
    {
         bool IsValid(User user);
        string Encrypt(string clearText);
        string Decrypt(string clearText);
        bool IsValidEmail(string email);
       
    }
}
