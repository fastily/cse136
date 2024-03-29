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
            var ta_Service = new TaService(mockRepository.Object);
            var ta_1 = new Ta { TaId = 2, TaType = "bb", FirstName = "cc", LastName = "dd" };

            mockRepository.Setup(x => x.AddTa(ta_1, ref errors));
            mockRepository.Setup(x => x.IsNotDuplicateTa(ta_1, ref errors)).Returns(true);

            //// Act
            ta_Service.InsertTa(ta_1, ref errors);

            //// Assert
            mockRepository.Verify(x => x.AddTa(ta_1, ref errors), Times.Once());
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
            var ta_Service = new TaService(mockRepository.Object);
            var ta_1 = new Ta { TaId = 2, TaType = "bb", FirstName = "cc", LastName = "dd" };

            mockRepository.Setup(x => x.UpdateTa(ta_1, ref errors));

            //// Act
            ta_Service.UpdateTa(ta_1, ref errors);

            //// Assert
            mockRepository.Verify(x => x.UpdateTa(ta_1, ref errors), Times.Once());
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
            teachingAssistantService.DeleteTa(0, ref errors);

            //// Assert instructor id cannot be null
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void DeleteTa()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<ITaRepository>();
            var ta_Service = new TaService(mockRepository.Object);
         
            mockRepository.Setup(x => x.RemoveTa(2, ref errors));

            //// Act
            ta_Service.DeleteTa(2, ref errors);

            //// Assert
            mockRepository.Verify(x => x.RemoveTa(2, ref errors), Times.Once());
        }

        [TestMethod]
        public void GetTa()
        {
            var errors = new List<string>();
            var mockRepository = new Mock<ITaRepository>();
            var teachingAssistantService = new TaService(mockRepository.Object);
            var ta_1 = new Ta { FirstName = "hi", LastName = "bye", TaId = 5 };
            var returnTa = new Ta();

            mockRepository.Setup(x => x.FindTaById(5, ref errors)).Returns(ta_1);
            returnTa = teachingAssistantService.GetTaById(5, ref errors);

            Assert.AreEqual(returnTa.TaId, 5);
            Assert.AreEqual(returnTa.FirstName, "hi");
            Assert.AreEqual(returnTa.LastName, "bye");
        } 

        [TestMethod]
        public void GetTaList()
        {
            var errors = new List<string>();
            var mockRepository = new Mock<ITaRepository>();
            var teachingAssistantService = new TaService(mockRepository.Object);
            var ta_List = new List<Ta>();
            var ta_1 = new Ta { FirstName = "hi", LastName = "bye" };
            var ta_2 = new Ta { FirstName = "hi", LastName = "bye" };
            var returnTaList = new List<Ta>();

            ta_List.Add(ta_1);
            ta_List.Add(ta_2);

            mockRepository.Setup(x => x.GetTaList(ref errors)).Returns(ta_List);
            returnTaList = teachingAssistantService.GetTaList(ref errors);

            Assert.AreEqual(returnTaList.Count, 2);
        }  
    }
}
