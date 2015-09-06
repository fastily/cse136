namespace Service
{
    using System;
    using System.Collections.Generic;
    using IRepository;
    using POCO;

    public class GradeChangeService
    {
        private readonly IGradeChangeRepository repository;

        public GradeChangeService(IGradeChangeRepository repository)
        {
            this.repository = repository;
        }

        public void AddGradeChange(GradeChange gc, ref List<string> errors)
        {
            if (gc.Course_id == -1)
            {
                errors.Add("Invalid course ID");
                throw new ArgumentException();
            }

            if (gc.Schedule_id == -1)
            {
                errors.Add("Invalid schedule ID");
                throw new ArgumentException();
            }

            this.repository.AddGradeChange(gc, ref errors);
        }

        public void ApproveGradeChange(GradeChange gc, ref List<string> errors)
        {
            if (gc.Course_id == -1)
            {
                errors.Add("Invalid course ID");
                throw new ArgumentException();
            }

            this.repository.ApproveGradeChange(gc.GradeChangeId, ref errors);

            if (errors.Count != 0)
            {
                errors.Add("Invalid schedule ID");
                throw new ArgumentException();
            }
        }

        public GradeChange FindGradeChangeByStudentId(string student_id, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(student_id))
            {
                errors.Add("Invalid student_id");
                throw new ArgumentException();
            }

            return this.repository.FindGradeChangeByStudentId(student_id, ref errors);
        }

        public GradeChange FindGradeChangeByCourseId(int course_id, ref List<string> errors)
        {
            if (course_id <= 0)
            {
                errors.Add("Invalid course_id");
                throw new ArgumentException();
            }

            return this.repository.FindGradeChangeByCourseId(course_id, ref errors);
        }

        public int GetGradeChangeScheduleId(string student_id, int course_id, ref List<string> errors)
        {
            if (course_id <= 0 || string.IsNullOrEmpty(student_id))
            {
                errors.Add("Invalid id");
                throw new ArgumentException();
            }

            return this.repository.GetGradeChangeScheduleId(student_id, course_id, ref errors);
        }
    }
}
