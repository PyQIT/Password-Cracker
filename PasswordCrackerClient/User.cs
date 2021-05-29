using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordCrackerClient
{
    static public class User
    {
        static private string password;

        public static void SetPassword(String password1)
        {
            password = password1;
        }

        public static String GetPassword()
        {
            return password;
        }

    }
}
