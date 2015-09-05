namespace Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        public Schedule GetScheduleById(int id, ref List<string> errors)
        {
            if (id <= 0)
            {
                errors.Add("Invalid schedule Id");
                throw new ArgumentException();
            }

            return this.repository.GetScheduleById(id, ref errors);
        }

        public void AddCourseToSchedule(Schedule schedule, ref List<string> errors)
        {
            if (schedule == null)
            {
                errors.Add("Invalid schedule");
                throw new ArgumentException();
            }

            if (schedule.ScheduleId <= 0)
            {
                errors.Add("Invalid schedule");
                throw new ArgumentException();
            }

            if (schedule.Day.DayId <= 0)
            {
                errors.Add("Invalid Day");
                throw new ArgumentException();
            }

            if (schedule.Time.TimeId <= 0 )
            {
                errors.Add("Invalid Time");
                throw new ArgumentException();
            }

            if (schedule.Course.CourseId <= 0)
            {
                errors.Add("Invalid Course");
                throw new ArgumentException();
            }

            if (schedule.Instructor.InstructorId <= 0)
            {
                errors.Add("Invalid Instructor");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(schedule.Year))
            {
                errors.Add("Invalid year");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(schedule.Quarter))
            {
                errors.Add("Invalid quarter");
                throw new ArgumentException();
            }

            this.repository.AddCourseToSchedule(schedule, ref errors);
        }

        public void UpdateCourseFromSchedule(Schedule schedule, ref List<string> errors)
        {
            if (schedule == null)
            {
                errors.Add("Invalid schedule");
                throw new ArgumentException();
            }

            if (schedule.ScheduleId <= 0)
            {
                errors.Add("Invalid schedule");
                throw new ArgumentException();
            }

            if (schedule.Day.DayId <= 0)
            {
                errors.Add("Invalid Day");
                throw new ArgumentException();
            }

            if (schedule.Time.TimeId <= 0)
            {
                errors.Add("Invalid Time");
                throw new ArgumentException();
            }

            if (schedule.Course.CourseId <= 0)
            {
                errors.Add("Invalid Course");
                throw new ArgumentException();
            }

            if (schedule.Instructor.InstructorId <= 0)
            {
                errors.Add("Invalid Instructor");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(schedule.Year))
            {
                errors.Add("Invalid year");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(schedule.Quarter))
            {
                errors.Add("Invalid quarter");
                throw new ArgumentException();
            }

            this.repository.UpdateCourseFromSchedule(schedule, ref errors);
        }

        public void RemoveCourseFromSchedule(int scheduleId, ref List<string> errors)
        {
            if (scheduleId <= 0)
            {
                errors.Add("Invalid Schedule");
                throw new ArgumentException();
            }

            this.repository.RemoveCourseFromSchedule(scheduleId, ref errors);
        }

        public void RemoveWholeSchedule(string year, string quarter, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(year))
            {
                errors.Add("Invalid year");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(quarter))
            {
                errors.Add("Invalid quarter");
                throw new ArgumentException();
            }

            this.repository.RemoveWholeSchedule(year, quarter, ref errors);
        }

        public List<ScheduleMin> GetScheduleMin(ref List<string> errors)
        {
            List<ScheduleMin> getMinSchedule = this.repository.GetAllSchedulesMin(ref errors);
            List<ScheduleMin> convertMinSchedule = new List<ScheduleMin>();

            foreach (ScheduleMin sm in getMinSchedule)
            {
                if (convertMinSchedule.Where(x => x.Quarter == sm.Quarter && x.Year == sm.Year).Count() == 0)
                {
                    convertMinSchedule.Add(sm);
                }
            }

            return convertMinSchedule;
        }

        public List<ScheduleMin> GetStudentScheduleMin(string id, ref List<string> errors)
        {
            List<ScheduleMin> getMinSchedule = this.repository.GetStudentScheduleMin(id, ref errors);
            List<ScheduleMin> convertMinSchedule = new List<ScheduleMin>();

            foreach (ScheduleMin sm in getMinSchedule)
            {
                if (convertMinSchedule.Where(x => x.Quarter == sm.Quarter && x.Year == sm.Year).Count() == 0)
                {
                    convertMinSchedule.Add(sm);
                }
            }

            return convertMinSchedule;
        }

        public List<ScheduleDay> GetDays(ref List<string> errors)
        {
            return this.repository.GetDays(ref errors);
        }

        public List<ScheduleTime> GetTimes(ref List<string> errors)
        {
            return this.repository.GetTimes(ref errors);
        }
    }
}