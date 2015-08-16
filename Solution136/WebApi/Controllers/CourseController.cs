namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using IRepository;
    using POCO;
    using Repository;
    using Service;

    public class CourseController : ApiController
    {
        private cse136Entities entities;

        /// <summary>
        /// default constructor for runtime use
        /// </summary>
        public CourseController()
        {
            this.entities = new cse136Entities();
        }

        /// <summary>
        /// overloaded constructor for dependency injection. used for testing
        /// </summary>
        /// <param name="entities"></param>
        public CourseController(cse136Entities entities)
        {
            this.entities = entities;
        }

        [HttpGet]
        public List<Course> GetCourseList()
        {
            var service = new CourseService(new CourseRepository(this.entities));
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetCourseList(ref errors);
        }

        //// you can add more [HttpGet] and [HttpPost] methods as you need
    }
}