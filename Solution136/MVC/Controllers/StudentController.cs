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

         public ActionResult ScheduleList(string id)
         {
              ViewBag.Id = id;
              return this.View();
         }
        
         public ActionResult AllScheduleList()
         {
             return this.View();
         }

         public ActionResult AddCourseToSchedule()
         {
             return this.View();
         }
 
    }
}
