namespace Service
{
    using System;
    using System.Collections.Generic;
    using IRepository;
    using POCO;

    public class CapeReviewService
    {
        private readonly ICapeReviewRepository repository;

        public CapeReviewService(ICapeReviewRepository repository)
        {
            this.repository = repository;
        }
    }
}
