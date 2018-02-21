using System.Linq;
using System.Web.Http;
using UserManagementService.DataAccess;
using UserManagementService.Model;
namespace UserManagementService.Controllers
{
    public class UsersController : ApiController
    {
        private IUnitOfWork unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET api/values
        public IHttpActionResult GetAll()
        {
            var users = unitOfWork.Users.GetAllUsersFullInfo();
            if (!users.Any()) return StatusCode(System.Net.HttpStatusCode.NoContent);

            return Ok(users);
        }

        // POST api/values
        [HttpPost]
        public IHttpActionResult CreateUser([FromBody] User user)
        {
            //if (user == null) return BadRequest("The user is null");
            unitOfWork.Users.Add(user);
            unitOfWork.Complete();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}
