namespace POCO
{
    public class ScheduleTime
    {
        public int TimeId { get; set; }

        public string Time { get; set; }

        public override string ToString()
        {
            return this.TimeId + "-" + this.Time;
        }
    }
}
