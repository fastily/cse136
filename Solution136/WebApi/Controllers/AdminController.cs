namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using IRepository;
    using POCO;
    using Repository;
    using Service;

    public class AdminController : ApiController
    {
        private cse136Entities entities;

        /// <summary>
        /// default constructor for runtime use
        /// </summary>
        public AdminController()
        {
            this.entities = new cse136Entities();
        }

        /// <summary>
        /// overloaded constructor for dependency injection.  Used for testing
        /// </summary>
        /// <param name="entities"></param>
        public AdminController(cse136Entities entities)
        {
            this.entities = entities;
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
            var errors = new List<string>();


            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }
    }
}