namespace WebApi.Controllers
{
    using System.Web.Http;
    using POCO;

    public class AdminController : ApiController
    {
        [HttpGet]
        public Admin GetAdminInfo(int adminId)
        {
            //// 136 TODO: get the admin info 
            //// for now, returning the hard-coded value
            return new Admin() { FirstName = "Isaac", LastName = "Chu", Id = adminId };
        }

        [HttpPost]
        public string UpdateAdminInfo(Admin admin)
        {
            //// 136 TODO : update admin info here...
            return "update not yet implemented";
        }
    }
}