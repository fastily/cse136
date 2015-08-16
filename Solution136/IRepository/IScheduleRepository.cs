namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IScheduleRepository
    {
        List<Schedule> GetScheduleList(string year, string quarter, ref List<string> errors);

        void AddCourseToSchedule(Schedule schedule,int instructorId, int dayId, int timeId, ref List<string> errors);

        void RemoveCourseFromSchedule(string year, int courseId,string quarter, ref List<string> errors);

        void IsDuplicateCourseFromSchedule(string year, int courseId, string quarter, ref List<string> errors);
    }
}
