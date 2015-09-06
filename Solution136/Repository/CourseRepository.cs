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
        public List<Course> FindCourseByName(string courseName, ref List<string> errors)
        {
            List<Course> searchResults = new List<Course>();
            IEnumerable<course> db_courses;

            try
            {
                db_courses = this.context.courses.Where(x => x.course_title.Contains(courseName));

                foreach (course db_course in db_courses)
                {
                    POCO.Course pocoCourse = new POCO.Course();

                    pocoCourse.CourseId = db_course.course_id;
                    pocoCourse.Description = db_course.course_description;
                    pocoCourse.Title = db_course.course_title;
                    pocoCourse.CourseLevel = (CourseLevel)Enum.Parse(typeof(CourseLevel), db_course.course_level);
                    pocoCourse.Level = db_course.course_level;
                    searchResults.Add(pocoCourse);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error occured in CourseRepository.FindCourseByName: " + e);
            }

            return searchResults;
        }

        public Course FindCourseById(int courseId, ref List<string> errors)
        {
            POCO.Course pocoCourse = new POCO.Course();
            course db_course;
            try
            {
                ////will search primary key
                db_course = this.context.courses.Find(courseId);
                if (db_course != null)
                {
                    pocoCourse.CourseId = db_course.course_id;
                    pocoCourse.Description = db_course.course_description;
                    pocoCourse.Title = db_course.course_title;
                    pocoCourse.CourseLevel = (CourseLevel)Enum.Parse(typeof(CourseLevel), db_course.course_level);
                    pocoCourse.Level = db_course.course_level;
                }
            }
            catch (Exception e)
            {
                errors.Add("Error occured in CourseRepository.FindCourseById: " + e);
            }

            return pocoCourse;
        }

        ////good method for validation when adding new course
        public bool IsNotDuplicateCourse(POCO.Course c, ref List<string> errors)
        {
            try
            {
                var isDuplicate = this.context.courses.Where(
                    x => x.course_description == c.Description &&
                    x.course_level == c.CourseLevel.ToString() &&
                    x.course_title == c.Title).Count() > 0;

                if (isDuplicate)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                errors.Add("Error occured in CourseRepository.IsDuplicateCourse: " + e);
            }

            return false;
        }

        public void UpdateCourse(POCO.Course c, ref List<string> errors)
        {
            var db_course = new course();

            try
            {
                db_course = this.context.courses.Find(c.CourseId);
                db_course.course_level = c.Level;
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
                db_course.course_level = c.Level;
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
                db_course = this.context.courses.Find(course_id);
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
            IEnumerable<course> db_courseList;
            try
            {
                db_courseList = this.context.courses;

                foreach (course i_course in db_courseList)
                {
                    var tempPoco = new POCO.Course();
                    tempPoco.CourseId = i_course.course_id;
                    tempPoco.CourseLevel = (CourseLevel)Enum.Parse(typeof(CourseLevel), i_course.course_level);
                    tempPoco.Level = i_course.course_level;
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

        public List<Course> GetAllScheduleCourseList(ref List<string> errors)
        {
            List<POCO.Course> pocoCourseList = new List<POCO.Course>();
            IEnumerable<course> db_courseList;
            try
            {
                db_courseList = this.context.courses.Include("course_schedule");

                foreach (course i_course in db_courseList)
                {
                    var tempPoco = new POCO.Course();
                    if (i_course.course_schedule.Count() > 0)
                    {
                        tempPoco.CourseId = i_course.course_id;
                        tempPoco.ScheduleId = i_course.course_schedule.First().schedule_id;
                        tempPoco.CourseLevel = (CourseLevel)Enum.Parse(typeof(CourseLevel), i_course.course_level);
                        tempPoco.Level = i_course.course_level;
                        tempPoco.Description = i_course.course_description;
                        tempPoco.Title = i_course.course_title;
                        pocoCourseList.Add(tempPoco);
                    }
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
                db_coursePreReq = this.context.courses.Find(preReqCourseId);
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
            course_preReq db_preReq = new course_preReq();
            try
            {
                db_preReq = this.context.course_preReq.Where(x => x.course_id == courseId && x.preReq_id == preReqToRemoveCourseId).First();
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
            IEnumerable<course_preReq> db_preReqCourseList;
            try
            {
                db_preReqCourseList = this.context.course_preReq.Where(x => x.course_id == courseId);

                foreach (course_preReq preReq in db_preReqCourseList)
                {
                    var tempPoco = new POCO.Course();
                    var db_Poco = new course();
                    db_Poco = this.context.courses.Find(preReq.preReq_id);
                    tempPoco.CourseId = db_Poco.course_id;
                    tempPoco.Title = db_Poco.course_title;
                    tempPoco.Description = db_Poco.course_description;
                    tempPoco.Level = db_Poco.course_level;
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
