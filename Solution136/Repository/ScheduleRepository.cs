namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

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

        public List<Schedule> GetScheduleList(string year, string quarter, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            var scheduleList = new List<Schedule>();

            try
            {
                var adapter = new SqlDataAdapter(GetScheduleListProcedure, conn);

                if (year.Length > 0)
                {
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@year", SqlDbType.Int));
                    adapter.SelectCommand.Parameters["@year"].Value = year;
                }

                if (quarter.Length > 0)
                {
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@quarter", SqlDbType.VarChar, 25));
                    adapter.SelectCommand.Parameters["@quarter"].Value = quarter;
                }

                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    var schedule = new Schedule
                    {
                        ScheduleId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["schedule_id"].ToString()), 
                        Year = dataSet.Tables[0].Rows[i]["year"].ToString(), 
                        Quarter = dataSet.Tables[0].Rows[i]["quarter"].ToString(), 
                        Session = dataSet.Tables[0].Rows[i]["session"].ToString(), 
                        Course = new Course
                        {
                            CourseId = dataSet.Tables[0].Rows[i]["course_id"].ToString(), 
                            Title = dataSet.Tables[0].Rows[i]["course_title"].ToString(), 
                            Description = dataSet.Tables[0].Rows[i]["course_description"].ToString(), 
                        }
                    };
                    scheduleList.Add(schedule);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
            finally
            {
                conn.Dispose();
            }

            return scheduleList;
        }

        public void AddCourseToSchedule(Schedule schedule, int instructorId, int dayId, int timeId, ref List<string> errors)

        {
            course_schedule db_Schedule = new course_schedule();

            try
            {
                db_Schedule.course_id = Int32.Parse(schedule.Course.CourseId);
                db_Schedule.instructor_id = instructorId;
                db_Schedule.schedule_day_id = dayId;
                db_Schedule.schedule_time_id = timeId;
                db_Schedule.year = Int32.Parse(schedule.Year);
                db_Schedule.quarter = schedule.Quarter;
                db_Schedule.session = schedule.Session;
                this.context.course_schedule.Add(db_Schedule);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }

        public void RemoveCourseFromSchedule(string year, int courseId, string quarter, ref List<string> errors)
        {
            try
            {

            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }

        public void IsDuplicateCourseFromSchedule(string year, int courseId, string quarter, ref List<string> errors)
        {
            try
            {

            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }
    }
}
