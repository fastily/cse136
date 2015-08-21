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

        public void InsertGradeChange(GradeChange gc, ref List<string> errors)
        {
            if (gc.Course_id == -1)
            {
                errors.Add("Invalid course ID");
                throw new ArgumentException();
            }

            int scheduleid = this.repository.GetEverything(gc.Student_id, gc.Course_id, ref errors);
            if (scheduleid == -1)
            {
                errors.Add("Invalid schedule ID");
                throw new ArgumentException();
            }

            gc.Schedule_id = scheduleid;
            this.repository.RequestGradeChange(gc, ref errors);
        }

        public void RespondToGradeChange(GradeChange gc, ref List<string> errors)
        {
            if (gc.Course_id == -1)
            {
                errors.Add("Invalid course ID");
                throw new ArgumentException();
            }

            this.repository.RespondToGradeChange(gc.GradeChangeId, ref errors);

            if (errors.Count != 0)
            {
                errors.Add("Invalid schedule ID");
                throw new ArgumentException();
            }
        }
    }
}
