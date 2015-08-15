using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCO
{
    public class GradeChange
    {
        public int GradeChangeId { get; set; }
        public int student_id { get; set; }
        public int schedule_id { get; set; }
        public Nullable<bool> approved { get; set; }
        public int course_id { get; set; }
    }
}
