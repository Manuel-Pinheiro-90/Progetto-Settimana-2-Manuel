using System.Text;
using System.Security.Cryptography;
namespace Progetto_Settimana_2_Manuel.Service
{
    public class PasswordEncoder : IPasswordEncoder
    {
        public string Encode(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.UTF8.GetBytes(password);
            var hashedPassword = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashedPassword);
        }

        public bool IsSame(string plainText, string codedText)
        {
            return Encode(plainText) == codedText;
        }
    }
}
