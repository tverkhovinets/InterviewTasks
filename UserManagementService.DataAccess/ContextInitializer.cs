using System.Collections.Generic;
using System.Data.Entity;
using UserManagementService.Model;

namespace UserManagementService.DataAccess
{
    public class ContextInitializer: DropCreateDatabaseAlways<UserManagementDbContext>
    {

        protected override void Seed(UserManagementDbContext context)
        {
            var companies = new Company []
            {
                new Company
                {
                    Name = "Apple", 
                    IsDefault = true
                },
                new Company
                {
                    Name = "Google",
                },
                new Company
                {
                    Name = "IBM"
                }
            };

            var initialUsersData = new List<User>
            {
                new User
                {
                    Company = companies[0],
                    Name = "John Smith"
                },
                new User
                {
                    Company = companies[0],
                    Name = "Steve Jobs"
                },
                new User
                {
                    Company = companies[1],
                    Name = "Henrik Jensen"
                },
                new User
                {
                    Company = companies[2],
                    Name = "Bill Gates"
                }
            };

            context.Users.AddRange(initialUsersData);
            context.SaveChanges();
        }
    }
}