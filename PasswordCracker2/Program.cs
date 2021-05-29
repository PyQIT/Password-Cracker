using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.Net;
using Nancy;
using Nancy.Hosting.Self;

namespace PasswordCracker
{

    class Program : NancyModule
    {
        static void Main(string[] args)
        {

            using (var host = new NancyHost(new Uri("http://localhost:1411")))
            {
                host.Start();
                Console.WriteLine("Running on http://localhost:1411");
                Console.ReadLine();
            }

        }
    }
}