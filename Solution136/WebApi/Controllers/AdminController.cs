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
            var errors = new List<string>();
            var repository = new AdminRepository(this.entities);
            var service = new AdminService(repository);
            var adminPoco = service.GetAdminById(adminId.ToString(), ref errors);

            if (errors.Count == 0 && adminPoco != null)
            {
                return adminPoco;
            }

            return new Admin();
        }

        [HttpPost]
        public string UpdateAdminInfo(Admin admin)
        {
            var errors = new List<string>();
            var repository = new AdminRepository(this.entities);
            var service = new AdminService(repository);
            service.UpdateAdmin(admin, ref errors);

            if (errors.Count == 0)
            {
                return "Update Successful";
            }

            return "error";
        }

        [HttpGet]
        public List<Admin> GetAdminList()
        {
            var service = new AdminService(new AdminRepository(this.entities));
            var errors = new List<string>();

            return service.GetAdminList(ref errors);
        }
    }
}