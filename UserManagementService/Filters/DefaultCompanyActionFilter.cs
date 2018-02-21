using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Threading;
using System.Threading.Tasks;
using UserManagementService.Model;
using UserManagementService.DataAccess;

namespace UserManagementService.Filters
{
    public class DefaultCompanyActionFilter : IAutofacActionFilter
    {
        private IUnitOfWork unitOfWork;
        private const string USER_PARAM = "user";

        public DefaultCompanyActionFilter(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var user = actionContext.ActionArguments.SingleOrDefault(arg => arg.Key == USER_PARAM).Value as User;
            if (user.Company == null)
            {
                var defaultCompany = unitOfWork.Companies.GetDefaultCompany();
                user.Company = defaultCompany;
                actionContext.ActionArguments[USER_PARAM] = user;
                actionContext.ModelState.Remove(USER_PARAM + ".Company"); //Clear validation error
            }
            return Task.FromResult(0);
        }
    }
}