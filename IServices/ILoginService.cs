using VacationSplit.Models;

namespace VacationSplit.IServices
{
    public interface ILoginService
    {
        public bool FindUserByEmailAndPassword(User user);
        public bool FindUserByEmail(string email);
    }
}
