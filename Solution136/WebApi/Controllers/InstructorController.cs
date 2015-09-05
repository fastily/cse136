namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using IRepository;
    using POCO;
    using Repository;
    using Service;

    public class InstructorController : ApiController
    {
        private cse136Entities entities;

        /// <summary>
        /// default constructor for runtime use
        /// </summary>
        public InstructorController()
        {
            this.entities = new cse136Entities();
        }

        /// <summary>
        /// overloaded constructor for dependency injection. used for testing
        /// 
        /// </summary>
        /// <param name="entities"></param>
        public InstructorController(cse136Entities entities)
        {
            this.entities = entities;
        }

        [HttpGet]
        public List<Instructor> GetInstructorList()
        {
            var errors = new List<string>();
            var repository = new InstructorRepository(this.entities);
            var service = new InstructorService(repository);
            return service.GetInstructorList(ref errors);
        }

        [HttpGet]
        public Instructor GetInstructor(string id)
        {
            var errors = new List<string>();
            var repository = new InstructorRepository(this.entities);
            var service = new InstructorService(repository);
            return service.GetInstructor(id, ref errors);
        }

        [HttpPost]
        public string InsertInstructor(Instructor instructor)
        {
            var errors = new List<string>();
            var repository = new InstructorRepository(this.entities);
            var service = new InstructorService(repository);
            service.InsertInstructor(instructor, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string UpdateInstructor(Instructor instructor)
        {
            var errors = new List<string>();
            var repository = new InstructorRepository(this.entities);
            var service = new InstructorService(repository);
            service.UpdateInstructor(instructor, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpGet]
        public string DeleteInstructor(int id)
        {
            var errors = new List<string>();
            var repository = new InstructorRepository(this.entities);
            var service = new InstructorService(repository);
            service.DeleteInstructor(id, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }
        
        [HttpPost]
        public string AssignGrade(Schedule schedule, string studentId, int instructorId, string grade)
        {
            var errors = new List<string>();
            var repository = new InstructorRepository(this.entities);
            var service = new InstructorService(repository);
            service.AssignGrade(schedule, studentId, instructorId, grade, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string RespondToPreReqOverride(int scheduleId, string studentId)
        {
            var errors = new List<string>();
            var repository = new InstructorRepository(this.entities);
            var service = new InstructorService(repository);
            service.RespondToPreReqOverride(scheduleId, studentId, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }
    }
}