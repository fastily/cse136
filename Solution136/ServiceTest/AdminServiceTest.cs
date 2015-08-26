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
    public class AdminServiceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateAdminErrorTest1()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IAdminRepository>();
            var adminService = new AdminService(mockRepository.Object);

            //// Act
            adminService.UpdateAdmin(null, ref errors);

            //// Assert instructor object not null
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateAdminErrorTest2()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IAdminRepository>();
            var adminService = new AdminService(mockRepository.Object);
            var adminPoco = new Admin()
            {
                FirstName = string.Empty
            };

            //// Act
            adminService.UpdateAdmin(adminPoco, ref errors);

            //// Assert instructor object not null
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UpdateAdminErrorTest3()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IAdminRepository>();
            var adminService = new AdminService(mockRepository.Object);
            var adminPoco = new Admin()
            {
                FirstName = "hi",
                LastName = string.Empty
            };

            //// Act
            adminService.UpdateAdmin(adminPoco, ref errors);

            //// Assert instructor object not null
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAdminErrorTest3()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IAdminRepository>();
            var adminService = new AdminService(mockRepository.Object);
            var adminPoco = new Admin()
            {
                FirstName = "hi",
                LastName = string.Empty
            };

            //// Act
            adminService.GetAdminById(0, ref errors);

            //// Assert instructor object not null
            Assert.AreEqual(1, errors.Count);
        }

        [TestMethod]
        public void UpdateAdmin()
        {
            //// Arranage
            var errors = new List<string>();
            var mockRepository = new Mock<IAdminRepository>();
            var adminService = new AdminService(mockRepository.Object);
            var adminPoco = new Admin()
            {
                FirstName = "hi",
                LastName = "bye",
                Id = 1
            };

            mockRepository.Setup(x => x.UpdateAdmin(adminPoco, ref errors));
            //// Act
            adminService.UpdateAdmin(adminPoco, ref errors);

            //// Assert instructor object not null
            mockRepository.Verify(x => x.UpdateAdmin(adminPoco, ref errors), Times.Once());
        }
    }
}
