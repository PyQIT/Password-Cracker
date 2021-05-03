using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordCrackerClient
{
    public class GeneratePassword
    {
        public static string CreateRandomPassword(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            string generatedPassword = new string(stringChars);

            string hashedPassword = HashPassword.generateMD5(generatedPassword);

            return hashedPassword;
        }
    }
}
