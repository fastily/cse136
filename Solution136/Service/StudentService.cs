namespace Service
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using IRepository;
    using POCO;

    public class StudentService
    {
        private readonly IStudentRepository repository;

        public StudentService(IStudentRepository repository)
        {
            this.repository = repository;
        }

        public void InsertStudent(Student student, ref List<string> errors)
        {
            if (student == null)
            {
                errors.Add("Student cannot be null");
                throw new ArgumentException();
            }

            if (student.StudentId.Length < 5)
            {
                errors.Add("Invalid student ID");
                throw new ArgumentException();
            }

            if (!Regex.IsMatch(student.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
            {
                errors.Add("Invalid student email");
                throw new ArgumentException();
            }

            this.repository.InsertStudent(student, ref errors);
        }

        public void UpdateStudent(Student student, ref List<string> errors)
        {
            if (student == null)
            {
                errors.Add("Student cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(student.StudentId))
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            if (student.StudentId.Length < 5)
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            if (!Regex.IsMatch(student.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
            {
                errors.Add("Invalid student email");
                throw new ArgumentException();
            }

            this.repository.UpdateStudent(student, ref errors);
        }

        public Student GetStudent(string id, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(id))
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            return this.repository.GetStudentDetail(id, ref errors);
        }

        public void DeleteStudent(string id, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(id))
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            this.repository.DeleteStudent(id, ref errors);
        }

        public List<Student> GetStudentList(ref List<string> errors)
        {
            return this.repository.GetStudentList(ref errors);
        }

        public void EnrollSchedule(string studentId, int scheduleId, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(studentId) || scheduleId < 0)
            {
                errors.Add("Invalid student id or schedule id");
                throw new ArgumentException();
            }

            if (this.repository.GetEnrollments(studentId, ref errors).Count < 4)
            {
                this.repository.EnrollSchedule(studentId, scheduleId, ref errors);
            }
            else
            {
                errors.Add("Enrolled in to many courses!");
                throw new ArgumentException();
            }
        }

        public void DropEnrolledSchedule(string studentId, int scheduleId, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(studentId) || scheduleId < 0)
            {
                errors.Add("Invalid student id or schedule id");
                throw new ArgumentException();
            }

            this.repository.DropEnrolledSchedule(studentId, scheduleId, ref errors);
        }

        public List<Enrollment> GetEnrollments(string studentId, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(studentId))
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            return this.repository.GetEnrollments(studentId, ref errors);
        }

        public float CalculateGpa(string studentId, ref List<string> errors)
        {
            List<Enrollment> enrollments = GetEnrollments(studentId, ref errors);

            if (string.IsNullOrEmpty(studentId))
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();
            }

            if (enrollments == null)
            {
                errors.Add("Invalid student id");
                throw new ArgumentException();                
            }

            if (enrollments.Count == 0)
            {
                return 0.0f;
            }

            var sum = 0.0f;
            var pnp = 0;

            foreach (var enrollment in enrollments)
            {
                switch (enrollment.Grade) 
                { 
                    case "ap":
                        sum += 4.0f;
                        break;
                    case "a":
                        sum += 4.0f;
                        break;
                    case "am":
                        sum += 3.7f;
                        break;
                    case "bp":
                        sum += 3.3f;
                        break;
                    case "b":
                        sum += 3.0f;
                        break;
                    case "bm":
                        sum += 2.7f;
                        break;
                    case "cp":
                        sum += 2.3f;
                        break;
                    case "c":
                        sum += 2.0f;
                        break;
                    case "cm":
                        sum += 1.7f;
                        break;
                    case "dp":
                        sum += 1.3f;
                        break;
                    case "d":
                        sum += 1.0f;
                        break;
                    case "dm":
                        sum += 0.7f;
                        break;
                    case "f":
                        sum += 0f;
                        break;
                    case "p":
                        pnp += 1;
                        break;
                    case "np":
                        pnp += 1;
                        break;
                }
            }

            return (sum / (enrollments.Count - pnp));
        }

        public void RequestPreReqOverride(int scheduleId, string studentId, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(studentId) || scheduleId < 0)
            {
                errors.Add("Invalid student id or schedule id");
                throw new ArgumentException();
            }

            this.repository.RequestPreReqOverride(scheduleId, studentId, ref errors);
        }
    }
}
