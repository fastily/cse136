namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using POCO;
    using Repository;
    using Service;
    using IRepository;

    public class CourseController : ApiController
    {
        private cse136Entities _cse136Entities;

        //default constructor for runtime use
        public CourseController()
        {
            _cse136Entities = new cse136Entities();
        }

        //overloaded constructor for dependency injection
        //used for testing
        public CourseController(cse136Entities Cse136Entities)
        {
            _cse136Entities = Cse136Entities;
        }

        [HttpGet]
        public List<Course> GetCourseList()
        {
            var service = new CourseService(new CourseRepository(_cse136Entities));
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetCourseList(ref errors);
        }

        //// you can add more [HttpGet] and [HttpPost] methods as you need
    }
}