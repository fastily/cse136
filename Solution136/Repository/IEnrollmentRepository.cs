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
    }
}
