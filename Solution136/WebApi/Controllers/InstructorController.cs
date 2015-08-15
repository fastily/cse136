using System.Collections.Generic;
using System.Web.Http;
using POCO;
using Repository;
using Service;
using IRepository;

namespace WebApi.Controllers
{
    public class InstructorController : ApiController
    {
        private cse136Entities _cse136Entities;

        //default constructor for runtime use
        public InstructorController()
        {
            _cse136Entities = new cse136Entities();
        }

        //overloaded constructor for dependency injection
        //used for testing
        public InstructorController(cse136Entities Cse136Entities)
        {
            _cse136Entities = Cse136Entities;
        }

    }
}