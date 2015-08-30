namespace POCO
{
    public class ScheduleDay
    {
        public int DayId { get; set; }

        public string Day { get; set; }

        public override string ToString()
        {
            return this.DayId + "-" + this.Day;
        }
    }
}
