using System.Collections.Generic;
using POCO;

namespace IRepository
{
    interface IGradeChangeRepository
    {
        void RequestGradeChange(GradeChange _GradeChange, ref List<string> errors);
        void ApproveGradeChange(GradeChange _GradeChange, ref List<string> errors);
    }
}
