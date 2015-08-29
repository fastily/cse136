namespace POCO
{
    public class ScheduleMin
    {
        public string Year { get; set; }

        public string Quarter { get; set; }

        public override string ToString()
        {
            return this.Year + "-" + this.Quarter;
        }
    }
}
