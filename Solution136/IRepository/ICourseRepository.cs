namespace IRepository
{
    using System.Collections.Generic;
    using POCO;

    public interface ICourseRepository
    {
        Course FindCourseByName(string _courseName, ref List<string> errors);
        Course FindCourseById(string _courseId, ref List<string> errors);

        bool IsDuplicateCourse(Course _course, ref List<string> errors);
        void UpdateCourse(Course _course, ref List<string> errors);
        void AddCourse(Course _course, ref List<string> errors);
        void RemoveCourse(int CourseId, ref List<string> errors);
        void AssignPreReqToCourse(Course _course, Course _PreReqCourse, ref List<string> errors);

        List<Course> GetCourseList(ref List<string> errors);
    }
}
