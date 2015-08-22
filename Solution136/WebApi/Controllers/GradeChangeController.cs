namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using IRepository;
    using POCO;
    using Repository;
    using Service;

    public class GradeChangeController : ApiController
    {
        private cse136Entities entities;

        public GradeChangeController()
        {
            this.entities = new cse136Entities();
        }

        public GradeChangeController(cse136Entities entities)
        {
            this.entities = entities;
        }

        [HttpPost]
        public string InsertGradeChange(GradeChange gc)
        {
            var errors = new List<string>();
            var repository = new GradeChangeRepository(this.entities);
            var service = new GradeChangeService(repository);
            service.InsertGradeChange(gc, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string RespondToGradeChange(GradeChange gc)
        {
            var errors = new List<string>();
            var repository = new GradeChangeRepository(this.entities);
            var service = new GradeChangeService(repository);
            service.RespondToGradeChange(gc, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }
    }
}