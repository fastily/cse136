namespace MVC.Controllers
{
    using System.Web.Mvc;

    public class StaffController : Controller
    {
        public ActionResult Index(int id)
        {
            Session["staffId"] = id;
            return this.View();
        }

        public ActionResult StudentList(int scheduleId)
        {
            ViewBag.scheduleId = scheduleId;
            return this.View();
        }

        public ActionResult CreateStudent()
        {
            return this.View();
        }

        public ActionResult EditStudent(string studentId, int scheduleId)
        {
            ViewBag.studentId = studentId;
            ViewBag.scheduleId = scheduleId;
            return this.View();
        }

        public ActionResult TaList()
        {
            return this.View();
        }

        public ActionResult CreateTa()
        {
            return this.View();
        }

        public ActionResult CourseList()
        {
            return this.View();
        }
    }
}
