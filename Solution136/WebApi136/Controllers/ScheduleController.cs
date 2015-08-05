namespace WebApi136.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using POCO;

    using Repository;

    using Service;

    public class ScheduleController : ApiController
    {
        [HttpPost]
        public List<Schedule> GetScheduleList(string year, string quarter)
        {
            var service = new ScheduleService(new ScheduleRepository());
            var errors = new List<string>();
            return service.GetScheduleList(year, quarter, ref errors);
        }
    }
}