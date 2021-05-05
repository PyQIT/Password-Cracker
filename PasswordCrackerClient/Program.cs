using System;
using System.Diagnostics;

namespace PasswordCrackerClient
{
    class Program
    {
        static int stopWatchHelper = 0;

        static void menu(User user)
        {
            string control = "";
            
            Console.WriteLine("- First Step -");
            Console.WriteLine("Set password length: ");

            int passwordLength = Convert.ToInt32(Console.ReadLine());
            user.SetPassword(GeneratePassword.CreateRandomPassword(passwordLength));
            //user.SetPassword(HashPassword.generateMD5("dunlop"));
            Console.WriteLine(user.GetPassword());
            while (control != "3"){
                Console.WriteLine("\n1. BruteForce Attack");
                Console.WriteLine("2. Dictionary Attack");
                Console.WriteLine("3. Exit");
                control = Console.ReadLine();
                switch (control)
                {
                    case "1":
                        Console.WriteLine("\nWrite attack range or leave blank: ");
                        control = Console.ReadLine();

                        Stopwatch stopWatch = new Stopwatch();
                        stopWatch.Start();

                        Bruteforce.setBruteforcePattern(control);
                        ServersConnection.PostRequestBruteForce("http://localhost:1410/crack", user, Bruteforce.getBruteforcePattern(), "Serwer 1 ", stopWatch, stopWatchHelper);
                       /* ServersConnection.PostRequestBruteForce("http://localhost:1411/crack", user, Bruteforce.getBruteforcePattern(), "Serwer 2 ");
                        ServersConnection.PostRequestBruteForce("http://localhost:1412/crack", user, Bruteforce.getBruteforcePattern(), "Serwer 3 ");
                        ServersConnection.PostRequestBruteForce("http://localhost:1413/crack", user, Bruteforce.getBruteforcePattern(), "Serwer 4 ");*/
                        break;
                    case "2":
                        Console.WriteLine("\nWrite attack range: ");
                        control = Console.ReadLine();

                        Console.WriteLine("\n");

                        Stopwatch stopWatch1 = new Stopwatch();
                        stopWatch1.Start();

                        Dictionary.setPackageSize(Convert.ToInt32(control));

                        ServersConnection.PostRequestDictionaryPassword("http://localhost:1410/crack", user, "Serwer 1 ");
                        /*ServersConnection.PostRequestDictionaryPassword("http://localhost:1411/crack", user, "Serwer 2 ");
                        ServersConnection.PostRequestDictionaryPassword("http://localhost:1412/crack", user, "Serwer 3 ");
                        ServersConnection.PostRequestDictionaryPassword("http://localhost:1413/crack", user, "Serwer 4 ");*/
                        ServersConnection.PostRequestDictionary("http://localhost:1410/crack", user, Dictionary.splitDictionary(Dictionary.getDictionary()), "Serwer 1 ", stopWatch1, stopWatchHelper);
                        /*ServersConnection.PostRequestDictionary("http://localhost:1411/crack", user, Dictionary.splitDictionary(Dictionary.getDictionary()), "Serwer 2 ");
                        ServersConnection.PostRequestDictionary("http://localhost:1412/crack", user, Dictionary.splitDictionary(Dictionary.getDictionary()), "Serwer 3 ");
                        ServersConnection.PostRequestDictionary("http://localhost:1413/crack", user, Dictionary.splitDictionary(Dictionary.getDictionary()), "Serwer 4 ");*/
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        break;
                }
            }
        }

        static void Main(string[] args)
        {

            User user = new User();

            menu(user);
            Console.ReadKey();
        }
    }
}

//SPOSOB WYSYLANIA PACZEK