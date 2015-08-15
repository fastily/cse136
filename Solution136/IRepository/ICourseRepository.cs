namespace IRepository
{
    using System.Collections.Generic;

    using POCO;

    public interface ICourseRepository
    {
        Course FindCourseByName(string _courseName, ref List<string> errors);
        Course FindCourseById(string _courseName, ref List<string> errors);

        bool IsDuplicateCourse(POCO.Course _course, ref List<string> errors);
        void UpdateCourse(POCO.Course _course, ref List<string> errors);
        void AddCourse(POCO.Course _course, ref List<string> errors);
        void RemoveCourse(POCO.Course _course, ref List<string> errors);

        List<Course> GetCourseList(ref List<string> errors);
    }
}
