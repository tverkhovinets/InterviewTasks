using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UserManagementService.Model;

namespace UserManagementService.ConsoleClient
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }
        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:7990");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            while (true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Getting all users...");
                    var users = await GetUsersAsync();
                    if (users == null || !users.Any())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Failed to retrieve any users.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        ShowUsers(users);
                    }
                    Console.WriteLine("Would you like to add a user? Y/N");
                    var answer = Console.ReadLine();
                    switch (answer.ToLower())
                    {
                        case "y":
                            await SendCreateUserRequest();
                            break;
                        case "n":
                            return;
                    }

                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                }
            }

        }

        static void ShowUsers(IEnumerable<User> users)
        {
            foreach(var user in users)
            {
                var company = user.Company == null ? "Error!" : user.Company.Name;
                Console.WriteLine("Name: {0,10}\tCompany: {1,10}", user.Name, company);
            }
        }

        static async Task SendCreateUserRequest()
        {
            Console.WriteLine("Please enter user details in format [username] [company name](optional)");
            var userInfoString = Console.ReadLine();
            var userInfoArray = userInfoString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            User user = null;

            if (userInfoArray.Length == 0)
            {
                Console.WriteLine("Could not read the input string.");
                return;
            }
            else if (userInfoArray.Length == 1)
            {
                user = new User
                {
                    Name = userInfoArray[0]
                };
            }
            else
            {
                user = new User
                {
                    Name = userInfoArray[0],
                    Company = new Company
                    {
                        Name = userInfoArray[1]
                    }
                };
            }

            var url = await CreateUserAsync(user);
            Console.WriteLine($"Created at {url}");
        }

        static async Task<IEnumerable<User>> GetUsersAsync()
        {
            var response = await client.GetAsync($"api/users/");
            PrintStatusCode(response);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<User>>();
            }
            return Enumerable.Empty<User>();
        }

        private static void PrintStatusCode(HttpResponseMessage response)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Status code: {response.StatusCode}\nErrorMessage: { response.ReasonPhrase}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static async Task<Uri> CreateUserAsync(User user)
        {
            var response = await client.PostAsJsonAsync(
                "api/users", user);
            PrintStatusCode(response);

            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }
    }
}
