using System.Security.Cryptography;
using System.Text;

namespace Utilities
{
    public class Password
    {
        private string _password = string.Empty;

        public Password(string password)
        {
            _password = password;
        }

        public override string ToString()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convertit la chaîne en tableau d'octets
                byte[] bytes = Encoding.UTF8.GetBytes(_password);
                // Calcule le hachage
                byte[] hash = sha256.ComputeHash(bytes);
                // Convertit le tableau d'octets en chaîne hexadécimale
                return BitConverter.ToString(hash).ToLower();
            }
        }
    }
}
