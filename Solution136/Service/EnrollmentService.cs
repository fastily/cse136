namespace Service
{
    using System;
    using System.Collections.Generic;
    using IRepository;
    using POCO;

    public class EnrollmentService
    {
        private readonly IEnrollmentRepository repository;

        public EnrollmentService(IEnrollmentRepository repository)
        {
            this.repository = repository;
        }
    }
}
