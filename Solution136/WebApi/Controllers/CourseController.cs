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

        [HttpGet]
        public Course GetCourse(string id)
        {
            var errors = new List<string>();
            var repository = new CourseRepository(this.entities);
            var service = new CourseService(repository);
            return service.GetCourse(id, ref errors);
        }

        [HttpPost]
        public string InsertCourse(Course course)
        {
            var errors = new List<string>();
            var repository = new CourseRepository(this.entities);
            var service = new CourseService(repository);
            service.InsertCourse(course, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string UpdateCourse(Course course)
        {
            var errors = new List<string>();
            var repository = new CourseRepository(this.entities);
            var service = new CourseService(repository);
            service.UpdateCourse(course, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string DeleteCourse(string id)
        {
            var errors = new List<string>();
            var repository = new CourseRepository(this.entities);
            var service = new CourseService(repository);
            service.DeleteCourse(id, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }
    }
}