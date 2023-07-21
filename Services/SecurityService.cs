using System.Security.Cryptography;
using System.Text;
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
            
        }
        public bool IsValidEmail(string email)
        {
            return usersDAO.FindUserByEmail(email);

        }



        public string Encrypt(string clearText)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public  string Decrypt(string cipherText)
        {
            string encryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

    }
}
