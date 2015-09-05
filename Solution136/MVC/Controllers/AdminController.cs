namespace MVC.Controllers
{
    using System.Web.Mvc;

    public class AdminController : Controller
    {
        public ActionResult Index(int id)
        {
            ViewBag.id = id;
            this.Session["adminId"] = id;
            return this.View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult EditStudent(string id)
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

        public ActionResult EditCourse(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult EditCoursePreReq(int id)
        {
            ViewBag.id = id;
            return this.View();
        }

        public ActionResult EditCourseInSchedule(string year, string quarter, int courseId)
        {
            ViewBag.year = year;
            ViewBag.quarter = quarter;
            ViewBag.Id = courseId;
            return this.View();
        }

        public ActionResult EditSchedule(string year, string quarter)
        {
            ViewBag.year = year;
            ViewBag.quarter = quarter;
            return this.View();
        }

        public ActionResult AddCourseToSchedule(string year, string quarter, int courseId)
        {
            ViewBag.year = year;
            ViewBag.quarter = quarter;
            ViewBag.CourseId = courseId;
            return this.View();
        }

        public ActionResult AddSchedule()
        {
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

        public ActionResult CourseList()
        {
            return this.View();
        }

        public ActionResult ScheduleList()
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

        public ActionResult CreateCourse()
        {
            return this.View();
        }

        public ActionResult CreateSchedule()
        {
            return this.View();
        }

        public ActionResult SharedStudentEnrollment(string id)
        {
            ViewBag.Id = id;
            return this.View();
        }
    }
}
