namespace IRepository
{
    using System.Collections.Generic;
    using POCO;

    public interface IGradeChangeRepository
    {
        void RequestGradeChange(GradeChange gradeChange, ref List<string> errors);

        void RespondToGradeChange(int gradeChangeId, ref List<string> errors);

        ////find gradeChangeBySomeSortOfId

        int GetEverything(string student_id, int course_id, ref List<string> errors);
    }
}
