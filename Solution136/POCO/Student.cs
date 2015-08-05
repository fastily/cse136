namespace POCO
{
    using System.Collections.Generic;

    public class Student
    {
        public string StudentId { get; set; }

        public string SSN { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public float ShoeSize { get; set; }

        public int Weight { get; set; }

        public List<Schedule> Enrolled { get; set; }

        public override string ToString()
        {
            return this.StudentId + "-"
                + this.SSN + "-"
                + this.FirstName + "-"
                + this.LastName + "-"
                + this.Email + "-"
                + this.Password + "-"
                + this.ShoeSize + "-"
                + this.Weight;
        }
    }
}
