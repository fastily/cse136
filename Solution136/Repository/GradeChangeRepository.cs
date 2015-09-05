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

        public GradeChange FindGradeChangeByStudentId(string student_id, ref List<string> errors)
        {
            POCO.GradeChange pocoGradeChange = new POCO.GradeChange();
            grade_change db_GradeChange;
            try
            {
                db_GradeChange = this.context.grade_change.Find(student_id);
                if (db_GradeChange != null)
                {
                    pocoGradeChange.GradeChangeId = db_GradeChange.gradeChange_id;
                    pocoGradeChange.Student_id = db_GradeChange.student_id;
                    pocoGradeChange.Schedule_id = db_GradeChange.schedule_id;
                    pocoGradeChange.Approved = db_GradeChange.approved;
                    pocoGradeChange.Course_id = db_GradeChange.course_id;
                    pocoGradeChange.Description = db_GradeChange.description;
                }
            }
            catch (Exception e)
            {
                errors.Add("Error occured in GradeChangeRepository.FindGradeChangeById: " + e);
            }

            return pocoGradeChange;
        }

        public GradeChange FindGradeChangeByCourseId(int course_id, ref List<string> errors)
        {
            POCO.GradeChange pocoGradeChange = new POCO.GradeChange();
            grade_change db_GradeChange;
            try
            {
                db_GradeChange = this.context.grade_change.Find(course_id);
                if (db_GradeChange != null)
                {
                    pocoGradeChange.GradeChangeId = db_GradeChange.gradeChange_id;
                    pocoGradeChange.Student_id = db_GradeChange.student_id;
                    pocoGradeChange.Schedule_id = db_GradeChange.schedule_id;
                    pocoGradeChange.Approved = db_GradeChange.approved;
                    pocoGradeChange.Course_id = db_GradeChange.course_id;
                    pocoGradeChange.Description = db_GradeChange.description;
                }
            }
            catch (Exception e)
            {
                errors.Add("Error occured in GradeChangeRepository.FindGradeChangeById: " + e);
            }

            return pocoGradeChange;
        }

        public void AddGradeChange(GradeChange gradeChange, ref List<string> errors)
        {
            grade_change db_gradeChange = new grade_change();

            try
            {
                db_gradeChange.student_id = gradeChange.Student_id;
                db_gradeChange.schedule_id = gradeChange.Schedule_id;
                db_gradeChange.course_id = gradeChange.Course_id;
                db_gradeChange.approved = gradeChange.Approved;
                db_gradeChange.description = gradeChange.Description;

                this.context.grade_change.Add(db_gradeChange);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in GradeChangeRepository.RequestGradeChange: " + e);
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
                errors.Add("Error occured in GradeChangeRepository.ApproveGradeChange: " + e);
            }
        }
    
        public int GetGradeChangeScheduleId(string student_id, int course_id, ref List<string> errors)
        {
            grade_change db_gradeChange = new grade_change();

            IEnumerable<int> scheduleIDs = this.context.enrollments.Where(x => x.student_id == student_id).Select(y => y.schedule_id);

            bool temp;
           
            foreach (int s in scheduleIDs)
            {
                temp = this.context.course_schedule.Where(x => x.schedule_id == s && x.course_id == course_id).Count() > 0;
                if (temp)
                {
                    return s;
                }
            }

            return -1;
        }
    }
}
