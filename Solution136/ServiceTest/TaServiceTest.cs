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
    public class TaServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertTaErrorTest1()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ITaRepository>();
            var teachingAssistantService = new TaService(mockRepository.Object);

            //// Act
            teachingAssistantService.InsertTa(null, ref errors);

            //// Assert cant be null instructor object
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertTaErrorTest2()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ITaRepository>();
            var teachingAssistantService = new TaService(mockRepository.Object);
            var ta = new Ta { FirstName = string.Empty };
            
            //// Act
            teachingAssistantService.InsertTa(ta, ref errors);

            //// Assert firstname cannot be empty
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertTaErrorTest3()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ITaRepository>();
            var teachingAssistantService = new TaService(mockRepository.Object);
            var ta = new Ta { FirstName = "nick", LastName = string.Empty };

            //// Act
            teachingAssistantService.InsertTa(ta, ref errors);

            //// Assert last name cannot be empty
            Assert.AreEqual(1, errors.Count);
        }


        [TestMethod]
        public void InsertTa()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ITaRepository>();
            var taService = new TaService(mockRepository.Object);
            var ta = new Ta { TaId = 2, TaType = "bb", FirstName = "cc", LastName = "dd" };

            mockRepository.Setup(x => x.AddTa(ta, ref errors));

            //// Act
            taService.InsertTa(ta, ref errors);

            //// Assert
            mockRepository.Verify(x => x.AddTa(ta, ref errors), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateTaErrorTest1()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ITaRepository>();
            var teachingAssistantService = new TaService(mockRepository.Object);

            //// Act
            teachingAssistantService.UpdateTa(null, ref errors);

            //// Assert instructor object not null
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateTaErrorTest2()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ITaRepository>();
            var teachingAssistantService = new TaService(mockRepository.Object);
            var ta = new Ta { FirstName = string.Empty };

            //// Act
            teachingAssistantService.UpdateTa(ta, ref errors);

            //// Assert first name cannot be empty
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateTaErrorTest3()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ITaRepository>();
            var teachingAssistantService = new TaService(mockRepository.Object);
            var ta = new Ta { FirstName = "nick", LastName = string.Empty };

            //// Act
            teachingAssistantService.UpdateTa(ta, ref errors);

            //// Assert last name cannot be empty
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void UpdateTa()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ITaRepository>();
            var taService = new TaService(mockRepository.Object);
            var ta = new Ta { TaId = 2, TaType = "bb", FirstName = "cc", LastName = "dd" };


            mockRepository.Setup(x => x.UpdateTa(ta, ref errors));

            //// Act
            taService.UpdateTa(ta, ref errors);

            //// Assert
            mockRepository.Verify(x => x.UpdateTa(ta, ref errors), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeleteTaErrorTest()
        {
            //// Arrange
            var errors = new List<string>();

            var mockRepository = new Mock<ITaRepository>();
            var teachingAssistantService = new TaService(mockRepository.Object);

            //// Act
            teachingAssistantService.DeleteTa(null, ref errors);

            //// Assert instructor id cannot be null
            Assert.AreEqual(1, errors.Count);
        }


        [TestMethod]
        public void DeleteTa()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ITaRepository>();
            var taService = new TaService(mockRepository.Object);
         
            mockRepository.Setup(x => x.RemoveTa(2, ref errors));

            //// Act
            taService.DeleteTa("2", ref errors);

            //// Assert
            mockRepository.Verify(x => x.RemoveTa(2, ref errors), Times.Once());
        }
    }
}
