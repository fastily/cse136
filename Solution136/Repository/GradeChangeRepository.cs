namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using IRepository;
    using POCO;

    public class GradeChangeRepository : IGradeChangeRepository
    {
        private cse136Entities context;

        public GradeChangeRepository(cse136Entities entities)
        {
            this.context = entities;
        }

        public void RequestGradeChange(GradeChange gradeChange, ref List<string> errors)
        {
            grade_change db_gradeChange = new grade_change();

            try
            {
                db_gradeChange.course_id = gradeChange.Course_id;
                db_gradeChange.schedule_id = gradeChange.Schedule_id;
                db_gradeChange.student_id = gradeChange.Student_id;
                this.context.grade_change.Add(db_gradeChange);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }

        public void ApproveGradeChange(int gradeChangeId, ref List<string> errors)
        {
            grade_change db_gradeChange = new grade_change();

            try
            {
                db_gradeChange.gradeChange_id = gradeChangeId;
                db_gradeChange = this.context.grade_change.Find(db_gradeChange);
                db_gradeChange.approved = true;
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }
    }
}
