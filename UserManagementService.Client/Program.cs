using Microsoft.Owin.Hosting;
using System;

namespace UserManagementService.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            string uri = "http://localhost:7990";

            using (WebApp.Start<Startup>(uri))
            {
                Console.WriteLine("Web server on {0} has started...", uri);
                Console.ReadKey();
                Console.WriteLine("Web server on {0} has stopped...", uri);
            }
        }
    }
}
