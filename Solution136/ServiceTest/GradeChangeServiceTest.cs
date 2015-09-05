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
    public class GradeChangeServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertGradeChageTestError1()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IGradeChangeRepository>();
            var gradeChangeService = new GradeChangeService(mockRepository.Object);
            var gc = new GradeChange { Course_id = -1 };

            //// Act
            gradeChangeService.AddGradeChange(gc, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RespondToGradeChageTestError1()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IGradeChangeRepository>();
            var gradeChangeService = new GradeChangeService(mockRepository.Object);
            var gc = new GradeChange { Course_id = -1 };

            //// Act
            gradeChangeService.ApproveGradeChange(gc, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void InsertGradeChangeTest()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IGradeChangeRepository>();
            var gradeChangeService = new GradeChangeService(mockRepository.Object);
            var gc = new GradeChange { Course_id = 5 };

            mockRepository.Setup(x => x.AddGradeChange(gc, ref errors));

            //// Act
            gradeChangeService.AddGradeChange(gc, ref errors);
            mockRepository.Verify(mock => mock.AddGradeChange(gc, ref errors), Times.Once());

            mockRepository.VerifyAll();
            //// Assert
            Assert.AreEqual(0, errors.Count);
        }

        [TestMethod]
        public void RespondToGradeChageTest()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IGradeChangeRepository>();
            var gradeChangeService = new GradeChangeService(mockRepository.Object);
            var gc = new GradeChange { Course_id = 5, GradeChangeId = 1 };

            mockRepository.Setup(x => x.ApproveGradeChange(5, ref errors));

            //// Act
            gradeChangeService.ApproveGradeChange(gc, ref errors);
            mockRepository.Verify(mock => mock.ApproveGradeChange(1, ref errors), Times.Once());

            //// Assert
            Assert.AreEqual(0, errors.Count);
        }
    }
}
