namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using IRepository;
    using POCO;
    using Repository;
    using Service;

    public class StudentController : ApiController
    {
        private cse136Entities entities;

        /// <summary>
        /// default constructor for runtime use
        /// </summary>
        public StudentController()
        {
            this.entities = new cse136Entities();
        }

        /// <summary>
        /// overloaded constructor for dependency injection. used for testing
        /// </summary>
        /// <param name="entities"></param>
        public StudentController(cse136Entities entities)
        {
            this.entities = entities;
        }

        [HttpGet]
        public List<Student> GetStudentList()
        {
            var errors = new List<string>();
            var repository = new StudentRepository(this.entities);
            var service = new StudentService(repository);
            return service.GetStudentList(ref errors);
        }

        [HttpGet]
        public Student GetStudent(string id)
        {
            var errors = new List<string>();
            var repository = new StudentRepository(this.entities);
            var service = new StudentService(repository);
            return service.GetStudent(id, ref errors);
        }

        [HttpPost]
        public string InsertStudent(Student student)
        {
            var errors = new List<string>();
            var repository = new StudentRepository(this.entities);
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
            var repository = new StudentRepository(this.entities);
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
            var repository = new StudentRepository(this.entities);
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