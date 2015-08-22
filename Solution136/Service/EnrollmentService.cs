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

            if (string.IsNullOrEmpty(scheduleId.ToString()))
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

            if (string.IsNullOrEmpty(scheduleId.ToString()))
            {
                errors.Add("Invalid schedule Id");
                throw new ArgumentException();
            }

            this.repository.RemoveEnrollment(studentId, scheduleId, ref errors);
        }
    }
}
