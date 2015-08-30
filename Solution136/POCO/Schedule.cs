namespace POCO
{
    public class Schedule
    {
        public int ScheduleId { get; set; }

        public string Year { get; set; }

        public string Quarter { get; set; }

        public string Session { get; set; }

        public ScheduleDay Day { get; set; }

        public ScheduleTime Time { get; set; }

        public Instructor Instructor { get; set; }

        public Course Course { get; set; }

        public override string ToString()
        {
            return this.ScheduleId + "-" + this.Year + "-" + this.Quarter + "-" + this.Session + "-" + this.Course;
        }
    }
}
