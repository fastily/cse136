namespace ServiceTest
{
    using System;
    using System.Collections.Generic;

    using IRepository;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using POCO;
    using Service;

    [TestClass]
    public class AuthorizeServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AuthErrorTest()
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<IAuthorizeRepository>();
            var authService = new AuthorizeService(mockRepository.Object);

            //// Act
            authService.Authenticate(string.Empty, string.Empty, ref errors);

            //// Assert
            mockRepository.Verify(x => x.Authenticate(It.IsAny<string>(), It.IsAny<string>(), ref errors), Times.Never);
        }

        [TestMethod]
        public void AuthTest()
        {
            //// Arrange
            var errors = new List<string>();
            var logon = new Logon { Id = "1", Role = "TestRole" };

            var mockRepository = new Mock<IAuthorizeRepository>();
            mockRepository.Setup(x => x.Authenticate("testuser", "testpassword", ref errors))
                .Returns(logon);

            var authService = new AuthorizeService(mockRepository.Object);

            //// Act
            var logonReturned = authService.Authenticate("testuser", "testpassword", ref errors);

            //// Assert
            Assert.AreEqual(logon.ToString(), logonReturned.ToString());
            Assert.AreEqual(0, errors.Count);
            mockRepository.Verify(x => x.Authenticate(It.IsAny<string>(), It.IsAny<string>(), ref errors), Times.Once);
        }
    }
}
