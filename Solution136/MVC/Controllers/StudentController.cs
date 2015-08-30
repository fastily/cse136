namespace MVC.Controllers
{
    using System.Web.Mvc;

    public class StudentController : Controller
    {
        public ActionResult Index(string id)
        {
            ViewBag.Id = id;
            Session["studentId"] = id;
            return this.View();
        }

        public ActionResult Edit(string id)
        {
            ViewBag.Id = id;
            Session["studentId"] = id;
            return this.View();
        }

        public ActionResult ViewCurrentSchedule(string id)
        {
            ViewBag.Id = id;
            return this.View();
        }

    }
}
