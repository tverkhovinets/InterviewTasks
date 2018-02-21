//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UserManagementService.DataAccess;
//using UserManagementService.Model;

//namespace UserManagementService.Tests
//{
//    class FakeUsersRepository : IUsersRepository
//    {
//        private List<User> users;

//        public FakeUsersRepository()
//        {
//            var companies = new Company[]
//            {
//                new Company
//                {
//                    Name = "Apple",
//                    IsDefault = true
//                },
//                new Company
//                {
//                    Name = "Google",
//                },
//                new Company
//                {
//                    Name = "IBM"
//                }
//            };

//            users = new List<User>
//            {
//                new User
//                {
//                    Company = companies[0],
//                    Name = "John Smith"
//                },
//                new User
//                {
//                    Company = companies[0],
//                    Name = "Steve Jobs"
//                },
//                new User
//                {
//                    Company = companies[1],
//                    Name = "Henrik Jensen"
//                },
//                new User
//                {
//                    Company = companies[2],
//                    Name = "Bill Gates"
//                }
//            };
//        }

//        public void CreateUser(User user)
//        {
//            users.Add(user);
//        }

//        public IEnumerable<User> GetAll()
//        {
//            return users;
//        }
//    }
//}
