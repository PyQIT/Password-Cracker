using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordCrackerClient
{
    public class Bruteforce
    {
        static int bruteforcePattern = 0;

        public static void setBruteforcePattern(int bruteforcePatternNew)
        {
            bruteforcePattern = bruteforcePatternNew;
        }

        public static int getBruteforcePattern()
        {
            return bruteforcePattern;
        }
    }
}
