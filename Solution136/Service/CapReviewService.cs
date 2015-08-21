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

        ////course rating

        ////instructor rating

        ////find cape reviews by scheduleId

        ////insert cape review schedule id, student id, instructor id
    }
}
