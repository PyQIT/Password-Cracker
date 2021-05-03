using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordCracker
{
    class Dictionary
    {

        static List<string> dictionary = new List<string>();

        public static void setPackage(List<string> words)
        {
            dictionary = words;
        }

        public static string TryWord()
        {
            foreach (var word in dictionary)
            {
                string newWord = word.Remove(word.Length - 1);
                if (Login.VerifyLogin(newWord))
                {
                    return newWord;
                }
            }
            return null;
        }
    }
}
