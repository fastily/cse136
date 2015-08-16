namespace IRepository
{
    using System.Collections.Generic;
    using POCO;

    public interface IGradeChangeRepository
    {
        void RequestGradeChange(GradeChange gradeChange, ref List<string> errors);

        void ApproveGradeChange(int gradeChangeId, ref List<string> errors);

        ////find gradeChangeBySomeSortOfId
    }
}
