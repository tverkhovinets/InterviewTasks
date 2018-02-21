using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementService.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private UserManagementDbContext context;

        public IUsersRepository Users { get; private set; }
        public ICompaniesRepository Companies { get; private set; }

        public UnitOfWork()
        {
            context = new UserManagementDbContext();
            Users = new UsersRepository(context);
            Companies = new CompaniesRepository(context);
        }
        public int Complete()
        {
           return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
