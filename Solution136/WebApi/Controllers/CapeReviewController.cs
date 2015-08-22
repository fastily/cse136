namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using IRepository;
    using POCO;
    using Repository;
    using Service;

    public class CapeReviewController : ApiController
    {
        private cse136Entities entities;

        public CapeReviewController()
        {
            this.entities = new cse136Entities();
        }

        public CapeReviewController(cse136Entities entities)
        {
            this.entities = entities;
        }

        [HttpGet]
        public int GetCourseRating(int courseId)
        {
            var errors = new List<string>();
            var repository = new CapeReviewRepository(this.entities);
            var service = new CapeReviewService(repository);
            return service.GetCourseRating(courseId, ref errors); 
        }

        [HttpGet]
        public int GetInstructorRating(int instructorId)
        {
            var errors = new List<string>();
            var repository = new CapeReviewRepository(this.entities);
            var service = new CapeReviewService(repository);
            return service.GetInstructorRating(instructorId, ref errors);
        }
    }
}