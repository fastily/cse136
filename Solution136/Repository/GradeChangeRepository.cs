﻿namespace Repository
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
                errors.Add("Error occured in GradeChangeRepository.RequestGradeChange: " + e);
            }
        }

        public void RespondToGradeChange(int gradeChangeId, ref List<string> errors)
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
    
        public int GetEverything(string student_id, int course_id, ref List<string> errors)
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
