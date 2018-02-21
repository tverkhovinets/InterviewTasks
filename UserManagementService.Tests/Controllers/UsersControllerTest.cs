//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Web.Http;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using UserManagementService.Controllers;
//using UserManagementService.Model;
//using UserManagementService.DataAccess;
//using Moq;
//using System.Web.Http.Results;
//using System.Web.Http.Routing;

//namespace UserManagementService.Tests.Controllers
//{
//    [TestClass]
//    public class UsersControllerTest
//    {
//        private User [] allUsers;
//        private Company [] allCompanies;

//        [TestInitialize]
//        public void Init()
//        {
//            allCompanies = new Company[]
//                  {
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
//                  };

//            allUsers = new User []
//            {
//                new User
//                {
//                    Company = allCompanies[0],
//                    Name = "John Smith"
//                },
//                new User
//                {
//                    Company = allCompanies[0],
//                    Name = "Steve Jobs"
//                },
//                new User
//                {
//                    Company = allCompanies[1],
//                    Name = "Henrik Jensen"
//                },
//                new User
//                {
//                    Company = allCompanies[2],
//                    Name = "Bill Gates"
//                }
//            };
//        }

//        [TestMethod]
//        public void GetAll_StatusOKReturned()
//        {
//            // Arrange
//            var repositoryMock = new Mock<IUsersRepository>();
//            repositoryMock.Setup(m => m.GetAll()).Returns(allUsers);
//            UsersController controller = new UsersController(repositoryMock.Object);

//            // Act
//            var response = controller.GetAll();
//            var contentResponse = response as OkNegotiatedContentResult<IEnumerable<User>>;

//            // Assert
//            Assert.IsNotNull(contentResponse);
//            Assert.AreEqual(4, contentResponse.Content.Count());
//            repositoryMock.Verify(v => v.GetAll(), Times.Once);
//        }

//        [TestMethod]
//        public void GetAll_ExceptionOccured_StatusServerErrorReturned()
//        {
//            // Arrange
//            var repositoryMock = new Mock<IUsersRepository>();
//            repositoryMock.Setup(m => m.GetAll()).Throws<Exception>();
//            UsersController controller = new UsersController(repositoryMock.Object);

//            // Act
//            var response = controller.GetAll();
//            var exceptionResponse = response as ExceptionResult;
            
//            //Assert
//            Assert.IsNotNull(exceptionResponse);
//            repositoryMock.Verify(v => v.GetAll(), Times.Once);
//        }

//        [TestMethod]
//        public void CreateUser_StatusCreatedReturned()
//        {
//            // Arrange
//            var repositoryMock = new Mock<IUsersRepository>();
//            UsersController controller = new UsersController(repositoryMock.Object);
//            controller.Request = new HttpRequestMessage
//            {
//                RequestUri = new Uri("http://localhost/api/users")
//            };
//            controller.Configuration = new HttpConfiguration();
//            controller.Configuration.Routes.MapHttpRoute(
//                name: "DefaultApi",
//                routeTemplate: "api/{controller}/{id}",
//                defaults: new { id = RouteParameter.Optional });

//            controller.RequestContext.RouteData = new HttpRouteData(
//                route: new HttpRoute(),
//                values: new HttpRouteValueDictionary { { "controller", "users"} });

//            var userToAdd = new User
//            {
//                Id = 7,
//                Name = "Peter Nelson",
//                Company = allCompanies[1]
//            };
//            var returnResult = new List<User>(allUsers)
//            {
//                userToAdd
//            };

//            repositoryMock.Setup(m => m.Add(It.IsAny<User>()));
                
//            // Act
//            var response = controller.CreateUser(userToAdd);
//            var createdResponse = response as CreatedNegotiatedContentResult<User>;
//            //Assert
//            Assert.IsNotNull(createdResponse);
//            Assert.AreEqual(new Uri("http://localhost/api/users/7"), createdResponse.Location);
//            repositoryMock.Verify(v => v.Add(It.Is<User>(p => p.Equals(userToAdd))));
//        }

//        [TestMethod]
//        public void CreateUser_BadRequestReturned()
//        {
//            var repositoryMock = new Mock<IUsersRepository>();
//            repositoryMock.Setup(m => m.GetAll()).Throws<Exception>();
//            UsersController controller = new UsersController(repositoryMock.Object);

//            var userToAdd = new User
//            {
//                Id = 7,
//                Name = "Peter Nelson",
//                Company = allCompanies[1]
//            };
//            var returnResult = new List<User>(allUsers)
//            {
//                userToAdd
//            };

//            repositoryMock.Setup(m => m.Add(It.IsAny<User>()));
//            // Act
//            var response = controller.CreateUser(userToAdd);
//            var badRequestResponse = response as BadRequestErrorMessageResult;

//            //Assert
//            Assert.IsNotNull(badRequestResponse);
//            repositoryMock.Verify(v => v.Add(It.Is<User>(p => p.Equals(userToAdd))));
//        }

//        //[TestMethod]
//        //public void CreateUser_UserWithoutCompany_UserWithCompanyInserted()
//        //{
//        //    var repositoryMock = new Mock<IUsersRepository>();
//        //    repositoryMock.Setup(m => m.GetAll()).Throws<Exception>();
//        //    UsersController controller = new UsersController(repositoryMock.Object);

//        //    var userToAdd = new User
//        //    {
//        //        Id = 7,
//        //        Name = "Peter Nelson",
//        //    };
//        //    var returnResult = new List<User>(allUsers)
//        //    {
//        //        userToAdd
//        //    };

//        //    repositoryMock.Setup(m => m.CreateUser(It.IsAny<User>()));
//        //    // Act
//        //    var response = controller.CreateUser(userToAdd);
//        //    var createdResponse = response as CreatedNegotiatedContentResult<User>;

//        //    //Assert
//        //    Assert.IsNotNull(createdResponse);
//        //    Assert.IsNotNull(createdResponse.Content.Company);
//        //    repositoryMock.Verify(v => v.CreateUser(It.Is<User>(p => p.Equals(userToAdd))));
//        //}
//    }
//}
