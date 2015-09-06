namespace Service
{
    using System;
    using System.Collections.Generic;
    using IRepository;
    using POCO;

    public class EnrollmentService
    {
        private readonly IEnrollmentRepository repository;

        public EnrollmentService(IEnrollmentRepository repository)
        {
            this.repository = repository;
        }

        public void AddEnrollment(string studentId, int scheduleId, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(studentId))
            {
                errors.Add("Invalid studentId");
                throw new ArgumentException();
            }

            if (scheduleId <= 0)
            {
                errors.Add("Invalid schedule Id");
                throw new ArgumentException();
            }

            if (this.repository.IsNotDuplicateEnrollment(studentId, scheduleId, ref errors))
            {
                this.repository.AddEnrollment(studentId, scheduleId, ref errors);
            }
            else
            {
                errors.Add("Enrollment Already Exists");
                throw new ArgumentException();
            }
        }

        public void RemoveEnrollment(string studentId, int scheduleId, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(studentId))
            {
                errors.Add("Invalid studentId");
                throw new ArgumentException();
            }

            if (scheduleId <= 0)
            {
                errors.Add("Invalid schedule Id");
                throw new ArgumentException();
            }

            this.repository.RemoveEnrollment(studentId, scheduleId, ref errors);
        }

        public List<Enrollment> GetAllStudentEnrolledSchedules(string studentId, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(studentId))
            {
                errors.Add("Invalid studentId");
                throw new ArgumentException();
            }

            return this.repository.GetAllStudentEnrolledSchedules(studentId, ref errors);
        }

        public List<Enrollment> GetStudentEnrolledSchedulesByQuarter(string studentId, string year, string quarter, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(studentId))
            {
                errors.Add("Invalid studentId");
                throw new ArgumentException();
            }

            return this.repository.GetStudentEnrolledSchedulesByQuarter(studentId, year, quarter, ref errors);
        }

        public List<Student> GetStudentsByScheduleId(int scheduleId, ref List<string> errors)
        {
            if (scheduleId <= 0)
            {
                errors.Add("Invalid scheduleId");
                throw new ArgumentException();
            }

            return this.repository.GetStudentsByScheduleId(scheduleId, ref errors);
        }
    }
}
