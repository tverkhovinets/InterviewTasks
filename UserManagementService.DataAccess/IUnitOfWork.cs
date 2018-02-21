using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementService.DataAccess
{
    public interface IUnitOfWork: IDisposable
    {
        IUsersRepository Users { get; }
        ICompaniesRepository Companies { get; }
        int Complete();
    }
}
