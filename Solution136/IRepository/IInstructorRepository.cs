namespace IRepository
{
    using System.Collections.Generic;
    using POCO;

    public interface IInstructorRepository
    {
        Instructor FindInstructorByName(string instructorName, ref List<string> errors);

        Instructor FindInstructorById(int instructorId, ref List<string> errors);

        bool IsNotDuplicateInstructor(Instructor instructor, ref List<string> errors);

        void UpdateInstructor(Instructor instructor, ref List<string> errors);

        void AddInstructor(Instructor instructor, ref List<string> errors);

        void RemoveInstructor(int instructor_id, ref List<string> errors);

        List<Instructor> GetInstructorList(ref List<string> errors);

        void AssignGradeToStudent(Schedule schedule, string studentId, int instructorId, string grade, ref List<string> errors);

        void ApprovePreReqOverride(int scheduleId, string studentId, ref List<string> errors);

        void InsertInstructor(Instructor instructor, ref List<string> errors);
    }
}
