using VacationSplit.Models;

namespace VacationSplit.Services
{
    public class SecurityService
    {

      //  List<User> knownUsers = new List<User> ();
        UsersDAO usersDAO = new UsersDAO ();

        public SecurityService()
        {
           /*
            knownUsers.Add (new User { Id = 0, Email = "BillGates", Password = "bigbucks"});
            knownUsers.Add(new User { Id = 1, Email = "MarieCurie", Password = "radioactive" });
            knownUsers.Add(new User { Id = 2, Email = "WatstonCrick", Password = "dna" });
            knownUsers.Add(new User { Id = 3, Email = "Alexander", Password = "abcdef" }); */
        }

        public bool IsValid(User user)
        {
            return usersDAO.FindUserByEmailAndPassword(user);
            // return true if found in the list
           // return knownUsers.Any(x => x.Email == user.Email && x.Password == user.Password);
        }

    }
}
