namespace IRepository
{
    using System.Collections.Generic;
    using POCO;

    public interface IEnrollmentRepository
    {
        void AddEnrollment(string studentId, int scheduleId, ref List<string> errors);

        void RemoveEnrollment(string studentId, int scheduleId, ref List<string> errors);

        bool IsNotDuplicateEnrollment(string studentId, int scheduleId, ref List<string> errors);
    }
}
