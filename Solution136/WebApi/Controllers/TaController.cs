namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using IRepository;
    using POCO;
    using Repository;
    using Service;

    public class TaController : ApiController
    {
        private cse136Entities entities;

        public TaController()
        {
            this.entities = new cse136Entities();
        }

        public TaController(cse136Entities entities)
        {
            this.entities = entities;
        }

        [HttpGet]
        public List<Ta> GetTaList()
        {
            var errors = new List<string>();
            var repository = new TaRepository(this.entities);
            var service = new TaService(repository);
            return service.GetTaList(ref errors);
        }

        [HttpGet]
        public Ta GetTa(int id)
        {
            var errors = new List<string>();
            var repository = new TaRepository(this.entities);
            var service = new TaService(repository);
            return service.GetTaById(id, ref errors);
        }

        [HttpPost]
        public string InsertTa(Ta ta)
        {
            var errors = new List<string>();
            var repository = new TaRepository(this.entities);
            var service = new TaService(repository);
            service.InsertTa(ta, ref errors);
            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string UpdateTa(Ta ta)
        {
            var errors = new List<string>();
            var repository = new TaRepository(this.entities);
            var service = new TaService(repository);
            service.UpdateTa(ta, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }

        [HttpPost]
        public string DeleteTa(int id)
        {
            var errors = new List<string>();
            var repository = new TaRepository(this.entities);
            var service = new TaService(repository);
            service.DeleteTa(id, ref errors);

            if (errors.Count == 0)
            {
                return "ok";
            }

            return "error";
        }
    }
}