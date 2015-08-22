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
            courseService.GetCourse(string.Empty, ref errors);

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
            courseService.DeleteCourse(string.Empty, ref errors);

            //// Assert course id cannot be null
            Assert.AreEqual(1, errors.Count);
        }

        // WTF int can't be null ?
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
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
        [ExpectedException(typeof(ArgumentException))]
        public void AssignPreReqErrorTest1() // preReqCourseId == null
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
        [ExpectedException(typeof(ArgumentException))]
        public void AssignPreReqErrorTest1() // courseid == null
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
        [ExpectedException(typeof(ArgumentException))]
        public void AssignPreReqErrorTest1() // preReqCourseId == null
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
        [ExpectedException(typeof(ArgumentException))]
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

    }
}
