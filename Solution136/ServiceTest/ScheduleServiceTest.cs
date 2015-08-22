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
            int? nullInt = null;
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
        public void RemoveCourseFromScheduleErrorTest() // course == null
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<IScheduleRepository>();
            var scheduleService = new ScheduleService(mockRepository.Object);

            //// Act
            scheduleService.RemoveCourseFromSchedule(string.Empty, ref errors);

            //// Assert course id cannot be null
            Assert.AreEqual(1, errors.Count);
        }
    }
}
