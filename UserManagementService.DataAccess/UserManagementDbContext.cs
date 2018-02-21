using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UserManagementService.Model;

namespace UserManagementService.DataAccess
{
    public class UserManagementDbContext : DbContext
    {
        static UserManagementDbContext()
        {
            Database.SetInitializer(new ContextInitializer());
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public UserManagementDbContext() : base("UsersConnection")
        {

        }
    }
}