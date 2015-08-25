namespace Service
{
    using System;
    using System.Collections.Generic;
    using IRepository;
    using POCO;

    public class ScheduleService
    {
        private readonly IScheduleRepository repository;

        public ScheduleService(IScheduleRepository repository)
        {
            this.repository = repository;
        }

        public List<Schedule> GetScheduleList(string year, string quarter, ref List<string> errors)
        {
            return this.repository.GetScheduleList(year, quarter, ref errors);
        }

        public void AddCourseToSchedule(Schedule schedule, int instructorId, string dayId, string timeId, ref List<string> errors)
        {    
            if (schedule == null)
            {
                errors.Add("Invalid schedule");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(dayId))
            {
                errors.Add("Invalid Day");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(timeId))
            {
                errors.Add("Invalid Time");
                throw new ArgumentException();
            }

            this.repository.AddCourseToSchedule(schedule, instructorId, int.Parse(dayId), int.Parse(timeId), ref errors);
        }

        public void RemoveCourseFromSchedule(string scheduleId, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(scheduleId))
            {
                errors.Add("Invalid Schedule");
                throw new ArgumentException();
            }

            this.repository.RemoveCourseFromSchedule(int.Parse(scheduleId), ref errors);
        }
    }
}
