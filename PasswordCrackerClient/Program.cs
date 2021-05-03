using System;

namespace PasswordCrackerClient
{
    class Program
    {

        static void menu(User user)
        {
            string control = "";
            
            Console.WriteLine("- First Step -");
            Console.WriteLine("Set password length: ");

            int passwordLength = Convert.ToInt32(Console.ReadLine());
            //user.SetPassword(GeneratePassword.CreateRandomPassword(passwordLength));
            user.SetPassword(HashPassword.generateMD5("dunlop"));

            while (control != "3"){
                Console.WriteLine("1. BruteForce Attack");
                Console.WriteLine("2. Dictionary Attack");
                Console.WriteLine("3. Exit");
                control = Console.ReadLine();
                switch (control)
                {
                    case "1":
                        Console.WriteLine("\nWrite attack range or leave blank: ");
                        control = Console.ReadLine();

                        Bruteforce.setBruteforcePattern(control);
                        ServersConnection.PostRequestBruteForce("http://localhost:1410/crack", user, Bruteforce.getBruteforcePattern());
                        break;
                    case "2":
                        Console.WriteLine("\nWrite attack range: ");
                        control = Console.ReadLine();
                        Dictionary.setPackageSize(Convert.ToInt32(control));
                        ServersConnection.PostRequestDictionaryPassword("http://localhost:1410/crack", user);
                        ServersConnection.PostRequestDictionary("http://localhost:1410/crack", user, Dictionary.splitDictionary(Dictionary.getDictionary()));
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

//DODANIE 4 SERWEROW
//POPRAWA ZLICZANIA CZASU
//DODANIE NAZWY SERWERA W INFORMACJACH
//SPOSOB WYSYLANIA PACZEK