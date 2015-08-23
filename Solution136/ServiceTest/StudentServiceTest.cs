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
        public void UpdateStudentErrorTest()
        {
            //// Arrange
            var errors = new List<string>();
            var mockRepository = new Mock<IStudentRepository>();
            var studentService = new StudentService(mockRepository.Object);

            //// Act
            studentService.UpdateStudent(null, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateStudentErrorTest2()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IStudentRepository>();
            var studentService = new StudentService(mockRepository.Object);
            var student = new Student { StudentId = string.Empty };

            //// Act
            studentService.UpdateStudent(student, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateStudentErrorTest3()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IStudentRepository>();
            var studentService = new StudentService(mockRepository.Object);
            var student = new Student { StudentId = "aaa" };

            //// Act
            studentService.UpdateStudent(student, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateStudentErrorTest4()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IStudentRepository>();
            var studentService = new StudentService(mockRepository.Object);
            var student = new Student { StudentId = "aaaaaa", Email = "c@c" };

            //// Act
            studentService.UpdateStudent(student, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

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
        public void InsertStudentErrorTest3()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IStudentRepository>();
            var studentService = new StudentService(mockRepository.Object);
            var student = new Student { StudentId = "aaa" };

            //// Act
            studentService.InsertStudent(student, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertStudentErrorTest4()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IStudentRepository>();
            var studentService = new StudentService(mockRepository.Object);
            var student = new Student { StudentId = "aaaaaa", Email = "c@c" };

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
            mockRepository.Setup(x => x.GetEnrollments("testId", ref errors)).Returns(new List<Enrollment>());

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

            mockRepository.Setup(x => x.GetEnrollments("testId", ref errors)).Returns(enrollments);

            //// Act
            var gap = studentService.CalculateGpa("testId", enrollments, ref errors);

            //// Assert
            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(true, gap > 3.2f && gap < 3.3f);
        }

        [TestMethod]
        public void GetStudent()
        {
            var errors = new List<string>();
            var mockRepository = new Mock<IStudentRepository>();
            var studentService = new StudentService(mockRepository.Object);
            var student = new Student { StudentId = "aaaaaa", Email = "c@c.com" };
            var returnStudent = new Student();

            mockRepository.Setup(x => x.GetStudentDetail("aaaaaa", ref errors)).Returns(student);
            returnStudent = studentService.GetStudent("aaaaaa", ref errors);

            Assert.AreEqual("aaaaaa", returnStudent.StudentId);
            Assert.AreEqual("c@c.com", returnStudent.Email);
        }

        [TestMethod]
        public void GetStudentList()
        {
            var errors = new List<string>();
            var mockRepository = new Mock<IStudentRepository>();
            var studentService = new StudentService(mockRepository.Object);
            var studentList = new List<Student>();
            var student1 = new Student { StudentId = "aaaaaa", Email = "c@c.com" };
            var student2 = new Student { StudentId = "bbbbbb", Email = "c@c.com" };
            var returnStudent = new List<Student>();

            studentList.Add(student1);
            studentList.Add(student2);

            mockRepository.Setup(x => x.GetStudentList(ref errors)).Returns(studentList);
            returnStudent = studentService.GetStudentList(ref errors);

            Assert.AreEqual(returnStudent.Count, 2);
        }

        [TestMethod]
        public void UpdateStudent()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IStudentRepository>();
            var studentService = new StudentService(mockRepository.Object);
            var student = new Student { StudentId = "aaaaaa", Email = "c@c.com" };

            mockRepository.Setup(x => x.UpdateStudent(student, ref errors));

            //// Act
            studentService.UpdateStudent(student, ref errors);

            //// Assert
            mockRepository.Verify(x => x.UpdateStudent(student, ref errors), Times.Once());
        }

        [TestMethod]
        public void InsertStudent()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IStudentRepository>();
            var studentService = new StudentService(mockRepository.Object);
            var student = new Student { StudentId = "aaaaaa", Email = "c@c.com" };

            mockRepository.Setup(x => x.InsertStudent(student, ref errors));

            //// Act
            studentService.InsertStudent(student, ref errors);

            //// Assert
            mockRepository.Verify(x => x.InsertStudent(student, ref errors), Times.Once());
        }

        [TestMethod]
        public void DeleteStudent()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IStudentRepository>();
            var studentService = new StudentService(mockRepository.Object);

            mockRepository.Setup(x => x.DeleteStudent("aaaaaa", ref errors));

            //// Act
            studentService.DeleteStudent("aaaaaa", ref errors);

            //// Assert
            mockRepository.Verify(x => x.DeleteStudent("aaaaaa", ref errors), Times.Once());
        }
    }
}
