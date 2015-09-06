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
        public List<ScheduleDay> GetDays()
        {
            var service = new ScheduleService(new ScheduleRepository(this.entities));
            var errors = new List<string>();
            return service.GetDays(ref errors);
        }

        [HttpGet]
        public List<ScheduleTime> GetTimes()
        {
            var service = new ScheduleService(new ScheduleRepository(this.entities));
            var errors = new List<string>();
            return service.GetTimes(ref errors);
        }

        [HttpGet]
        public List<Schedule> GetScheduleList(string year, string quarter)
        {
            var service = new ScheduleService(new ScheduleRepository(this.entities));
            var errors = new List<string>();

            return service.GetScheduleList(year, quarter, ref errors);
        }

        [HttpGet]
        public Schedule GetScheduleById(int id)
        {
            var service = new ScheduleService(new ScheduleRepository(this.entities));
            var errors = new List<string>();

            return service.GetScheduleById(id, ref errors);
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
        public string AddCourseToSchedule(Schedule schedule)
        {
            var errors = new List<string>();
            var repository = new ScheduleRepository(this.entities);
            var service = new ScheduleService(repository);
            service.AddCourseToSchedule(schedule, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string UpdateCourseFromSchedule(Schedule schedule)
        {
            var errors = new List<string>();
            var repository = new ScheduleRepository(this.entities);
            var service = new ScheduleService(repository);
            service.UpdateCourseFromSchedule(schedule, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpGet]
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

        [HttpPost]
        public string DeleteAllFromSchedule(ScheduleMin schedule)
        {
            var errors = new List<string>();
            var repository = new ScheduleRepository(this.entities);
            var service = new ScheduleService(repository);
            service.RemoveWholeSchedule(schedule.Year, schedule.Quarter, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpGet]
        public List<Schedule> GetInstructorSchedule(int instructorId)
        {
            var service = new ScheduleService(new ScheduleRepository(this.entities));
            var errors = new List<string>();

            return service.GetInstructorSchedule(instructorId, ref errors);
        }

        [HttpGet]
        public List<Ta> GetTaBySchedule(int scheduleId)
        {
            var service = new ScheduleService(new ScheduleRepository(this.entities));
            var errors = new List<string>();

            return service.GetTaBySchedule(scheduleId, ref errors);
        }
    }
}