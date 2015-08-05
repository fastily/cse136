namespace RepositoryTest
{
    using System;
    using System.Collections.Generic;

    using IRepository;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using POCO;

    using Repository;

    [TestClass]
    public class StudentRepositoryTest
    {
        private readonly IStudentRepository studentRepository = new StudentRepository();

        private readonly ScheduleRepository scheduleRepositoryRepository = new ScheduleRepository();

        public void StudentIntegrationTest()
        {
            var student = new Student
            {
                FirstName = "first", 
                LastName = " last", 
                StudentId = Guid.NewGuid().ToString().Substring(0, 20), 
                SSN = "888991234", 
                Email = "myemail@ucsd.edu", 
                Password = "pass1234", 
                ShoeSize = 0, 
                Weight = 0
            };

            var errors = new List<string>();
            this.studentRepository.InsertStudent(student, ref errors);

            Assert.AreEqual(0, errors.Count);

            var verifyStudent = this.studentRepository.GetStudentDetail(student.StudentId, ref errors);

            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(student.FirstName, verifyStudent.FirstName);
            Assert.AreEqual(student.LastName, verifyStudent.LastName);
            Assert.AreEqual(student.StudentId, verifyStudent.StudentId);
            Assert.AreEqual(student.SSN, verifyStudent.SSN);
            Assert.AreEqual(student.Email, verifyStudent.Email);
            Assert.AreEqual(student.Password, verifyStudent.Password);
            Assert.AreEqual(student.ShoeSize, verifyStudent.ShoeSize);
            Assert.AreEqual(student.Weight, verifyStudent.Weight);

            var student2 = new Student
            {
                FirstName = "first2", 
                LastName = " last2", 
                StudentId = student.StudentId, 
                SSN = "777664321", 
                Email = "myemail2@ucsd.edu", 
                Password = "pass1234", 
                ShoeSize = 2, 
                Weight = 2
            };

            this.studentRepository.UpdateStudent(student2, ref errors);

            verifyStudent = this.studentRepository.GetStudentDetail(student2.StudentId, ref errors);
            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(student2.FirstName, verifyStudent.FirstName);
            Assert.AreEqual(student2.LastName, verifyStudent.LastName);
            Assert.AreEqual(student2.StudentId, verifyStudent.StudentId);
            Assert.AreEqual(student2.SSN, verifyStudent.SSN);
            Assert.AreEqual(student2.Email, verifyStudent.Email);
            Assert.AreEqual(student2.Password, verifyStudent.Password);
            Assert.AreEqual(student2.ShoeSize, verifyStudent.ShoeSize);
            Assert.AreEqual(student2.Weight, verifyStudent.Weight);

            var scheduleList = this.scheduleRepositoryRepository.GetScheduleList(string.Empty, string.Empty, ref errors);
            Assert.AreEqual(0, errors.Count);

            // enroll all available scheduled courses for this student
            foreach (var schedule in scheduleList)
            {
                this.studentRepository.EnrollSchedule(student.StudentId, schedule.ScheduleId, ref errors);
                Assert.AreEqual(0, errors.Count);
            }

            // drop all available scheduled courses for this student
            foreach (var schedule in scheduleList)
            {
                this.studentRepository.DropEnrolledSchedule(student.StudentId, schedule.ScheduleId, ref errors);
                Assert.AreEqual(0, errors.Count);
            }

            this.studentRepository.DeleteStudent(student.StudentId, ref errors);

            var verifyEmptyStudent = this.studentRepository.GetStudentDetail(student.StudentId, ref errors);
            Assert.AreEqual(0, errors.Count);
            Assert.AreEqual(null, verifyEmptyStudent);
        }
    }
}
