using UserManagementService.Model;

namespace UserManagementService.DataAccess
{
    public interface ICompaniesRepository : IRepository<Company>
    {
        Company GetDefaultCompany();
    }
}
