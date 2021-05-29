using System;
using System.Collections.Generic;

namespace PasswordCracker
{
    public class Bruteforce
    {
        private static int range = 0;
        private static int wordsCounter = 0;

        private static char[] characters =
        {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
        'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
        'u', 'v', 'w', 'x', 'y', 'z','A','B','C','D','E',
        'F','G','H','I','J','K','L','M','N','O','P','Q','R',
        'S','T','U','V','W','X','Y','Z','1','2','3','4','5',
        '6','7','8','9','0','!','$','#','@','-'
         };

        public static int getWordsCounter()
        {
            return wordsCounter;
        }

        public static void setWordsCounter(int wordsCounterNew)
        {
            wordsCounter = wordsCounterNew;
        }

        public static int getRange()
        {
            return range;
        }

        public static void setRange(int rangeNew)
        {
            range = rangeNew;
        }

        public static string CrackPassword()
        {
            try
            {
                IDictionary<int, string> checkWord = new Dictionary<int, string>();

                bool isRange = true;
                int wordsCountertmp = 0;
                int rangeTmp = range * 2;

                for (int i = 0; i < characters.Length; i++)
                {
                    char value = characters[i];
                    int key = checkWord.Count;
                    string password = "";

                    if (isRange)
                    {
                        checkWord.Add(key, Convert.ToString(value));

                        for (int j = 0; j < key + 1; j++)
                        {
                            password += checkWord[j];
                        }
                    }
                    else
                    {
                        for (int j = 0; j < key; j++)
                        {
                            password += checkWord[j];
                        }
                    }

                    if (wordsCountertmp == rangeTmp)
                    {
                        rangeTmp = rangeTmp + 4 * range;
                    }
                    else if (wordsCountertmp < rangeTmp && wordsCountertmp >= (rangeTmp - 100))
                    {
                        wordsCounter++;
                        if (Login.VerifyLogin(password))
                            return password;
                    }
                    wordsCountertmp++;

                    if (i / (characters.Length - 1) == 1)
                    {
                        i = -1;
                        key = checkWord.Count;
                        if (key == 1)
                        {
                            checkWord[0] = Convert.ToString(characters[0]);
                        }
                        else
                        {
                            for (int c = checkWord.Count - 1; c > -1; c--)
                            {
                                if (checkWord[c] != "-" && c == 0)
                                {
                                    int index = Array.IndexOf(characters, Convert.ToChar(checkWord[c]));
                                    checkWord[c] = Convert.ToString(characters[index + 1]);
                                    checkWord.Remove(checkWord.Count - 1);
                                    break;
                                }
                                else
                                {
                                    if (checkWord[c] != "-")
                                    {
                                        int index = Array.IndexOf(characters, Convert.ToChar(checkWord[c]));
                                        checkWord[c] = Convert.ToString(characters[index + 1]);
                                        checkWord.Remove(checkWord.Count - 1);
                                        break;
                                    }
                                    else
                                    {
                                        checkWord[c] = Convert.ToString(characters[0]);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (isRange)
                            checkWord.Remove(key);
                        else
                            checkWord.Remove(key - 1);
                    }
                    isRange = true;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

    }
}
