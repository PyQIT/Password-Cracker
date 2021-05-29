using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace PasswordCrackerClient
{
    class Program
    {
        static int stopWatchHelper = 0;

        static void menu()
        {
            string control = "";
            
            Console.WriteLine("- First Step -");
            Console.WriteLine("Set password length: ");

            int passwordLength = Convert.ToInt32(Console.ReadLine());
            //User.SetPassword(GeneratePassword.CreateRandomPassword(passwordLength));
            //User.SetPassword(HashPassword.generateMD5("abaaa"));
            User.SetPassword(HashPassword.generateMD5("cheese"));
            Console.WriteLine(User.GetPassword());

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

                        Bruteforce.setBruteforcePattern(Convert.ToInt32(control));
                        ThreadPool.QueueUserWorkItem(state => {
                            ServersConnection.PostRequestBruteForce("http://localhost:1410/crack", Bruteforce.getBruteforcePattern(), "Serwer 1 ", stopWatch, stopWatchHelper);
                        });
                        ThreadPool.QueueUserWorkItem(state => {
                            ServersConnection.PostRequestBruteForce("http://localhost:1411/crack", Bruteforce.getBruteforcePattern(), "Serwer 2 ", stopWatch, stopWatchHelper);
                        });
                        ThreadPool.QueueUserWorkItem(state => {
                            ServersConnection.PostRequestBruteForce("http://localhost:1412/crack", Bruteforce.getBruteforcePattern(), "Serwer 3 ", stopWatch, stopWatchHelper);
                        });
                        ThreadPool.QueueUserWorkItem(state => {
                            ServersConnection.PostRequestBruteForce("http://localhost:1413/crack", Bruteforce.getBruteforcePattern(), "Serwer 4 ", stopWatch, stopWatchHelper);
                        });
                        break;
                    case "2":
                        Dictionary.setPackageSent(0);
                        Console.WriteLine("\nWrite attack range: ");
                        control = Console.ReadLine();

                        Console.WriteLine("\n");

                        Stopwatch stopWatch1 = new Stopwatch();
                        stopWatch1.Start();

                        Dictionary.setPackageSize(Convert.ToInt32(control));
                        List<List<string>> packages = Dictionary.splitDictionary(Dictionary.getDictionary());

                        ThreadPool.QueueUserWorkItem(state =>{
                            ServersConnection.PostRequestDictionaryPassword("http://localhost:1410/crack", "Serwer 1 ");
                            ServersConnection.PostRequestDictionary("http://localhost:1410/crack", "Serwer 1 ", stopWatch1, stopWatchHelper, packages);
                        });
                        ThreadPool.QueueUserWorkItem(state =>
                        {
                            ServersConnection.PostRequestDictionaryPassword("http://localhost:1411/crack", "Serwer 2 ");
                            ServersConnection.PostRequestDictionary("http://localhost:1411/crack", "Serwer 2 ", stopWatch1, stopWatchHelper, packages);
                        });
                        ThreadPool.QueueUserWorkItem(state =>
                        {
                            ServersConnection.PostRequestDictionaryPassword("http://localhost:1412/crack", "Serwer 3 ");
                            ServersConnection.PostRequestDictionary("http://localhost:1412/crack", "Serwer 3 ", stopWatch1, stopWatchHelper, packages);
                        });
                        ThreadPool.QueueUserWorkItem(state =>
                        {
                            ServersConnection.PostRequestDictionaryPassword("http://localhost:1413/crack", "Serwer 4 ");
                            ServersConnection.PostRequestDictionary("http://localhost:1413/crack", "Serwer 4 ", stopWatch1, stopWatchHelper, packages);
                        });
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
            menu();
            Console.ReadKey();
        }
    }
}