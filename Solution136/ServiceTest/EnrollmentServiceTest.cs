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
    public class EnrollmentServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddEnrollmentErrorTest1() // :  studentId  == null
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IEnrollmentRepository>();
            var enrollmentService = new EnrollmentService(mockRepository.Object);

            //// Act
            enrollmentService.AddEnrollment(string.Empty, 0, ref errors);

            //// Assert cant be null course object
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddEnrollmentErrorTest2() // :  scheduleId  == null
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IEnrollmentRepository>();
            var enrollmentService = new EnrollmentService(mockRepository.Object);
            int? nullInt = null;
            //// Act
            enrollmentService.AddEnrollment("1", (int)nullInt, ref errors);

            //// Assert cant be null course object
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddEnrollmentErrorTest3() // :  scheduleId  == null
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IEnrollmentRepository>();
            var enrollmentService = new EnrollmentService(mockRepository.Object);
            int? nullInt = null;
            //// Act
            enrollmentService.AddEnrollment("1", (int)nullInt, ref errors);

            //// Assert cant be null course object
            Assert.AreEqual(1, errors.Count);
        }

        /* how to check error for duplicate insertion - do we even need this ?
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddEnrollmentErrorTest1() // :  scheduleId  == null
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IEnrollmentRepository>();
            var enrollmentService = new EnrollmentService(mockRepository.Object);
            int? nullInt = null;
            //// Act
            enrollmentService.AddEnrollment("1", (int)nullInt, ref errors);

            //// Assert cant be null course object
            Assert.AreEqual(1, errors.Count);
        }
         */

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveEnrollmentErrorTest1() // course == null
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<IEnrollmentRepository>();
            var enrollmentService = new EnrollmentService(mockRepository.Object);
            
            //// Act
            enrollmentService.RemoveEnrollment(string.Empty, 1, ref errors);

            //// Assert course id cannot be null
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RemoveEnrollmentErrorTest2() // course == null
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<IEnrollmentRepository>();
            var enrollmentService = new EnrollmentService(mockRepository.Object);

            int? nullInt = null;
            //// Act
            enrollmentService.AddEnrollment("1", (int)nullInt, ref errors);

            //// Assert course id cannot be null
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void RemoveEnrollmentServiceTest()
        {
            //// Arrange
            var errors = new List<string>();

            Mock<IEnrollmentRepository> mockRepository = new Mock<IEnrollmentRepository>();
            EnrollmentService enrollservice = new EnrollmentService(mockRepository.Object);

            List<Enrollment> crl = new List<Enrollment>();
            crl.Add(new Enrollment { StudentId = "99", ScheduleId = 1000, Grade = "A+", GradeValue = 3.0f });

            mockRepository.Setup(x => x.RemoveEnrollment("99", 1000, ref errors));

            //// Act
            enrollservice.RemoveEnrollment("99", 1000, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
            ////Assert.AreEqual(3, rating);
        }

        [TestMethod]
        public void AddEnrollmentTest()
        {
            //// Arranage
            var errors = new List<string>();

            Mock<IEnrollmentRepository> mockRepository = new Mock<IEnrollmentRepository>();
            EnrollmentService iserv = new EnrollmentService(mockRepository.Object);

            Enrollment s = new Enrollment { StudentId = "99", ScheduleId = 1000, Grade = "A+", GradeValue = 3.0f };

            mockRepository.Setup(x => x.AddEnrollment("99", 1000, ref errors));
            mockRepository.Setup(x => x.IsNotDuplicateEnrollment("99", 1000, ref errors)).Returns(true);

            //// Act
            iserv.AddEnrollment("99", 1000, ref errors);

            //// Assert
            mockRepository.Verify(x => x.AddEnrollment("99", 1000, ref errors), Times.Once());
        }
    }
}
