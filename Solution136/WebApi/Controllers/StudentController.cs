namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using POCO;
    using Repository;
    using Service;
    using IRepository;

    public class StudentController : ApiController
    {

        private cse136Entities _cse136Entities;

        //default constructor for runtime use
        public StudentController()
        {
            _cse136Entities = new cse136Entities();
        }

        //overloaded constructor for dependency injection
        //used for testing
        public StudentController(cse136Entities Cse136Entities)
        {
            _cse136Entities = Cse136Entities;
        }

        [HttpGet]
        public List<Student> GetStudentList()
        {
            var errors = new List<string>();
            var repository = new StudentRepository(_cse136Entities);
            var service = new StudentService(repository);
            return service.GetStudentList(ref errors);
        }

        [HttpGet]
        public Student GetStudent(string id)
        {
            var errors = new List<string>();
            var repository = new StudentRepository(_cse136Entities);
            var service = new StudentService(repository);
            return service.GetStudent(id, ref errors);
        }

        [HttpPost]
        public string InsertStudent(Student student)
        {
            var errors = new List<string>();
            var repository = new StudentRepository(_cse136Entities);
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
            var repository = new StudentRepository(_cse136Entities);
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
            var repository = new StudentRepository(_cse136Entities);
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