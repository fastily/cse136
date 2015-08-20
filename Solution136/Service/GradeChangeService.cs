namespace Service
{
    using System;
    using System.Collections.Generic;
    using IRepository;
    using POCO;

    public class GradeChangeService
    {
        private readonly IGradeChangeRepository repository;

        public GradeChangeService(IGradeChangeRepository repository)
        {
            this.repository = repository;
        }
    }
}
