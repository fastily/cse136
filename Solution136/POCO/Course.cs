namespace POCO
{
    public class Course
    {
        public int CourseId { get; set; }

        public string Title { get; set; }

        public CourseLevel CourseLevel { get; set; }

        public string Description { get; set; }

        public string Level { get; set; }

        public int ScheduleId { get; set; }

        public override string ToString()
        {
            return this.CourseId + "-" + this.Title + "-" + this.CourseLevel + "-" + this.Description;
        }
    }
}
