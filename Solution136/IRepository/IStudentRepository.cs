namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface IStudentRepository
    {
        void InsertStudent(Student student, ref List<string> errors);

        void UpdateStudent(Student student, ref List<string> errors);

        void DeleteStudent(string id, ref List<string> errors);

        Student GetStudentDetail(string id, ref List<string> errors);

        List<Student> GetStudentList(ref List<string> errors);

        void EnrollSchedule(string studentId, int scheduleId, ref List<string> errors);

        void DropEnrolledSchedule(string studentId, int scheduleId, ref List<string> errors);

        List<Enrollment> GetEnrollments(string studentId);
    }
}
