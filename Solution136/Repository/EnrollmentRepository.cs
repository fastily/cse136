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
                db_enrollment = this.context.enrollments.Find(studentId, scheduleId);
                this.context.enrollments.Remove(db_enrollment);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }

        public List<POCO.Enrollment> GetAllStudentEnrolledSchedules(string studentId, ref List<string> errors)
        {
            IEnumerable<enrollment> studentEnrollments;
            List<POCO.Enrollment> pocoEnrollmentList = new List<POCO.Enrollment>();

            try
            {
                studentEnrollments = this.context.enrollments.Include("course_schedule").Where(x => x.student_id == studentId);
                foreach (enrollment enrolled in studentEnrollments)
                {
                    var poco = new POCO.Enrollment();

                    poco.ScheduleId = enrolled.schedule_id;
                    poco.StudentId = enrolled.student_id;
                    poco.Grade = enrolled.grade;
                    poco.EnrolledSchedule.ScheduleId = enrolled.course_schedule.schedule_id;
                    poco.EnrolledSchedule.Year = enrolled.course_schedule.year.ToString();
                    poco.EnrolledSchedule.Quarter = enrolled.course_schedule.quarter;
                    poco.EnrolledSchedule.Session = enrolled.course_schedule.session;

                    pocoEnrollmentList.Add(poco);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }

            return pocoEnrollmentList;
        }

        public List<POCO.Enrollment> GetStudentEnrolledSchedulesByQuarter(string studentId, string year, string quarter, ref List<string> errors)
        {
            IEnumerable<enrollment> studentEnrollments;
            List<POCO.Enrollment> pocoEnrollmentList = new List<POCO.Enrollment>();

            try
            {
                studentEnrollments = this.context.enrollments.Include("course_schedule").Where(x => x.student_id == studentId);
                foreach (enrollment enrolled in studentEnrollments)
                {
                    var poco = new POCO.Enrollment();

                    poco.ScheduleId = enrolled.schedule_id;
                    poco.StudentId = enrolled.student_id;
                    poco.Grade = enrolled.grade;
                    poco.EnrolledSchedule.ScheduleId = enrolled.course_schedule.schedule_id;
                    poco.EnrolledSchedule.Year = enrolled.course_schedule.year.ToString();
                    poco.EnrolledSchedule.Quarter = enrolled.course_schedule.quarter;
                    poco.EnrolledSchedule.Session = enrolled.course_schedule.session;

                    if (enrolled.course_schedule.year.ToString() == year && enrolled.course_schedule.quarter == quarter)
                        pocoEnrollmentList.Add(poco);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }

            return pocoEnrollmentList;
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
