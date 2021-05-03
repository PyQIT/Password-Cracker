using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordCracker
{
    class Login
    {
        public static bool VerifyLogin(string password)
        {
            if (User.GetPassword().Equals(HashPassword.generateMD5(password)))
                return true;
            return false;
        }
    }
}
