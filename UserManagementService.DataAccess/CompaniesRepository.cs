using System;
using System.Data.Entity;
using System.Linq;
using UserManagementService.Model;

namespace UserManagementService.DataAccess
{
    public class CompaniesRepository : Repository<Company>, ICompaniesRepository
    {
        public CompaniesRepository(DbContext context) : base(context)
        {
        }

        public Company GetDefaultCompany()
        {
            return context.Set<Company>().SingleOrDefault(c => c.IsDefault);
        }
    }
}
