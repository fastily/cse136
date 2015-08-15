namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using POCO;
    using Repository;
    using Service;
    using IRepository;

    public class AdminController : ApiController
    {
        private cse136Entities _cse136Entities;

        //default constructor for runtime use
        public AdminController()
        {
            _cse136Entities = new cse136Entities();
        }

        //overloaded constructor for dependency injection
        //used for testing
        public AdminController(cse136Entities Cse136Entities)
        {
            _cse136Entities = Cse136Entities;
        }

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