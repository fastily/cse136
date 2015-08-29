namespace WebApiTest
{
    using System.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using POCO;
    using WebApi.Controllers;

    /// <summary>
    /// Enrollment test cases
    /// </summary>
    [TestClass]
    public class EnrollmentTest
    {
        [TestMethod]
        public void EnrollmentGetTest()
        {
            var enrollmentController = new EnrollmentController();
            var  = enrollmentController.AddEnrollment("1", 1);
            Assert.AreEqual("1", login.Id);
        }
        
        [TestMethod]
        public void EnrollmentPostTest()
        {
            var enrollmentController = new EnrollmentController();
            var login = enrollmentController.RemoveEnrollment("1", 1);
            Assert.AreEqual("1", login.Id);
        }
    }
}
