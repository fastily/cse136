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
        private cse136Entities _context;

        private const string GetCourseListProcedure = "spGetCourseList";

        public CourseRepository(cse136Entities _cse136Entities)
        {
            _context = _cse136Entities;
        }

        //probably not necessary
        public Course FindCourseByName(string _courseName, ref List<string> errors)
        {
            POCO.Course pocoCourse = new POCO.Course();
            course dbCourse;
            try
            {
                dbCourse = _context.courses.Where(x => x.course_title == _courseName).First();
                if (dbCourse != null)
                {
                    pocoCourse.CourseId = dbCourse.course_id.ToString();
                    pocoCourse.Description = dbCourse.course_description;
                    pocoCourse.Title = dbCourse.course_title;
                    pocoCourse.CourseLevel = 0;
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }

            return pocoCourse;
        }

        //shouldn't call this method unless we know course exists
        public Course FindCourseById(string _courseName, ref List<string> errors)
        {
            POCO.Course pocoCourse = new POCO.Course();
            course dbCourse;
            try
            {
                //will search primary key
                dbCourse = _context.courses.Find(_courseName);
                if (dbCourse != null)
                {
                    pocoCourse.CourseId = dbCourse.course_id.ToString();
                    pocoCourse.Description = dbCourse.course_description;
                    pocoCourse.Title = dbCourse.course_title;
                    pocoCourse.CourseLevel = 0;
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }

            return pocoCourse;
        }

        //good method for validation when adding new course
        public bool IsDuplicateCourse(POCO.Course _course, ref List<string> errors)
        {
            var dbCourse = new course();

            try
            {
                dbCourse = _context.courses.Find(dbCourse);

                if (dbCourse == null)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }

            return true;

        }

        //Unsure If We need to get course before updating... TODO
        public void UpdateCourse(POCO.Course  _course, ref List<string> errors)
        {
            var dbCourse = new course();

            try
            {
                //might have to retrieve course then update, but I dont think so
                dbCourse.course_level = _course.CourseLevel.ToString();
                dbCourse.course_description = _course.Description;
                dbCourse.course_title = _course.Title;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }

        public void AddCourse(POCO.Course _course, ref List<string> errors)
        {
            var dbCourse = new course();

            try
            {
                dbCourse.course_level = _course.CourseLevel.ToString();
                dbCourse.course_description = _course.Description;
                dbCourse.course_title = _course.Title;
                _context.courses.Add(dbCourse);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }

        public void RemoveCourse(POCO.Course  _course, ref List<string> errors)
        {
            var dbCourse = new course();

            try
            {
                dbCourse.course_level = _course.CourseLevel.ToString();
                dbCourse.course_description = _course.Description;
                dbCourse.course_title = _course.Title;
                _context.courses.Remove(dbCourse);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }

        public List<Course> GetCourseList(ref List<string> errors)
        {
            List<POCO.Course> pocoCourseList = new List<POCO.Course>();
            List<course> dbCourseList;
            try
            {
                dbCourseList = _context.courses.ToList();

                foreach (course i_course in dbCourseList)
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
