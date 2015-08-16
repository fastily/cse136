namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using IRepository;
    using POCO;

    public class EnrollmentRepository : IEnrollmentRepository
    {
        private cse136Entities context;

        public EnrollmentRepository(cse136Entities entities)
        {
            this.context = entities;
        }

        public void AddEnrollment(int studentId, int ScheduleId, string year, string quarter, string session, Course Course, ref List<string> errors);
        {
        }


        public void RemoveEnrolement (int studentId, int ScheduleId, ref List<string> errors);
        {
        }
    }
}
