namespace Service
{
    using System;
    using System.Collections.Generic;
    using IRepository;
    using POCO;

    public class InstructorService
    {
        private readonly IInstructorRepository repository;

        public InstructorService(IInstructorRepository repository)
        {
            this.repository = repository;
        }

        public void InsertInstructor(Instructor instructor, ref List<string> errors)
        {
            if (instructor == null)
            {
                errors.Add("Instructor cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(instructor.FirstName))
            {
                errors.Add("Instructor first name cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(instructor.LastName))
            {
                errors.Add("Instructor last name cannot be null");
                throw new ArgumentException();
            }

            if (this.repository.IsNotDuplicateInstructor(instructor, ref errors))
            {
                this.repository.AddInstructor(instructor, ref errors);
            }
        }

        public void UpdateInstructor(Instructor instructor, ref List<string> errors)
        {
            if (instructor == null)
            {
                errors.Add("Instructor cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(instructor.FirstName))
            {
                errors.Add("Instructor first name cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(instructor.LastName))
            {
                errors.Add("Instructor last name cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(instructor.InstructorId.ToString()))
            {
                errors.Add("Instructor id be null");
                throw new ArgumentException();
            }

            this.repository.AddInstructor(instructor, ref errors);
        }

        public Instructor GetInstructor(string instructor_id, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(instructor_id))
            {
                errors.Add("Invalid instructor_id");
                throw new ArgumentException();
            }

            return this.repository.FindInstructorById(int.Parse(instructor_id), ref errors);
        }

        public void DeleteInstructor(string instructor_id, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(instructor_id))
            {
                errors.Add("Invalid ta_id");
                throw new ArgumentException();
            }

            this.repository.RemoveInstructor(int.Parse(instructor_id), ref errors);
        }

        public List<Instructor> GetInstructorList(ref List<string> errors)
        {
            return this.repository.GetInstructorList(ref errors);
        }

        public void AssignGrade(Schedule schedule, string studentId, int instructorId, string grade, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(instructorId.ToString()))
            {
                errors.Add("Invalid instructorId");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(grade))
            {
                errors.Add("Must assign a grade");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(schedule.ScheduleId.ToString()))
            {
                errors.Add("Invalid schedule");
                throw new ArgumentException();
            }

            this.repository.AssignGradeToStudent(schedule, studentId, instructorId, grade, ref errors);
        }

        public void RespondToPreReqOverride(int scheduleId, string studentId, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(studentId))
            {
                errors.Add("Invalid studentId");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(scheduleId.ToString()))
            {
                errors.Add("Invalid schedule");
                throw new ArgumentException();
            }

            this.repository.ApprovePreReqOverride(scheduleId, studentId, ref errors);
        }
    }
}
