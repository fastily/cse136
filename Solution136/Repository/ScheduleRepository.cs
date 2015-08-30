namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using IRepository;
    using POCO;

    public class ScheduleRepository : BaseRepository, IScheduleRepository
    {
        private const string GetScheduleListProcedure = "spGetScheduleList";
        private cse136Entities context;

        public ScheduleRepository(cse136Entities entities)
        {
            this.context = entities;
        }

        public List<ScheduleMin> GetAllSchedulesMin(ref List<string> errors)
        {
            List<ScheduleMin> pocoScheduleList = new List<ScheduleMin>();
            IEnumerable<course_schedule> db_ScheduleList;

            try
            {
                db_ScheduleList = this.context.course_schedule.OrderBy(x => x.year);

                foreach (course_schedule sched in db_ScheduleList)
                {
                    ScheduleMin tempPoco = new ScheduleMin();
                    tempPoco.Year = sched.year.ToString();
                    tempPoco.Quarter = sched.quarter;

                    pocoScheduleList.Add(tempPoco);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error occured in ScheduleRepository.GetAllSchedules: " + e);
            }

            return pocoScheduleList;
        }

        public List<ScheduleMin> GetStudentScheduleMin(string id, ref List<string> errors)
        {
            List<ScheduleMin> pocoScheduleList = new List<ScheduleMin>();
            IEnumerable<course_schedule> db_ScheduleList;

            try
            {
                db_ScheduleList = this.context.course_schedule.OrderBy(x => x.year);

                foreach (course_schedule sched in db_ScheduleList)
                {
                    ScheduleMin tempPoco = new ScheduleMin();
                    tempPoco.Year = sched.year.ToString();
                    tempPoco.Quarter = sched.quarter;

                    pocoScheduleList.Add(tempPoco);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error occured in ScheduleRepository.GetAllSchedules: " + e);
            }

            return pocoScheduleList;
         }

        public List<Schedule> GetScheduleList(string year, string quarter, ref List<string> errors)
        {
            var scheduleList = new List<Schedule>();
            List<Schedule> pocoScheduleList = new List<Schedule>();
            IEnumerable<course_schedule> db_ScheduleList;

            try
            {
                var myYear = int.Parse(year);
                db_ScheduleList = this.context.course_schedule.Where(x => x.quarter == quarter && x.year == myYear);

                foreach (course_schedule c in db_ScheduleList)
                {
                    var day = new schedule_day();
                    var time = new schedule_time();
                    var instructor = new instructor();
                    day = this.context.schedule_day.Find((int)c.schedule_day_id);
                    time = this.context.schedule_time.Find((int)c.schedule_time_id);
                    instructor = this.context.instructors.Find((int)c.instructor_id);

                    var schedule = new Schedule
                    {
                        ScheduleId = c.schedule_id,
                        Year = c.year.ToString(),
                        Quarter = c.quarter,
                        Session = c.session,
                        Instructor = new Instructor
                        {
                            InstructorId = instructor.instructor_id,
                            FirstName = instructor.first_name,
                            LastName = instructor.last_name,
                        },
                        Day = new ScheduleDay
                        {
                            DayId = day.schedule_day_id,
                            Day = day.schedule_day1
                        },
                        Time = new ScheduleTime 
                        {
                            TimeId = time.schedule_time_id,
                            Time = time.schedule_time1
                        },
                        Course = new Course
                        {
                            CourseId = c.course.course_id,
                            Title = c.course.course_title,
                            Description = c.course.course_description
                        }
                    };

                    scheduleList.Add(schedule);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error occured in ScheduleRepository.GetScheduleList: " + e);
            }

            return scheduleList;
        }

        public Schedule GetScheduleById(int scheduleId, ref List<string> errors)
        {
            Schedule pocoSchedule = new Schedule();
            course_schedule db_Schedule;

            try
            {
                db_Schedule = this.context.course_schedule.Find(scheduleId);

                var day = new schedule_day();
                var time = new schedule_time();
                var instructor = new instructor();
                day = this.context.schedule_day.Find((int)db_Schedule.schedule_day_id);
                time = this.context.schedule_time.Find((int)db_Schedule.schedule_time_id);
                instructor = this.context.instructors.Find((int)db_Schedule.instructor_id);

                pocoSchedule.ScheduleId = db_Schedule.schedule_id;
                pocoSchedule.Year = db_Schedule.year.ToString();
                pocoSchedule.Quarter = db_Schedule.quarter;
                pocoSchedule.Session = db_Schedule.session;
                pocoSchedule.Instructor = new Instructor
                {
                    InstructorId = instructor.instructor_id,
                    FirstName = instructor.first_name,
                    LastName = instructor.last_name,
                };
                pocoSchedule.Day = new ScheduleDay
                {
                    DayId = day.schedule_day_id,
                    Day = day.schedule_day1
                };
                pocoSchedule.Time = new ScheduleTime
                {
                    TimeId = time.schedule_time_id,
                    Time = time.schedule_time1
                };
                pocoSchedule.Course = new Course
                {
                    CourseId = db_Schedule.course.course_id,
                    Title = db_Schedule.course.course_title,
                    Description = db_Schedule.course.course_description
                };
            }            
            catch (Exception e)
            {
                errors.Add("Error occured in ScheduleRepository.GetScheduleList: " + e);
            }

            return pocoSchedule;
        }

        public void AddCourseToSchedule(Schedule schedule, int instructorId, int dayId, int timeId, ref List<string> errors)
        {
            course_schedule db_Schedule = new course_schedule();

            try
            {
                db_Schedule.course_id = schedule.Course.CourseId;
                db_Schedule.instructor_id = instructorId;
                db_Schedule.schedule_day_id = dayId;
                db_Schedule.schedule_time_id = timeId;
                db_Schedule.year = int.Parse(schedule.Year);
                db_Schedule.quarter = schedule.Quarter;
                db_Schedule.session = schedule.Session;
                this.context.course_schedule.Add(db_Schedule);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in ScheduleRepository.AddCourseToSchedule: " + e);
            }
        }

        public void RemoveCourseFromSchedule(int scheduleId, ref List<string> errors)
        {
            var db_Schedule = new course_schedule();

            try
            {
                db_Schedule.schedule_id = scheduleId;
                db_Schedule = this.context.course_schedule.Remove(db_Schedule);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in ScheduleRepository.RemoveCourseFromSchedule: " + e);
            }
        }

        public bool IsNotDuplicateCourseFromSchedule(int year, int courseId, string quarter, ref List<string> errors)
        {
            var isDuplicate = false;

            try
            {
                isDuplicate = this.context.course_schedule.Where(x => x.course_id == courseId && x.year == year && x.quarter == quarter).Count() > 0;
                if (isDuplicate)
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                errors.Add("Error occured in ScheduleRepository.IsDuplicateCourseToSchedule: " + e);
            }

            return isDuplicate;
        }
    }
}
