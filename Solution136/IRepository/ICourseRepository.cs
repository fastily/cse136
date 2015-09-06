namespace IRepository
{
    using System.Collections.Generic;
    using POCO;

    public interface ICourseRepository
    {
        List<Course> FindCourseByName(string courseName, ref List<string> errors);

        Course FindCourseById(int courseId, ref List<string> errors);

        bool IsNotDuplicateCourse(Course course, ref List<string> errors);

        void UpdateCourse(Course course, ref List<string> errors);

        void AddCourse(Course course, ref List<string> errors);

        void RemoveCourse(int courseId, ref List<string> errors);

        List<Course> GetCourseList(ref List<string> errors);

        void AssignPreReqToCourse(int courseId, int preReqCourseId, ref List<string> errors);

        void RemovePreReqFromCourse(int courseId, int preReqToRemoveCourseId, ref List<string> errors);

        List<Course> GetAllPreReqs(int courseId, ref List<string> errors);

        List<Course> GetAllScheduleCourseList(ref List<string> errors);

        ////Course GetCourse(string course_id, ref List<string> errors);
    }
}
