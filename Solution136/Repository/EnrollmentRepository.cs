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

        public void AddEnrollment(string studentId, int scheduleId, ref List<string> errors)
        {
            var db_Enrollment = new enrollment();

            try
            {
                db_Enrollment.student_id = studentId;
                db_Enrollment.schedule_id = scheduleId;
                ////db_Enrollment.schedule_id = this.context.course_schedule.Where(
                ////    y => y.quarter == quarter && y.year == int.Parse(year)).Select(x => x.schedule_id).First();
                db_Enrollment.grade = string.Empty;
                this.context.enrollments.Add(db_Enrollment);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in EnrollmentRepository.AddEnrollment: " + e);
            }
        }

        public void RemoveEnrollment(string studentId, int scheduleId, ref List<string> errors)
        {
            var db_enrollment = new enrollment();

            try
            {
                db_enrollment.student_id = studentId;
                db_enrollment.schedule_id = scheduleId;
                this.context.enrollments.Remove(db_enrollment);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }

        public bool IsNotDuplicateEnrollment(string studentId, int scheduleId, ref List<string> errors)
        {
            var isDuplicate = true;

            try
            {
                isDuplicate = this.context.enrollments.Where(x => x.student_id == studentId && x.schedule_id == scheduleId).Count() > 0;
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
