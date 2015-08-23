namespace WebApiTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Diagnostics;
    using POCO;

    using WebApi.Controllers;

    /// <summary>
    /// Authorize test cases
    /// </summary>
    [TestClass]
    public class AuthorizeTest
    {
        [TestMethod]
        public void AuthorizeGetTest()
        {
            var authorizeController = new AuthorizeController();
            var login = authorizeController.Authenticate("admin@cs.ucsd.edu", "password");
            ////Debug.Write("WTF IS THIS " + login.Id);
            Assert.AreEqual("1", login.Id);
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
