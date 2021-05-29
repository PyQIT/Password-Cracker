using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordCracker
{
    public class User
    {
        private static string password;

        public static void SetPassword(String passwordNew)
        {
            password = passwordNew;
        }

        public static String GetPassword()
        {
            return password;
        }

    }
}
