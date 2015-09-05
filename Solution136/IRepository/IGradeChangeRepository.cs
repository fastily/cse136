namespace IRepository
{
    using System.Collections.Generic;
    using POCO;

    public interface IGradeChangeRepository
    {
        GradeChange FindGradeChangeByStudentId(string student_id, ref List<string> errors);

        GradeChange FindGradeChangeByCourseId(int course_id, ref List<string> errors);
       
        void AddGradeChange(GradeChange gradeChange, ref List<string> errors);

        void ApproveGradeChange(int gradeChangeId, ref List<string> errors);

        int GetGradeChangeScheduleId(string student_id, int course_id, ref List<string> errors);
    }
}
