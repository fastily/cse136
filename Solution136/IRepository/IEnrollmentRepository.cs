namespace IRepository
{
    using System.Collections.Generic;
    using POCO;

    public interface IEnrollmentRepository
    {
        void AddEnrollment(string studentId, int scheduleId, ref List<string> errors);

        void RemoveEnrollment(string studentId, int scheduleId, ref List<string> errors);

        List<POCO.Enrollment> GetAllStudentEnrolledSchedules(string studentId, ref List<string> errors);

        List<Student> GetStudentsByScheduleId(int scheduleId, ref List<string> errors);

        List<POCO.Enrollment> GetStudentEnrolledSchedulesByQuarter(string studentId, string year, string quarter, ref List<string> errors);

        bool IsNotDuplicateEnrollment(string studentId, int scheduleId, ref List<string> errors);
    }
}
