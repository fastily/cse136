namespace Service
{
    using System;

    using System.Collections.Generic;

    using IRepository;

    using POCO;

    public class CourseService
    {
        private readonly ICourseRepository repository;

        public CourseService(ICourseRepository repository)
        {
            this.repository = repository;
        }

        public void InsertCourse(Course course, ref List<string> errors)
        {
            if (course == null)
            {
                errors.Add("Course cannot be null");
                throw new ArgumentException();
            }
           
            if (string.IsNullOrEmpty(course.Title))
            {
                errors.Add("Course title cannot be null");
                throw new ArgumentException();
            }

            if (this.repository.IsNotDuplicateCourse(course, ref errors))
            {
                this.repository.AddCourse(course, ref errors);
            }
            else 
            {
                errors.Add("Duplicate Course");
            }
        }

        public void UpdateCourse(Course course, ref List<string> errors)
        {
            if (course == null)
            {
                errors.Add("Course cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(course.Title))
            {
                errors.Add("Course title cannot be null");
                throw new ArgumentException();
            }

            if (course.CourseId <= 0)
            {
                errors.Add("Course id cannot be null");
                throw new ArgumentException();
            }

            this.repository.UpdateCourse(course, ref errors);
        }

        public Course GetCourse(int course_id, ref List<string> errors)
        {
            if (course_id <= 0)
            {
                errors.Add("Invalid course_id");
                throw new ArgumentException();
            }

            return this.repository.FindCourseById(course_id, ref errors);
        }

        public void DeleteCourse(int course_id, ref List<string> errors)
        {
            if (course_id <= 0)
            {
                errors.Add("Invalid course_id");
                throw new ArgumentException();
            }

            this.repository.RemoveCourse(course_id, ref errors);
        }

        public List<Course> GetCourseList(ref List<string> errors)
        {
            return this.repository.GetCourseList(ref errors);
        }

        public void AssignPreReq(int courseId, int preReqCourseId, ref List<string> errors)
        {
            if (courseId <= 0)
            {
                errors.Add("Invalid courseId");
                throw new ArgumentException();
            }

            if (preReqCourseId <= 0)
            {
                errors.Add("Invalid preReqCourseId");
                throw new ArgumentException();
            }

            this.repository.AssignPreReqToCourse(courseId, preReqCourseId, ref errors);
        }

        public void RemovePreReq(int courseId, int preReqCourseId, ref List<string> errors)
        {
            if (courseId <= 0)
            {
                errors.Add("Invalid courseId");
                throw new ArgumentException();
            }

            if (preReqCourseId <= 0)
            {
                errors.Add("Invalid preReqCourseId");
                throw new ArgumentException();
            }

            this.repository.AssignPreReqToCourse(courseId, preReqCourseId, ref errors);
        }

        public List<Course> GetPreReqList(int courseId, ref List<string> errors)
        {
            if (courseId <= 0)
            {
                errors.Add("Invalid courseId");
                throw new ArgumentException();
            }

            return this.repository.GetAllPreReqs(courseId, ref errors);
        }

        public List<Course> SearchCourses(string courseName, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(courseName))
            {
                errors.Add("Must Enter something to search");
                throw new ArgumentException();
            }

            return this.repository.FindCourseByName(courseName, ref errors);
        }
    }
}
