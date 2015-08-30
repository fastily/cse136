namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using IRepository;
    using POCO;
    using Repository;
    using Service;

    public class ScheduleController : ApiController
    {
        private cse136Entities entities;

        public ScheduleController()
        {
            this.entities = new cse136Entities();
        }

        public ScheduleController(cse136Entities entities)
        {
            this.entities = entities;
        }

        [HttpGet]
        public List<Schedule> GetScheduleList(string year, string quarter)
        {
            var service = new ScheduleService(new ScheduleRepository(this.entities));
            var errors = new List<string>();

            return service.GetScheduleList(year, quarter, ref errors);
        }

        [HttpGet]
        public List<ScheduleMin> GetScheduleListMin()
        {
            var service = new ScheduleService(new ScheduleRepository(this.entities));
            var errors = new List<string>();

            return service.GetScheduleMin(ref errors);
        }

        [HttpGet]
        public List<ScheduleMin> GetStudentScheduleListMin(string id)
        {
            var service = new ScheduleService(new ScheduleRepository(this.entities));
            var errors = new List<string>();

            return service.GetStudentScheduleMin(id, ref errors);
        }


        [HttpPost]
        public string AddCourseToSchedule(Schedule schedule, int instructorId, string dayId, string timeId)
        {
            var errors = new List<string>();
            var repository = new ScheduleRepository(this.entities);
            var service = new ScheduleService(repository);
            service.AddCourseToSchedule(schedule, instructorId, dayId, timeId, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string DeleteCourseFromSchedule(int id)
        {
            var errors = new List<string>();
            var repository = new ScheduleRepository(this.entities);
            var service = new ScheduleService(repository);
            service.RemoveCourseFromSchedule(id, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }
    }
}