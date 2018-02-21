using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UserManagementService.Model;

namespace UserManagementService.DataAccess
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        //public UserManagementDbContext UserManagementDbContext
        //{
        //    get
        //    {
        //        return context as UserManagementDbContext;
        //    }
        //}

        public UsersRepository(UserManagementDbContext context) 
            : base(context)
        {
        }

        public IEnumerable<User> GetAllUsersFullInfo()
        {
            var result = from entity in context.Set<User>()
                         select new
                         {
                             Name = entity.Name,
                             Company = entity.Company.Name
                         };
            return context.Set<User>().Include("Company").ToList();
        }
    }
}
