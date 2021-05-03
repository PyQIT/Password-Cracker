using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordCrackerClient
{
    public class User
    {
        private string password;

        public void SetPassword(String password)
        {
            this.password = password;
        }

        public String GetPassword()
        {
            return password;
        }

    }
}
