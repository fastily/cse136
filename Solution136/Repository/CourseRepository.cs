namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using IRepository;
    using POCO;

    public class CourseRepository : BaseRepository, ICourseRepository
    {
        private cse136Entities context;

        ////private const string GetCourseListProcedure = "spGetCourseList";

        public CourseRepository(cse136Entities entities)
        {
            this.context = entities;
        }

        ////probably not necessary
        public Course FindCourseByName(string courseName, ref List<string> errors)
        {
            POCO.Course pocoCourse = new POCO.Course();
            course db_course;
            try
            {
                db_course = this.context.courses.Where(x => x.course_title == courseName).First();
                if (db_course != null)
                {
                    pocoCourse.CourseId = db_course.course_id.ToString();
                    pocoCourse.Description = db_course.course_description;
                    pocoCourse.Title = db_course.course_title;
                    pocoCourse.CourseLevel = 0;
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }

            return pocoCourse;
        }

        ////shouldn't call this method unless we know course exists
        public Course FindCourseById(string courseName, ref List<string> errors)
        {
            POCO.Course pocoCourse = new POCO.Course();
            course db_course;
            try
            {
                ////will search primary key
                db_course = this.context.courses.Find(courseName);
                if (db_course != null)
                {
                    pocoCourse.CourseId = db_course.course_id.ToString();
                    pocoCourse.Description = db_course.course_description;
                    pocoCourse.Title = db_course.course_title;
                    pocoCourse.CourseLevel = 0;
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }

            return pocoCourse;
        }

        ////good method for validation when adding new course
        public bool IsDuplicateCourse(POCO.Course c, ref List<string> errors)
        {
            var db_course = new course();

            try
            {
                db_course = this.context.courses.Find(db_course);

                if (db_course == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }

            return true;
        }

        ////Unsure If We need to get course before updating... TODO
        public void UpdateCourse(POCO.Course c, ref List<string> errors)
        {
            var db_course = new course();

            try
            {
                ////might have to retrieve course then update, but I dont think so
                db_course.course_level = c.CourseLevel.ToString();
                db_course.course_description = c.Description;
                db_course.course_title = c.Title;
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }

        public void AddCourse(POCO.Course c, ref List<string> errors)
        {
            var db_course = new course();

            try
            {
                db_course.course_level = c.CourseLevel.ToString();
                db_course.course_description = c.Description;
                db_course.course_title = c.Title;
                this.context.courses.Add(db_course);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }

        public void RemoveCourse(int course_id, ref List<string> errors)
        {
            var db_course = new course();

            try
            {
                db_course.course_id = course_id;
                this.context.courses.Remove(db_course);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }

        public void AssignPreReqToCourse(Course course, Course preReqCourse, ref List<string> errors)
        {
        }

        public List<Course> GetCourseList(ref List<string> errors)
        {
            List<POCO.Course> pocoCourseList = new List<POCO.Course>();
            List<course> db_courseList;
            try
            {
                db_courseList = this.context.courses.ToList();

                foreach (course i_course in db_courseList)
                {
                    var tempPoco = new POCO.Course();
                    tempPoco.CourseId = i_course.course_id.ToString();
                    tempPoco.CourseLevel = (CourseLevel)Enum.Parse(typeof(CourseLevel), i_course.course_level);
                    tempPoco.Description = i_course.course_description;
                    tempPoco.Title = i_course.course_title;
                    pocoCourseList.Add(tempPoco);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }

            return pocoCourseList;
        }
    }
}
