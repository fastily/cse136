namespace WebApi136.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using POCO;

    using Repository;

    using Service;

    public class StudentController : ApiController
    {
        private readonly StudentService service = new StudentService(new StudentRepository());

        private List<string> errors = new List<string>();

        [HttpGet]
        public Student GetStudent(string id)
        {
            return this.service.GetStudent(id, ref this.errors);
        }

        [HttpPost]
        public string InsertStudent(Student student)
        {
            this.service.InsertStudent(student, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpPost]
        public string UpdateStudent(Student student)
        {
            this.service.UpdateStudent(student, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpPost]
        public string DeleteStudent(string id)
        {
            this.service.DeleteStudent(id, ref this.errors);
            return this.errors.Count == 0 ? "ok" : "Error occurred";
        }

        [HttpGet]
        public List<Student> GetStudentList()
        {
            return this.service.GetStudentList(ref this.errors);
        }

        [HttpPost]
        public void EnrollSchedule(string studentId, int scheduleId)
        {
            this.service.EnrollSchedule(studentId, scheduleId, ref this.errors);
        }

        [HttpPost]
        public void DropEnrolledSchedule(string studentId, int scheduleId)
        {
            this.service.DropEnrolledSchedule(studentId, scheduleId, ref this.errors);
        }
    }
}