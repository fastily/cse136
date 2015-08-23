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
    public class CapeReviewServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CapeReviewErrorTest1() ////;IsNullOrEmpty
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ICapeReviewRepository>();
            var capeService = new CapeReviewService(mockRepository.Object);

            //// Act
            capeService.GetCourseRating(0, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CapeReviewErrorTest2() ////;IsNullOrEmpty
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ICapeReviewRepository>();
            var capeService = new CapeReviewService(mockRepository.Object);

            //// Act
            capeService.GetInstructorRating(0, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }
    }
}
