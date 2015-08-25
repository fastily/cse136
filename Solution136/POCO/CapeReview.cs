namespace POCO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CapeReview
    {
        public int CapeId { get; set; }

        public int InstructorId { get; set; }

        ////instead off id just save the whole instructor

        public int CourseId { get; set; }

        public int InstructorRating { get; set; }

        public string Summary { get; set; }

        public int CourseRating { get; set; }
    }
}
