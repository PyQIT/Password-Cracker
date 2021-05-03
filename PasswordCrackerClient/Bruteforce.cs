using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordCrackerClient
{
    public class Bruteforce
    {
        static string bruteforcePattern = "";

        public static void setBruteforcePattern(string bruteforcePatternNew)
        {
            bruteforcePattern = bruteforcePatternNew;
        }

        public static string getBruteforcePattern()
        {
            return bruteforcePattern;
        }
    }
}
