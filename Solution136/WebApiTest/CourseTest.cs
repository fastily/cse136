namespace WebApiTest
{
    using System.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using POCO;
    using WebApi.Controllers;

    /// <summary>
    /// Authorize test cases
    /// </summary>
    [TestClass]
    public class CourseTest
    {
        [TestMethod]
        public void CourseInsertTest()
        {
            var courseController = new CourseController();
            var returnString = courseController.InsertCourse(new Course { Title = "Nicks Test Course", CourseLevel = CourseLevel.grad, Description = "Easy A"});
            Assert.AreEqual("ok", returnString);
        }

        [TestMethod]
        public void AuthorizePostTest()
        {
            var authorizeController = new AuthorizeController();
            var login = authorizeController.Authenticate(new Logon { UserName = "admin@cs.ucsd.edu", Password = "password" });
            Assert.AreEqual("1", login.Id);
        }
    }
}
