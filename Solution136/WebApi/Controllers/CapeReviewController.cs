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
        public float GetCourseRating(int courseId)
        {
            var errors = new List<string>();
            var repository = new CapeReviewRepository(this.entities);
            var service = new CapeReviewService(repository);
            return service.GetCourseRating(courseId, ref errors); 
        }

        [HttpGet]
        public float GetInstructorRating(int instructorId)
        {
            var errors = new List<string>();
            var repository = new CapeReviewRepository(this.entities);
            var service = new CapeReviewService(repository);
            return service.GetInstructorRating(instructorId, ref errors);
        }


        [HttpGet]
        public List<CapeReview> FindCapeReviewByCourseId(int cid)
        {
            var errors = new List<string>();
            var repository = new CapeReviewRepository(this.entities);
            var service = new CapeReviewService(repository);
            return service.GetCapeReview(cid, ref errors);
        }

        [HttpPost]
        public string DeleteCapeReview(int cape_id)
        {
            var errors = new List<string>();
            var repository = new CapeReviewRepository(this.entities);
            var service = new CapeReviewService(repository);
            service.DeleteCapeReview(cape_id, ref errors);

            if (errors.Count == 0)
            {
                return "successful cape review delete";
            }

            return "Failed cape review delete";
        }

        [HttpPost]
        public string InsertCapeReview(CapeReview cr)
        {
            var errors = new List<string>();
            var repository = new CapeReviewRepository(this.entities);
            var service = new CapeReviewService(repository);
            service.InsertCapeReview(cr, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "Failed cape review insert";
        }

        [HttpPost]
        public string UpdateCapeReview(CapeReview cr)
        {
            var errors = new List<string>();
            var repository = new CapeReviewRepository(this.entities);
            var service = new CapeReviewService(repository);
            service.UpdateCapeReview(cr, ref errors);

            if (errors.Count == 0)
            {
                return "successful cape review insert";
            }

            return "Failed cape review insert";
        }
    }
}