namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using IRepository;
    using POCO;
    using Repository;
    using Service;

    public class InstructorController : ApiController
    {
        private cse136Entities entities;

        /// <summary>
        /// default constructor for runtime use
        /// </summary>
        public InstructorController()
        {
            this.entities = new cse136Entities();
        }

        /// <summary>
        /// overloaded constructor for dependency injection. used for testing
        /// 
        /// </summary>
        /// <param name="entities"></param>
        public InstructorController(cse136Entities entities)
        {
            this.entities = entities;
        }
    }
}