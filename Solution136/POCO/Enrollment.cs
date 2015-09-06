namespace POCO
{
    using System.Collections.Generic;

    public class Enrollment
    {
        public string StudentId { get; set; }

        public int ScheduleId { get; set; }

        public string Grade { get; set; }

        public float GradeValue { get; set; }

        public Schedule EnrolledSchedule { get; set; }
    }
}
