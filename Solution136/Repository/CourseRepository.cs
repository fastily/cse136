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
                errors.Add("Error occured in CourseRepository.FindCourseByName: " + e);
            }

            return pocoCourse;
        }

        public Course FindCourseById(string courseId, ref List<string> errors)
        {
            POCO.Course pocoCourse = new POCO.Course();
            course db_course;
            try
            {
                ////will search primary key
                db_course = this.context.courses.Find(courseId);
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
                errors.Add("Error occured in CourseRepository.FindCourseById: " + e);
            }

            return pocoCourse;
        }

        ////good method for validation when adding new course
        public bool IsDuplicateCourse(POCO.Course c, ref List<string> errors)
        {
            var db_course = new course();

            try
            {
                db_course.course_title = c.Title;
                db_course.course_description = c.Description;
                db_course.course_level = c.CourseLevel.ToString();
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
                errors.Add("Error occured in CourseRepository.IsDuplicateCourse: " + e);
            }

            return true;
        }

        public void UpdateCourse(POCO.Course c, ref List<string> errors)
        {
            var db_course = new course();

            try
            {
                db_course.course_id = int.Parse(c.CourseId);
                db_course = this.context.courses.Find(db_course);
                db_course.course_level = c.CourseLevel.ToString();
                db_course.course_description = c.Description;
                db_course.course_title = c.Title;
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in CourseRepository.UpdateCourse: " + e);
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
                errors.Add("Error occured in CourseRepository.AddCourse: " + e);
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
                errors.Add("Error occured in CourseRepository.RemoveCourse: " + e);
            }
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
                errors.Add("Error occured in CourseRepository.GetCourseList: " + e);
            }

            return pocoCourseList;
        }

        public void AssignPreReqToCourse(int courseId, int preReqCourseId, ref List<string> errors)
        {
            course db_coursePreReq = new course();
            course_preReq db_preReq = new course_preReq();
            try
            {
                db_coursePreReq.course_id = preReqCourseId;
                db_coursePreReq = this.context.courses.Find(db_coursePreReq);

                db_preReq.course_id = courseId;
                db_preReq.preReq_id = preReqCourseId;
                db_preReq.preReq_title = db_coursePreReq.course_title;

                this.context.course_preReq.Add(db_preReq);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in CourseRepository.AssignPreReqToCourse: " + e);
            }
        }

        public void RemovePreReqFromCourse(int courseId, int preReqToRemoveCourseId, ref List<string> errors)
        {
            course db_coursePreReq = new course();
            course_preReq db_preReq = new course_preReq();
            try
            {
                db_coursePreReq.course_id = preReqToRemoveCourseId;
                db_coursePreReq = this.context.courses.Find(db_coursePreReq);

                db_preReq.course_id = courseId;
                db_preReq.preReq_id = preReqToRemoveCourseId;
                db_preReq.preReq_title = db_coursePreReq.course_title;

                this.context.course_preReq.Remove(db_preReq);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in in CourseRepository.RemovePreReqFromCourse: " + e);
            }
        }

        public List<Course> GetAllPreReqs(int courseId, ref List<string> errors)
        {
            List<POCO.Course> pocoCourseList = new List<POCO.Course>();
            course db_course = new course();
            List<course_preReq> db_preReqCourseList;
            try
            {
                db_preReqCourseList = this.context.course_preReq.ToList();

                foreach (course_preReq preReq in db_preReqCourseList)
                {
                    var tempPoco = new POCO.Course();
                    tempPoco.CourseId = preReq.preReq_id.ToString();
                    tempPoco.Title = preReq.preReq_title;
                    pocoCourseList.Add(tempPoco);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error occured in CourseRepository.GetAllPreReqs: " + e);
            }

            return pocoCourseList;
        }
    }
}
