namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IScheduleRepository
    {
        List<Schedule> GetScheduleList(string year, string quarter, ref List<string> errors);

        void CreateSchedule(Schedule schedule, ref List<string> errors);

        void AddCourseToSchedule(Schedule schedule, Course course, ref List<string> errors);

        void RemoveCourseFromSchedule(Schedule schedule, Course course, ref List<string> errors);
    }
}
