namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using IRepository;
    using POCO;

    public class EnrollmentRepository : IEnrollmentRepository
    {
        private cse136Entities context;

        public EnrollmentRepository(cse136Entities entities)
        {
            this.context = entities;
        }

        public void AddEnrollment(int studentId, string year, string quarter, string session, Course Course, ref List<string> errors);
        {
            var db_Enrollment = new enrollment();

            try
            {
                db_Enrollment.student_id = studentId; 

                db_Enrollment.schedule_id = this.context.course_schedule.Where(
                    y => y.quarter == schedule.Quarter && y.year == Int32.Parse(schedule.Year) &&
                    y.instructor_id == InstructorId).Select(x => x.schedule_id).First();
                db_Enrollment.grade = "";
                this.context.enrollment.Add(db_Enrollment);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error at AddEnrollment: " + e);
            }
        }


        public void RemoveEnrolement (int studentId, int ScheduleId, ref List<string> errors);
        {
        }
    }
}
