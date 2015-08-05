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
    public class StudentServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertStudentErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IStudentRepository>();
            var studentService = new StudentService(mockRepository.Object);

            //// Act
            studentService.InsertStudent(null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertStudentErrorTest2()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IStudentRepository>();
            var studentService = new StudentService(mockRepository.Object);
            var student = new Student { StudentId = string.Empty };

            //// Act
            studentService.InsertStudent(student, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void StudentErrorTest()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IStudentRepository>();
            var studentService = new StudentService(mockRepository.Object);

            //// Act
            studentService.GetStudent(null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteStudentErrorTest()
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<IStudentRepository>();
            var studentService = new StudentService(mockRepository.Object);

            //// Act
            studentService.DeleteStudent(null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateGpaErrorTest()
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<IStudentRepository>();
            var studentService = new StudentService(mockRepository.Object);

            //// Act
            studentService.CalculateGpa(string.Empty, null, ref errors);

            //// Assert
            Assert.AreEqual(2, errors.Count);
        }

        [TestMethod]
        public void CalculateGpaNoEnrollmentTest()
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<IStudentRepository>();
            var studentService = new StudentService(mockRepository.Object);
            mockRepository.Setup(x => x.GetEnrollments("testId")).Returns(new List<Enrollment>());

            //// Act
            var enrollments = studentService.GetEnrollments("testId", ref errors);
            var gap = studentService.CalculateGpa("testId", enrollments, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(0.0f, gap);
        }

        [TestMethod]
        public void CalculateGpaWithEnrollmentTest()
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<IStudentRepository>();
            var studentService = new StudentService(mockRepository.Object);
            var enrollments = new List<Enrollment>();
            enrollments.Add(new Enrollment { Grade = "A", GradeValue = 4.0f, ScheduleId = 1, StudentId = "testId" });
            enrollments.Add(new Enrollment { Grade = "B", GradeValue = 3.0f, ScheduleId = 2, StudentId = "testId" });
            enrollments.Add(new Enrollment { Grade = "C+", GradeValue = 2.7f, ScheduleId = 3, StudentId = "testId" });

            mockRepository.Setup(x => x.GetEnrollments("testId")).Returns(enrollments);

            //// Act
            var gap = studentService.CalculateGpa("testId", enrollments, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(true, gap > 3.2f && gap < 3.3f);
        }
    }
}
