namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using IRepository;
    using POCO;

    public class InstructorRepository : BaseRepository, IInstructorRepository
    {
        private cse136Entities context;

        public InstructorRepository(cse136Entities entities)
        {
            this.context = entities;
        }

        public Instructor FindInstructorByName(string instName, ref List<string> errors)
        {
            POCO.Instructor pocoInstructor = new POCO.Instructor();
            instructor db_instructor;
            try
            {
                db_instructor = this.context.instructors.Where(x => x.first_name == instName).First();
                if (db_instructor != null)
                {
                    pocoInstructor.InstructorId = db_instructor.instructor_id;
                    pocoInstructor.FirstName = db_instructor.first_name;
                    pocoInstructor.LastName = db_instructor.last_name;
                    pocoInstructor.Title = db_instructor.title;
                }
            }
            catch (Exception e)
            {
                errors.Add("Error occured in InstructorRepository.FindInstructorByName: " + e);
            }

            return pocoInstructor;
        }

        public Instructor FindInstructorById(int instId, ref List<string> errors)
        {
            POCO.Instructor pocoInstructor = new POCO.Instructor();
            instructor db_instructor;
            try
            {
                db_instructor = this.context.instructors.Find(instId);
                if (db_instructor != null)
                {
                    pocoInstructor.InstructorId = db_instructor.instructor_id;
                    pocoInstructor.FirstName = db_instructor.first_name;
                    pocoInstructor.LastName = db_instructor.last_name;
                    pocoInstructor.Title = db_instructor.title;
                }
            }
            catch (Exception e)
            {
                errors.Add("Error occured in InstructorRepository.FindInstructorById: " + e);
            }

            return pocoInstructor;
        }

        ////good method for validation when adding new course
        public bool IsNotDuplicateInstructor(POCO.Instructor ins, ref List<string> errors)
        {
            var db_instructor = new instructor();

            try
            {
                var isDuplicate = this.context.instructors.Where(
                    x => x.first_name == ins.FirstName &&
                    x.last_name == ins.LastName &&
                    x.title == ins.Title).Count() > 0;

                if (isDuplicate)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                errors.Add("Error occured in in InstructorRepository.IsDuplicateInstructor: " + e);
            }

            return false;
        }

        public void UpdateInstructor(POCO.Instructor ins, ref List<string> errors)
        {
            var db_instructor = new instructor();
            var db_staff = new staff();

            try
            {
                db_instructor = this.context.instructors.Find(ins.InstructorId);
                db_staff = this.context.staffs.Find(ins.InstructorId);
                db_instructor.first_name = ins.FirstName;
                db_instructor.last_name = ins.LastName;
                db_instructor.title = ins.Title;
                db_staff.First = ins.FirstName;
                db_staff.Last = ins.LastName;
                db_staff.email = ins.FirstName.ToLower().Substring(0, 1) + ins.LastName.ToLower() + "@ucsd.edu";
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in InstructorRepository.UpdateInstructor: " + e);
            }
        }

        public void AddInstructor(POCO.Instructor ins, ref List<string> errors)
        {
            var db_instructor = new instructor();
            var db_staff = new staff();

            try
            {
                db_instructor.first_name = ins.FirstName;
                db_instructor.last_name = ins.LastName;
                db_instructor.title = ins.Title;
                db_staff.First = ins.FirstName;
                db_staff.Last = ins.LastName;
                db_staff.email = ins.FirstName.ToLower().Substring(0, 1) + ins.LastName.ToLower() + "@ucsd.edu";
                db_staff.password = ins.Password;
                this.context.instructors.Add(db_instructor);
                this.context.staffs.Add(db_staff);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in InstructorRepository.AddInstructor: " + e);
            }
        }

        public void RemoveInstructor(int instructor_id, ref List<string> errors)
        {
            var db_instructor = new instructor();
            var db_staff = new staff();

            try
            {
                db_instructor = this.context.instructors.Find(instructor_id);
                db_staff = this.context.staffs.Find(instructor_id);
                this.context.staffs.Remove(db_staff);
                this.context.instructors.Remove(db_instructor);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in InstructorRepository.RemoveInstructor: " + e);
            }
        }

        public List<Instructor> GetInstructorList(ref List<string> errors)
        {
            List<POCO.Instructor> pocoInstructorList = new List<POCO.Instructor>();
            List<instructor> db_instructorList;
            try
            {
                db_instructorList = this.context.instructors.ToList();

                foreach (instructor i_instructor in db_instructorList)
                {
                    var tempPoco = new POCO.Instructor();
                    tempPoco.InstructorId = i_instructor.instructor_id;
                    tempPoco.FirstName = i_instructor.first_name;
                    tempPoco.LastName = i_instructor.last_name;
                    tempPoco.Title = i_instructor.title;
                    
                    pocoInstructorList.Add(tempPoco);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error occured in InstructorRepository.GetInstructorList: " + e);
            }

            return pocoInstructorList;
        }

        public void AssignGradeToStudent(Schedule schedule, string studentId, int instructorId, string grade, ref List<string> errors)
        {
            var db_enrollment = new enrollment();
            int scheduleId;

            try
            {
                scheduleId = this.context.course_schedule.Where(
                    y => y.quarter == schedule.Quarter && y.year == int.Parse(schedule.Year) &&
                    y.instructor_id == instructorId).Select(x => x.schedule_id).First();
                db_enrollment = this.context.enrollments.Where(x => x.student_id == studentId && x.schedule_id == scheduleId).First();
                db_enrollment.grade = grade;
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in InstructorRepository.AssignGradeToStudent: " + e);
            }
        }

        public void ApprovePreReqOverride(int scheduleId, string studentId, ref List<string> errors)
        {
            preReq_Override db_PreReqRequest = new preReq_Override();

            try
            {
                db_PreReqRequest.schedule_id = scheduleId;
                db_PreReqRequest.student_id = studentId;
                db_PreReqRequest = this.context.preReq_Override.Find(db_PreReqRequest);
                db_PreReqRequest.approved = true;
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in InstructorRepository.ApprovePreReqOverride: " + e);
            }
        }
    }
}
