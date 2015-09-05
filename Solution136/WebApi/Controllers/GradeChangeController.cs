namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using IRepository;
    using POCO;
    using Repository;
    using Service;

    public class GradeChangeController : ApiController
    {
        private cse136Entities entities;

        public GradeChangeController()
        {
            this.entities = new cse136Entities();
        }

        public GradeChangeController(cse136Entities entities)
        {
            this.entities = entities;
        }

        [HttpPost]
        public string AddGradeChange(GradeChange gc)
        {
            var errors = new List<string>();
            var repository = new GradeChangeRepository(this.entities);
            var service = new GradeChangeService(repository);
            service.AddGradeChange(gc, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }


        [HttpPost]
        public string ApproveGradeChange(GradeChange gc)
        {
            var errors = new List<string>();
            var repository = new GradeChangeRepository(this.entities);
            var service = new GradeChangeService(repository);
            service.ApproveGradeChange(gc, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpGet]
        public GradeChange FindGradeChangeByStudentId(string student_id)
        {
            var errors = new List<string>();
            var repository = new GradeChangeRepository(this.entities);
            var service = new GradeChangeService(repository);
            return service.FindGradeChangeByStudentId(student_id, ref errors);
        }

        [HttpGet]
        public GradeChange FindGradeChangeByCourseId(int course_id)
        {
            var errors = new List<string>();
            var repository = new GradeChangeRepository(this.entities);
            var service = new GradeChangeService(repository);
            return service.FindGradeChangeByCourseId(course_id, ref errors);
        }

        [HttpGet]
        public int GetGradeChangeScheduleId(string student_id, int course_id)
        {
            var errors = new List<string>();
            var repository = new GradeChangeRepository(this.entities);
            var service = new GradeChangeService(repository);
            return service.GetGradeChangeScheduleId(student_id, course_id, ref errors);
        }
    }
}