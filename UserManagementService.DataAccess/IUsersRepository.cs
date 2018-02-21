using System.Collections.Generic;
using UserManagementService.Model;

namespace UserManagementService.DataAccess
{
    public interface IUsersRepository : IRepository<User>
    {
        //Declare additional operations here
        IEnumerable<User> GetAllUsersFullInfo();
    }
}