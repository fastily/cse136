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
                db_ScheduleList = this.context.course_schedule.Include("enrollments").OrderBy(x => x.year);

                foreach (course_schedule sched in db_ScheduleList)
                {
                    ScheduleMin tempPoco = new ScheduleMin();
                    tempPoco.Year = sched.year.ToString();
                    tempPoco.Quarter = sched.quarter;

                    if (sched.enrollments.Where(x => x.student_id == id).Count() > 0)
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
                db_ScheduleList = this.context.course_schedule.Include("schedule_day").Include("schedule_time").Include("instructor").Where(x => x.quarter == quarter && x.year == myYear);

                foreach (course_schedule c in db_ScheduleList)
                {
                    var day = new schedule_day();
                    var time = new schedule_time();
                    var instructor = new instructor();

                    var schedule = new Schedule
                    {
                        ScheduleId = c.schedule_id,
                        Year = c.year.ToString(),
                        Quarter = c.quarter,
                        Session = c.session,
                        Instructor = new Instructor
                        {
                            InstructorId = c.instructor.instructor_id,
                            FirstName = c.instructor.first_name,
                            LastName = c.instructor.last_name,
                        },
                        Day = new ScheduleDay
                        {
                            DayId = c.schedule_day.schedule_day_id,
                            Day = c.schedule_day.schedule_day1
                        },
                        Time = new ScheduleTime
                        {
                            TimeId = c.schedule_time.schedule_time_id,
                            Time = c.schedule_time.schedule_time1
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
                    Description = db_Schedule.course.course_description,
                    Level = db_Schedule.course.course_level,
                };
            }
            catch (Exception e)
            {
                errors.Add("Error occured in ScheduleRepository.GetScheduleList: " + e);
            }

            return pocoSchedule;
        }

        public void UpdateCourseFromSchedule(Schedule schedule, ref List<string> errors)
        {
            course_schedule db_Schedule = new course_schedule();

            try
            {
                db_Schedule = this.context.course_schedule.Find(schedule.ScheduleId);
                db_Schedule.instructor_id = schedule.Instructor.InstructorId;
                db_Schedule.schedule_day_id = schedule.Day.DayId;
                db_Schedule.schedule_time_id = schedule.Time.TimeId;
                db_Schedule.session = schedule.Session;
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in ScheduleRepository.AddCourseToSchedule: " + e);
            }
        }

        public void AddCourseToSchedule(Schedule schedule, ref List<string> errors)
        {
            course_schedule db_Schedule = new course_schedule();

            try
            {
                db_Schedule.course_id = schedule.Course.CourseId;
                db_Schedule.instructor_id = schedule.Instructor.InstructorId;
                db_Schedule.schedule_day_id = schedule.Day.DayId;
                db_Schedule.schedule_time_id = schedule.Time.TimeId;
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
            IEnumerable<enrollment> deleteEnrollments;

            try
            {
                db_Schedule = this.context.course_schedule.Find(scheduleId);
                db_Schedule = this.context.course_schedule.Remove(db_Schedule);
                deleteEnrollments = this.context.enrollments.Where(x => x.schedule_id == scheduleId);
                foreach (enrollment deleteMe in deleteEnrollments)
                {
                    this.context.enrollments.Remove(deleteMe);
                }

                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in ScheduleRepository.RemoveCourseFromSchedule: " + e);
            }
        }

        public void RemoveWholeSchedule(string year, string quarter, ref List<string> errors)
        {
            IEnumerable<course_schedule> db_ScheduleList;
            IEnumerable<enrollment> deleteEnrollments;

            try
            {
                var Year = int.Parse(year);
                db_ScheduleList = this.context.course_schedule.Where(x => x.quarter == quarter && x.year == Year);
                foreach (course_schedule deleteMe in db_ScheduleList)
                {
                    var deleteMeId = deleteMe.schedule_id;

                    this.context.course_schedule.Remove(deleteMe);
                    deleteEnrollments = this.context.enrollments.Where(x => x.schedule_id == deleteMeId);
                    foreach (enrollment deleteEnrollment in deleteEnrollments)
                    {
                        this.context.enrollments.Remove(deleteEnrollment);
                    }
                }
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

        public List<ScheduleDay> GetDays(ref List<string> errors)
        {
            List<ScheduleDay> pocoList = new List<ScheduleDay>();
            IEnumerable<schedule_day> dbList;

            try
            {
                dbList = this.context.schedule_day;
                
                foreach (schedule_day sd in dbList)
                {
                    var poco = new ScheduleDay();

                    poco.DayId = sd.schedule_day_id;
                    poco.Day = sd.schedule_day1;

                    pocoList.Add(poco);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error occured in ScheduleRepository.GetDays: " + e);
            }

            return pocoList;
        }

        public List<ScheduleTime> GetTimes(ref List<string> errors)
        {
            List<ScheduleTime> pocoList = new List<ScheduleTime>();
            IEnumerable<schedule_time> dbList;

            try
            {
                dbList = this.context.schedule_time;

                foreach (schedule_time sd in dbList)
                {
                    var poco = new ScheduleTime();

                    poco.TimeId = sd.schedule_time_id;
                    poco.Time = sd.schedule_time1;

                    pocoList.Add(poco);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error occured in ScheduleRepository.GetTimes: " + e);
            }

            return pocoList;
        }

        public List<Schedule> GetInstructorSchedule(int instructorId, ref List<string> errors)
        {
            IEnumerable<course_schedule> dbList;
            List<Schedule> pocoList = new List<Schedule>();

            try
            {
                dbList = this.context.course_schedule.Include("schedule_day").Include("schedule_time").Include("instructor").Where(x => x.instructor_id == instructorId);
                 
                foreach (course_schedule c in dbList)
                {
                    var day = new schedule_day();
                    var time = new schedule_time();
                    var instructor = new instructor();

                    var schedule = new Schedule
                    {
                        ScheduleId = c.schedule_id,
                        Year = c.year.ToString(),
                        Quarter = c.quarter,
                        Session = c.session,
                        Instructor = new Instructor
                        {
                            InstructorId = c.instructor.instructor_id,
                            FirstName = c.instructor.first_name,
                            LastName = c.instructor.last_name,
                        },
                        Day = new ScheduleDay
                        {
                            DayId = c.schedule_day.schedule_day_id,
                            Day = c.schedule_day.schedule_day1
                        },
                        Time = new ScheduleTime
                        {
                            TimeId = c.schedule_time.schedule_time_id,
                            Time = c.schedule_time.schedule_time1
                        },
                        Course = new Course
                        {
                            CourseId = c.course.course_id,
                            Title = c.course.course_title,
                            Description = c.course.course_description,
                            Level = c.course.course_level,
                        }
                    };

                    pocoList.Add(schedule);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error occured in ScheduleRepository.GetDays: " + e);
            }

            return pocoList;
        }

        public List<Ta> GetTaBySchedule(int scheduleId, ref List<string> errors)
        {
            IEnumerable<course_schedule> dbList;
            IEnumerable<TeachingAssistant> taList;
            List<Ta> pocoList = new List<Ta>();

            try
            {
                dbList = this.context.course_schedule.Include("TeachingAssistants").Where(x => x.schedule_id == scheduleId);

                foreach (course_schedule c in dbList)
                {
                    taList = c.TeachingAssistants;
                    foreach (TeachingAssistant t in taList)
                    {
                        var poco = new Ta();
                        poco.TaId = t.ta_id;
                        poco.FirstName = t.first;
                        poco.LastName = t.last;
                        poco.TaType = t.ta_type_id.ToString();

                        pocoList.Add(poco);
                    }
                    
                }
            }
            catch (Exception e)
            {
                errors.Add("Error occured in ScheduleRepository.GetDays: " + e);
            }

            return pocoList;
        }
    }
}
