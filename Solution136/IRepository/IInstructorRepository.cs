using System.Collections.Generic;
using POCO;

namespace IRepository
{
    public interface IInstructorRepository
    {
        Instructor FindInstructorByName(string _InstructorName, ref List<string> errors);
        Course FindInstructorById(string _InstructorId, ref List<string> errors);

        bool IsDuplicateInstructor(Instructor _Instructor, ref List<string> errors);
        void UpdateInstructor(Instructor _Instructor, ref List<string> errors);
        void AddInstructor(Instructor _Instructor, ref List<string> errors);
        void RemoveInstructor(Instructor _Instructor, ref List<string> errors);

        List<Instructor> GetInstructorList(ref List<string> errors);
    }
}
