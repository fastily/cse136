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
            enrollmentService.AddEnrollment(string.Empty, 0,ref errors);

            //// Assert cant be null course object
            Assert.AreEqual(1, errors.Count);
        }


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
        public void RemoveEnrollmentErrorTest() // course == null
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
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveEnrollmentErrorTest() // course == null
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


    }
}
