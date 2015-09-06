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

        public CourseController()
        {
            this.entities = new cse136Entities();
        }

        public CourseController(cse136Entities entities)
        {
            this.entities = entities;
        }

        [HttpGet]
        public List<Course> GetPreReqScheduleList()
        {
            var service = new CourseService(new CourseRepository(this.entities));
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetPreReqScheduleList(ref errors);
        }

        [HttpGet]
        public List<Course> GetCourseList()
        {
            var service = new CourseService(new CourseRepository(this.entities));
            var errors = new List<string>();

            //// we could log the errors here if there are any...
            return service.GetCourseList(ref errors);
        }

        [HttpGet]
        public Course GetCourse(int id)
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
        public string DeleteCourse(int id)
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

        [HttpGet]
        public string AssignPreReq(int cId, int pr_Id)
        {
            var errors = new List<string>();
            var repository = new CourseRepository(this.entities);
            var service = new CourseService(repository);
            service.AssignPreReq(cId, pr_Id, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpGet]
        public string RemovePreReq(int cid, int prid)
        {
            var errors = new List<string>();
            var repository = new CourseRepository(this.entities);
            var service = new CourseService(repository);
            service.RemovePreReq(cid, prid, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpGet]
        public List<Course> GetPreReqList(int courseId)
        {
            var errors = new List<string>();
            var repository = new CourseRepository(this.entities);
            var service = new CourseService(repository);
            return service.GetPreReqList(courseId, ref errors);
        }
    }
}