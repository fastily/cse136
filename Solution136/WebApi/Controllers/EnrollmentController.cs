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
        public string AddEnrollment(Enrollment enrollment)
        {
            var errors = new List<string>();
            var repository = new EnrollmentRepository(this.entities);
            var service = new EnrollmentService(repository);
            service.AddEnrollment(enrollment.StudentId, enrollment.ScheduleId, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string RemoveEnrollment(Enrollment enrollment)
        {
            var errors = new List<string>();
            var repository = new EnrollmentRepository(this.entities);
            var service = new EnrollmentService(repository);
            service.AddEnrollment(enrollment.StudentId, enrollment.ScheduleId, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpGet]
        public List<Enrollment> GetAllStudentEnrolledSchedules(string studentId)
        {
            var errors = new List<string>();
            var repository = new EnrollmentRepository(this.entities);
            var service = new EnrollmentService(repository);

            return service.GetAllStudentEnrolledSchedules(studentId, ref errors);
        }

        [HttpGet]
        public List<Enrollment> GetStudentEnrolledSchedulesByQuarter(string studentId, string year, string quarter)
        {
            var errors = new List<string>();
            var repository = new EnrollmentRepository(this.entities);
            var service = new EnrollmentService(repository);

            return service.GetStudentEnrolledSchedulesByQuarter(studentId, year, quarter, ref errors);
        }

    }
}