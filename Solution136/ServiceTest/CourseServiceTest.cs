﻿namespace ServiceTest
{
    using System;
    using System.Collections.Generic;

    using IRepository;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using POCO;
    using Service;

    [TestClass]
    public class CourseServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertCourseErrorTest1() // :  course == null
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);

            //// Act
            courseService.InsertCourse(null, ref errors);

            //// Assert cant be null course object
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertCourseErrorTest2() // : title == null
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);
            var course = new Course { Title = string.Empty };

            //// Act
            courseService.InsertCourse(course, ref errors);

            //// Assert last name cannot be empty
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateCourseErrorTest1() // : course == null
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);

            //// Act
            courseService.UpdateCourse(null, ref errors);

            //// Assert course object not null
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateCourseErrorTest2() // : title == null
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);
            var course = new Course { Title = string.Empty };

            //// Act
            courseService.UpdateCourse(course, ref errors);

            //// Assert first name cannot be empty
            Assert.AreEqual(1, errors.Count);
        }

        // getCourseRating test
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCourseErrorTest() // : course == null
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);

            //// Act
            courseService.GetCourse(0, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteCourseErrorTest() // course == null
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);

            //// Act
            courseService.DeleteCourse(0, ref errors);

            //// Assert course id cannot be null
            Assert.AreEqual(1, errors.Count);
        }

        // WTF int can't be null ?
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AssignPreReqErrorTest1() // courseid == null
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);
            int? nullInt = null;
            //// Act
            courseService.AssignPreReq((int)nullInt, 1, ref errors);

            //// Assert course object not null
            Assert.AreEqual(1, errors.Count);
        }

        // WTF int can't be null ?
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AssignPreReqErrorTest2() // preReqCourseId == null
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);
            int? nullInt = null;
            //// Act
            courseService.AssignPreReq(1, (int)nullInt, ref errors);

            //// Assert course object not null
            Assert.AreEqual(1, errors.Count);
        }

        // WTF int can't be null ?
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RemovePreReqErrorTest1() // courseid == null
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);
            int? nullInt = null;
            //// Act
            courseService.RemovePreReq((int)nullInt, 1, ref errors);

            //// Assert course object not null
            Assert.AreEqual(1, errors.Count);
        }

        // WTF int can't be null ?
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RemovePreReqErrorTest2() // preReqCourseId == null
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);
            int? nullInt = null;
            //// Act
            courseService.RemovePreReq(1, (int)nullInt, ref errors);

            //// Assert course object not null
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetPreReqErrorTest()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);
            int? nullInt = null;
            //// Act
            courseService.GetPreReqList((int)nullInt, ref errors);

            //// Assert
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void GetCourseListTest()
        {
            //// Arrange
            var errors = new List<string>();

            Mock<ICourseRepository> mockRepository = new Mock<ICourseRepository>();
            CourseService courseservice = new CourseService(mockRepository.Object);

            List<Course> crl = new List<Course>();
            crl.Add(new Course { CourseId = 99, Title = "T", Description = "Test" });

            mockRepository.Setup(x => x.GetCourseList(ref errors)).Returns(crl);

            //// Act
            Course temp = courseservice.GetCourseList(ref errors)[0];

            //// Assert
            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(99, temp.CourseId);
        }

        [TestMethod]
        public void RemoveCourseTest()
        {
            //// Arranage
            var errors = new List<string>();

            Mock<ICourseRepository> mockRepository = new Mock<ICourseRepository>();
            CourseService iserv = new CourseService(mockRepository.Object);

            Course s = new Course { CourseId = 99, Title = "T", Description = "Test" };

            mockRepository.Setup(x => x.RemoveCourse(99, ref errors));

            //// Act
            iserv.DeleteCourse(99, ref errors);

            //// Assert
            mockRepository.Verify(x => x.RemoveCourse(99, ref errors), Times.Once());
        }

        [TestMethod]
        public void AddCourseTest()
        {
            //// Arranage
            var errors = new List<string>();

            Mock<ICourseRepository> mockRepository = new Mock<ICourseRepository>();
            CourseService iserv = new CourseService(mockRepository.Object);

            Course s = new Course { CourseId = 99, Title = "T", Description = "Test" };

            mockRepository.Setup(x => x.AddCourse(s, ref errors));
            mockRepository.Setup(x => x.IsNotDuplicateCourse(s, ref errors)).Returns(true);

            //// Act
            iserv.InsertCourse(s, ref errors);

            //// Assert
            mockRepository.Verify(x => x.AddCourse(s, ref errors), Times.Once());
        }

        [TestMethod]
        public void UpdateCourseTest()
        {
            //// Arranage
            var errors = new List<string>();

            Mock<ICourseRepository> mockRepository = new Mock<ICourseRepository>();
            CourseService iserv = new CourseService(mockRepository.Object);

            Course s = new Course { CourseId = 99, Title = "T", Description = "Test" };

            mockRepository.Setup(x => x.UpdateCourse(s, ref errors));

            //// Act
            iserv.UpdateCourse(s, ref errors);

            //// Assert
            mockRepository.Verify(x => x.UpdateCourse(s, ref errors), Times.Once());
        }

        [TestMethod]
        public void AssignPreReqPassTest()
        {
            //// Arranage
            string courseName1 = "Test5";
            string courseName2 = "Test6";
            string courseName3 = "Test7";
            var errors1 = new List<string>();
            var errors2 = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);
            var course1 = new Course { Title = courseName1 };
            var course2 = new Course { Title = courseName2 };
            var course3 = new Course { Title = courseName3 };
            courseService.InsertCourse(course1, ref errors1);
            courseService.InsertCourse(course2, ref errors1);
            courseService.InsertCourse(course3, ref errors1);

            //// Act
            courseService.AssignPreReq(1, 2, ref errors2);

            //// Assert
            Assert.AreEqual(0, errors2.Count);
        }

        [TestMethod]
        public void RemovePreReqPassTest()
        {
            //// Arranage
            string courseName1 = "Test5";
            string courseName2 = "Test6";
            string courseName3 = "Test7";
            var errors1 = new List<string>();
            var errors2 = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);
            var course1 = new Course { Title = courseName1 };
            var course2 = new Course { Title = courseName2 };
            var course3 = new Course { Title = courseName3 };
            courseService.InsertCourse(course1, ref errors1);
            courseService.InsertCourse(course2, ref errors1);
            courseService.InsertCourse(course3, ref errors1);
            courseService.AssignPreReq(1, 2, ref errors1);

            //// Act
            courseService.RemovePreReq(1, 2, ref errors2);

            //// Assert
            Assert.AreEqual(0, errors2.Count);
        }

        [TestMethod]
        public void GetPreReqPassTest()
        {
            //// Arranage
            string courseName1 = "Test5";
            string courseName2 = "Test6";
            string courseName3 = "Test7";
            var errors1 = new List<string>();
            var errors2 = new List<string>();
            var mockRepository = new Mock<ICourseRepository>();
            var courseService = new CourseService(mockRepository.Object);
            var course1 = new Course { Title = courseName1 };
            var course2 = new Course { Title = courseName2 };
            var course3 = new Course { Title = courseName3 };
            courseService.InsertCourse(course1, ref errors1);
            courseService.InsertCourse(course2, ref errors1);
            courseService.InsertCourse(course3, ref errors1);
            courseService.AssignPreReq(1, 2, ref errors1);

            //// Act
            courseService.GetPreReqList(1, ref errors2);

            //// Assert
            Assert.AreEqual(0, errors2.Count);
        }
    }
}
