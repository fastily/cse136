namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IScheduleRepository
    {
        List<Schedule> GetScheduleList(string year, string quarter, ref List<string> errors);
        void CreateSchedule(Schedule _Schedule, ref List<string> errors);
        void AddCourseToSchedule(Schedule _Schedule, Course _Course, ref List<string> errors);
        void RemoveCourseFromSchedule(Schedule _Schedule, Course _Course, ref List<string> errors);
    }
}
