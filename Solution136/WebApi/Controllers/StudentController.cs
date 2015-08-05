namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using POCO;
    using Repository;
    using Service;

    public class StudentController : ApiController
    {
        [HttpGet]
        public List<Student> GetStudentList()
        {
            var errors = new List<string>();
            var repository = new StudentRepository();
            var service = new StudentService(repository);
            return service.GetStudentList(ref errors);
        }

        [HttpGet]
        public Student GetStudent(string id)
        {
            var errors = new List<string>();
            var repository = new StudentRepository();
            var service = new StudentService(repository);
            return service.GetStudent(id, ref errors);
        }

        [HttpPost]
        public string InsertStudent(Student student)
        {
            var errors = new List<string>();
            var repository = new StudentRepository();
            var service = new StudentService(repository);
            service.InsertStudent(student, ref errors);
            if (errors.Count == 0)
            {
                return "ok";
            }
            
            return "error";
        }

        [HttpPost]
        public string UpdateStudent(Student student)
        {
            var errors = new List<string>();
            var repository = new StudentRepository();
            var service = new StudentService(repository);
            service.UpdateStudent(student, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string DeleteStudent(string id)
        {
            var errors = new List<string>();
            var repository = new StudentRepository();
            var service = new StudentService(repository);
            service.DeleteStudent(id, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }
    }
}