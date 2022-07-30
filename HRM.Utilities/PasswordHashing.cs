using System.Security.Cryptography;
using System.Text;
namespace HRM.PasswordHashing
{
    public class PasswordHash
    {
        public static string HashText(string text,string salt,HashAlgorithm hasher)
        {
            byte[] textWithSaltBytes = Encoding.UTF8.GetBytes(string.Concat(text, salt));
            byte[] hashedBytes = hasher.ComputeHash(textWithSaltBytes);
            hasher.Clear();
            return Convert.ToBase64String(hashedBytes);
        }
    }


   

}
