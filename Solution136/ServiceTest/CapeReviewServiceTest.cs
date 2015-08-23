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
        [ExpectedException(typeof(NullReferenceException))]
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
        [ExpectedException(typeof(NullReferenceException))]
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

        [TestMethod]
        public void CapeReviewGetCourseRatingTest()
        {
            //// Arrange
            var errors = new List<string>();

            Mock<ICapeReviewRepository> mockRepository = new Mock<ICapeReviewRepository>();
            CapeReviewService capeservice = new CapeReviewService(mockRepository.Object);

            List<CapeReview> crl = new List<CapeReview>();
            crl.Add(new CapeReview { CapeId = 1000, CourseId = 999, InstructorId = 500, Summary = "T", InstructorRating = 3, CourseRating = 1 });
            crl.Add(new CapeReview { CapeId = 1001, CourseId = 999, InstructorId = 500, Summary = "T", InstructorRating = 2, CourseRating = 5 });
            crl.Add(new CapeReview { CapeId = 1002, CourseId = 999, InstructorId = 500, Summary = "T", InstructorRating = 1, CourseRating = 3 });
            mockRepository.Setup(x => x.FindCapeReviewsByCourseId(999, ref errors)).Returns(crl);

            //// Act
            var rating = capeservice.GetCourseRating(999, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(3, rating);
        }
    }
}
