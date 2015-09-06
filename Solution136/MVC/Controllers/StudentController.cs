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
            return this.View();
        }

         public ActionResult ScheduleList(string id)
         {
              ViewBag.Id = id;
              return this.View();
         }
        
         public ActionResult AllScheduleList()
         {
             return this.View();
         }

         public ActionResult AddCourseToSchedule(string year, string quarter)
         {
             ViewBag.Year = year;
             ViewBag.Quarter = quarter;
             return this.View();
         }

         public ActionResult GradeChange(string id)
         {
             ViewBag.Id = id;
             return this.View();
         }

         public ActionResult RequestPrereq()
         {
             return this.View();
         }

         public ActionResult AddCapeReview(string id, int inst, string coursename, int courseid) {
             ViewBag.ScheduleId = id;
             ViewBag.inst = inst;
             ViewBag.coursename = coursename;
             ViewBag.courseid = courseid;
             return this.View();
         }
    }
}
