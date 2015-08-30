namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IScheduleRepository
    {
        List<Schedule> GetScheduleList(string year, string quarter, ref List<string> errors);

        List<ScheduleMin> GetAllSchedulesMin(ref List<string> errors);

        List<ScheduleMin> GetStudentScheduleMin(string id, ref List<string> errors);

        Schedule GetScheduleById(int scheduleId, ref List<string> errors);

        void AddCourseToSchedule(Schedule schedule, int instructorId, int dayId, int timeId, ref List<string> errors);

        void RemoveCourseFromSchedule(int scheduleId, ref List<string> errors);

        bool IsNotDuplicateCourseFromSchedule(int year, int courseId, string quarter, ref List<string> errors);
    }
}
