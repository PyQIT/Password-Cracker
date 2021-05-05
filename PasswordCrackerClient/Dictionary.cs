using System;
using System.Collections.Generic;
using System.Linq;

namespace PasswordCrackerClient
{
    public class Dictionary
    {

        static int packageSent = 0;
        static int packageSize = 0;

        public static List<string> getDictionary()
        {
            string text = System.IO.File.ReadAllText("dictionary.txt");

            List<string> words = text.Split('\n').ToList();

            return words;
        }

        public static void setPackageSize(int packageSizeNew)
        {
            packageSize = packageSizeNew;
        }

        public static List<string> splitDictionary(List<string> dictionary)
        {
            List<List<string>> packages = new List<List<string>>();

            int dictionarySize = dictionary.Count;

            int startNumber = 0;

            double packagesNumber = Convert.ToDouble(dictionarySize) / Convert.ToDouble(packageSize);

            if ((Convert.ToInt32(packagesNumber) * packageSize) < dictionarySize)
                packagesNumber += 1.0;

            for(int i = 0; i < Convert.ToInt32(packagesNumber); i++)
            {
                List<string> package = new List<string>();

                if (i < Convert.ToInt32(packagesNumber) - 1)
                {
                    for(int j = startNumber; j < packageSize; j++)
                    {
                        package.Add(dictionary[j]);
                    }

                    startNumber += packageSize;

                }
                else
                {
                    for (int x = startNumber; x < dictionarySize - 1; x++)
                    {
                        package.Add(dictionary[x]);
                    }

                }

                packages.Add(package);
            }

            return dictionary;
        }

    }
}
