using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UserManagementService.Model;

namespace UserManagementService.DataAccess
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        public UsersRepository(UserManagementDbContext context) 
            : base(context)
        {
        }

        public IEnumerable<User> GetAllUsersFullInfo()
        {
            return context.Set<User>().Include("Company").ToList();
        }
    }
}
