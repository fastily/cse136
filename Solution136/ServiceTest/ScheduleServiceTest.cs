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
    public class ScheduleServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddCourseToScheduleErrorTest1() // :  schedule  == null
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IScheduleRepository>();
            var scheduleService = new ScheduleService(mockRepository.Object);
            ///  int? nullInt = null;
            //// Act
            scheduleService.AddCourseToSchedule(null, 1, "2", "3", ref errors);

            //// Assert cant be null course object
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddCourseToScheduleErrorTest2() // :  dayId  == null
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IScheduleRepository>();
            var scheduleService = new ScheduleService(mockRepository.Object);
            var schedule = new Schedule { };

            //// Act
            scheduleService.AddCourseToSchedule(schedule, 1, string.Empty, "3", ref errors);

            //// Assert cant be null course object
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddCourseToScheduleErrorTest3() // :  timeID  == null
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IScheduleRepository>();
            var scheduleService = new ScheduleService(mockRepository.Object);
            var schedule = new Schedule { };
            //// Act
            scheduleService.AddCourseToSchedule(schedule, 1, "2", string.Empty, ref errors);

            //// Assert cant be null course object
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveCourseFromScheduleErrorTest() //// course == null
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<IScheduleRepository>();
            var scheduleService = new ScheduleService(mockRepository.Object);

            //// Act
            scheduleService.RemoveCourseFromSchedule(0, ref errors);

            //// Assert course id cannot be null
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void GetScheduleListTest()
        {
            //// Arrange
            var errors = new List<string>();

            Mock<IScheduleRepository> mockRepository = new Mock<IScheduleRepository>();
            ScheduleService schedservice = new ScheduleService(mockRepository.Object);

            List<Schedule> crl = new List<Schedule>();
            crl.Add(new Schedule { ScheduleId = 99, Year = "1000", Quarter = "Winter", Session = "2" });

            mockRepository.Setup(x => x.GetScheduleList("1000", "Winter", ref errors)).Returns(crl);

            //// Act
            Schedule temp = schedservice.GetScheduleList("1000", "Winter", ref errors)[0];

            //// Assert
            Assert.AreEqual(99, temp.ScheduleId);
            ////Assert.AreEqual(3, rating);
        }

        [TestMethod]
        public void AddCourseScheduleTest()
        {
            //// Arranage
            var errors = new List<string>();

            Mock<IScheduleRepository> mockRepository = new Mock<IScheduleRepository>();
            ScheduleService iserv = new ScheduleService(mockRepository.Object);

            Schedule s = new Schedule { ScheduleId = 99, Year = "1000", Quarter = "Winter", Session = "2" };

            mockRepository.Setup(x => x.AddCourseToSchedule(s, 99, 1, 1, ref errors));

            //// Act
            iserv.AddCourseToSchedule(s, 99, "1", "1", ref errors);

            //// Assert
            mockRepository.Verify(x => x.AddCourseToSchedule(s, 99, 1, 1, ref errors), Times.Once());
        }

        [TestMethod]
        public void RemoveCourseFromScheduleTest()
        {
            //// Arranage
            var errors = new List<string>();

            Mock<IScheduleRepository> mockRepository = new Mock<IScheduleRepository>();
            ScheduleService iserv = new ScheduleService(mockRepository.Object);

            Schedule s = new Schedule { ScheduleId = 99, Year = "1000", Quarter = "Winter", Session = "2" };

            mockRepository.Setup(x => x.RemoveCourseFromSchedule(99, ref errors));

            //// Act
            iserv.RemoveCourseFromSchedule(99, ref errors);

            //// Assert
            mockRepository.Verify(x => x.RemoveCourseFromSchedule(99, ref errors), Times.Once());
        }
    }
}
