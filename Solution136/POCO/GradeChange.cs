namespace POCO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GradeChange
    {
        public int GradeChangeId { get; set; }

        public string Student_id { get; set; }

        public int Schedule_id { get; set; }

        public bool? Approved { get; set; }

        public int Course_id { get; set; }

        public string Desired { get; set; }

        public string Description { get; set; }

    }
}
