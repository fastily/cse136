namespace IRepository
{
    using System.Collections.Generic;
    using POCO;

    public interface IEnrollmentRepository
    {
        void AddEnrollment(string studentId, string year, string quarter, string session, Course course, ref List<string> errors);

        void RemoveEnrollment(string studentId, int scheduleId, ref List<string> errors);
    }
}
