namespace IRepository
{
    using System.Collections.Generic;
    using POCO;

    public interface IEnrollmentRepository
    {
    	void AddEnrolement (int studentId, int ScheduleId, string year, string quarter, string session, Course Course, ref List<string> errors);
        void RemoveEnrolement (int studentId, int ScheduleId, ref List<string> errors);
    }
}
