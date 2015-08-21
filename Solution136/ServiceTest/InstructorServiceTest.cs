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
    public class InstructorServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertInstructorErrorTest1()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IInstructorRepository>();
            var instructorService = new InstructorService(mockRepository.Object);

            //// Act
            instructorService.InsertInstructor(null, ref errors);

            //// Assert cant be null instructor object
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertInstructorErrorTest2()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IInstructorRepository>();
            var instructorService = new InstructorService(mockRepository.Object);
            var instructor = new Instructor { FirstName = string.Empty };
            //// Act
            instructorService.InsertInstructor(instructor, ref errors);

            //// Assert firstname cannot be empty
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertInstructorErrorTest3()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IInstructorRepository>();
            var instructorService = new InstructorService(mockRepository.Object);
            var instructor = new Instructor { FirstName = "nick", LastName = string.Empty };

            //// Act
            instructorService.InsertInstructor(null, ref errors);

            //// Assert last name cannot be empty
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateInstructorErrorTest1()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IInstructorRepository>();
            var instructorService = new InstructorService(mockRepository.Object);

            //// Act
            instructorService.UpdateInstructor(null, ref errors);

            //// Assert instructor object not null
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateInstructorErrorTest2()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IInstructorRepository>();
            var instructorService = new InstructorService(mockRepository.Object);
            var instructor = new Instructor { FirstName = string.Empty };

            //// Act
            instructorService.UpdateInstructor(instructor, ref errors);

            //// Assert first name cannot be empty
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateInstructorErrorTest3()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IInstructorRepository>();
            var instructorService = new InstructorService(mockRepository.Object);
            var instructor = new Instructor { FirstName = "nick", LastName = string.Empty };

            //// Act
            instructorService.UpdateInstructor(instructor, ref errors);

            //// Assert last name cannot be empty
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteInstructorErrorTest()
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<IInstructorRepository>();
            var instructorService = new InstructorService(mockRepository.Object);

            //// Act
            instructorService.DeleteInstructor(null, ref errors);

            //// Assert instructor id cannot be null
            Assert.AreEqual(1, errors.Count);
        }
    }
}