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


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertCapeReviewErrorTest1() // :  CapeReview == null
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ICapeReviewRepository>();
            var capeReviewService = new CapeReviewService(mockRepository.Object);

            //// Act
            capeReviewService.InsertCapeReview(null, ref errors);

            //// Assert cant be null CapeReview object
            Assert.AreEqual(1, errors.Count);
        }/*

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertCapeReviewErrorTest2() // : instr == null
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ICapeReviewRepository>();
            //int? nullInt = null; // (int)nullInt

            var capeReviewService = new CapeReviewService(mockRepository.Object);
            //// Act

            var capeReview = new CapeReview
            {
                CapeId = 1,
                CourseId = 2,
                CourseRating = 3,
                //InstructorId = (int)nullInt,
                InstructorRating = 5,
                Summary = "nope"
            };

            //// Act
            capeReviewService.InsertCapeReview(capeReview, ref errors);

            //// Assert last name cannot be empty
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertCapeReviewErrorTest3() // : course == null
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ICapeReviewRepository>();
            var capeReviewService = new CapeReviewService(mockRepository.Object);
            int? nullInt = null; // (int)nullInt

            var capeReview = new CapeReview
            {
                CapeId = 1,
                CourseId = (int)nullInt
                ,
                CourseRating = 3,
                InstructorId = 4,
                InstructorRating = 5,
                Summary = "nope"
            };
            //// Act
            capeReviewService.InsertCapeReview(capeReview, ref errors);

            //// Assert last name cannot be empty
            Assert.AreEqual(1, errors.Count);
        }*/

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertCapeReviewErrorTest4() // : summary == null
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ICapeReviewRepository>();
            var capeReviewService = new CapeReviewService(mockRepository.Object);
            var capeReview = new CapeReview { CapeId = 1, CourseId = 2, CourseRating = 3, InstructorId = 4, InstructorRating = 5, Summary = null};

            //// Act
            capeReviewService.InsertCapeReview(capeReview, ref errors);

            //// Assert last name cannot be empty
            Assert.AreEqual(1, errors.Count);
        }


        [TestMethod]
        public void InsertCapeReviewTest()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ICapeReviewRepository>();
            var capeReviewService = new CapeReviewService(mockRepository.Object);
            var capeReview = new CapeReview { CapeId = 1, CourseId = 2, CourseRating = 3, InstructorId = 4, InstructorRating = 5, Summary = "hello"};

            mockRepository.Setup(x => x.InsertCape(capeReview, ref errors));

            //// Act
            capeReviewService.InsertCapeReview(capeReview, ref errors);

            //// Assert
            mockRepository.Verify(x => x.InsertCape(capeReview, ref errors), Times.Once());
        }

        /*
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteCapeReviewErrorTest() // CapeReview == null
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<ICapeReviewRepository>();
            var capeReviewService = new CapeReviewService(mockRepository.Object);
            int? nullInt = null; // (int)nullInt
            var capeReview = new CapeReview { CapeId = (int)nullInt, CourseId = 2, CourseRating = 3, InstructorId = 4, InstructorRating = 5, Summary = "hello" };

            //// Act
             capeReviewService.DeleteCapeReview(capeReview.CapeId, ref errors);

            //// Assert capeReview id cannot be null
            Assert.AreEqual(1, errors.Count);
        }*/

        [TestMethod]
        public void DeleteCapeReviewTest()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ICapeReviewRepository>();
            var capeReviewService = new CapeReviewService(mockRepository.Object);
           
            mockRepository.Setup(x => x.DeleteCapeReview(2, ref errors));

            //// Act
            capeReviewService.DeleteCapeReview(2, ref errors);

            //// Assert
            mockRepository.Verify(x => x.DeleteCapeReview(2, ref errors), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateCapeReviewErrorTest1() // cape review == null
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ICapeReviewRepository>();
            var capeReviewService = new CapeReviewService(mockRepository.Object);

            //// Act
            capeReviewService.UpdateCapeReview(null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }
        /*
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateCapeReviewErrorTest2() // inst null
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ICapeReviewRepository>();
            var capeReviewService = new CapeReviewService(mockRepository.Object);
            int? nullInt = null; // (int)nullInt

            var capeReview = new CapeReview { CapeId = 1, CourseId = 2, CourseRating = 3, InstructorId = (int)nullInt, InstructorRating = 5, Summary = "hello" };

            //// Act
            capeReviewService.UpdateCapeReview(capeReview, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateCapeReviewErrorTest3() // course id == null
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ICapeReviewRepository>();
            var capeReviewService = new CapeReviewService(mockRepository.Object);
            int? nullInt = null; // (int)nullInt

            var capeReview = new CapeReview { CapeId = 1, CourseId = (int)nullInt, CourseRating = 3, InstructorId = 4, InstructorRating = 5, Summary = "hello" };

            //// Act
            capeReviewService.UpdateCapeReview(capeReview, ref errors);

            //// Assert 
            Assert.AreEqual(1, errors.Count);
        }
        */
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateCapeReviewErrorTest4() // summ null
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ICapeReviewRepository>();
            var capeReviewService = new CapeReviewService(mockRepository.Object);
            var capeReview = new CapeReview { CapeId = 1, CourseId = 2, CourseRating = 3, InstructorId = 4, InstructorRating = 5, Summary = null };

            //// Act
            capeReviewService.UpdateCapeReview(capeReview, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void UpdateCapeReviewTest()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ICapeReviewRepository>();
            var capeReviewService = new CapeReviewService(mockRepository.Object);
            var capeReview = new CapeReview { CapeId = 1, CourseId = 2, CourseRating = 3, InstructorId = 4, InstructorRating = 5, Summary = "hello" };


            mockRepository.Setup(x => x.UpdateCapeReview(capeReview, ref errors));

            //// Act
            capeReviewService.UpdateCapeReview(capeReview, ref errors);

            //// Assert
            mockRepository.Verify(x => x.UpdateCapeReview(capeReview, ref errors), Times.Once());
        }
    }
}
