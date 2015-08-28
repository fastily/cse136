namespace MVC.Controllers
{
    using System.Web.Mvc;

    public class AdminController : Controller
    {
        public ActionResult Index(int id)
        {
            ViewBag.id = id;
            Session["adminId"] = id;
            return this.View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult EditStudent(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult EditTa(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult EditInstructor(int id)
        {
            ViewBag.id = id;
            return this.View();
        }


        public ActionResult DetailsInstructor(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult DetailsTa(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult StudentList()
        {
            return this.View();
        }

        public ActionResult TaList()
        {
            return this.View();
        }

        public ActionResult InstructorList()
        {
            return this.View();
        }

        public ActionResult CreateStudent()
        {
            return this.View();
        }

        public ActionResult CreateTa()
        {
            return this.View();
        }

        public ActionResult CreateInstructor()
        {
            return this.View();
        }
    }
}
