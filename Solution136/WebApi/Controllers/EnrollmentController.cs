namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using IRepository;
    using POCO;
    using Repository;
    using Service;

    public class EnrollmentController : ApiController
    {
        private cse136Entities entities;

        public EnrollmentController()
        {
            this.entities = new cse136Entities();
        }

        public EnrollmentController(cse136Entities entities)
        {
            this.entities = entities;
        }

        [HttpPost]
        public string AddEnrollment(string studentId, int scheduleId)
        {
            var errors = new List<string>();
            var repository = new EnrollmentRepository(this.entities);
            var service = new EnrollmentService(repository);
            service.AddEnrollment(studentId, scheduleId, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        public string RemoveEnrollment(string studentId, int scheduleId)
        {
            var errors = new List<string>();
            var repository = new EnrollmentRepository(this.entities);
            var service = new EnrollmentService(repository);
            service.RemoveEnrollment(studentId, scheduleId, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }
    }
}