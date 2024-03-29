﻿namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using IRepository;
    using POCO;

    public class StudentRepository : BaseRepository, IStudentRepository
    {
        private const string InsertStudentInfoProcedure = "spInsertStudentInfo";
        private const string UpdateStudentInfoProcedure = "spUpdateStudentInfo";
        private const string DeleteStudentInfoProcedure = "spDeleteStudentInfo";
        private const string GetStudentListProcedure = "spGetStudentList";
        private const string GetStudentInfoProcedure = "spGetStudentInfo";
        private const string InsertStudentScheduleProcedure = "spInsertStudentSchedule";
        private const string DeleteStudentScheduleProcedure = "spDeleteStudentSchedule";

        private cse136Entities context;

        public StudentRepository(cse136Entities entities)
        {
            this.context = entities;
        }

        public Student FindStudentById(int student_id, ref List<string> errors)
        {
            student db_student;
            Student pocoStudent = new Student();

            try
            {
                db_student = this.context.students.Find(student_id);
                pocoStudent.StudentId = db_student.student_id;
                pocoStudent.FirstName = db_student.first_name;
                pocoStudent.LastName = db_student.last_name;
                pocoStudent.Password = db_student.password;
                pocoStudent.Email = db_student.email;
                pocoStudent.Enrolled = new List<Schedule>();
                foreach (enrollment enrolledCourse in db_student.enrollments)
                {
                    var pocoSchedule = new Schedule();
                    var db_Schedule = new course_schedule();

                    db_Schedule.schedule_id = enrolledCourse.schedule_id;
                    db_Schedule = this.context.course_schedule.Find(db_Schedule);

                    pocoSchedule.ScheduleId = db_Schedule.schedule_id;
                    pocoSchedule.Quarter = db_Schedule.quarter;
                    pocoSchedule.Year = db_Schedule.year.ToString();
                    pocoSchedule.Session = db_Schedule.session;

                    pocoStudent.Enrolled.Add(pocoSchedule);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error occured in StudentRepository.FindStudentById: " + e);
            }

            return pocoStudent;
        }

        public void InsertStudent(Student s, ref List<string> errors)
        {
            var db_student = new student();

            try
            {
                db_student.student_id = s.StudentId;
                db_student.first_name = s.FirstName;
                db_student.last_name = s.LastName;
                db_student.password = s.Password;
                db_student.email = s.Email;
                this.context.students.Add(db_student);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in StudentRepository.InsertStudent: " + e);
            }
        }

        public void UpdateStudent(Student s, ref List<string> errors)
        {
            var db_student = new student();

            try
            {
                db_student = this.context.students.Find(s.StudentId); 
                db_student.first_name = s.FirstName;
                db_student.last_name = s.LastName;
                db_student.password = s.Password;
                db_student.email = s.Email;
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in StudentRepository.UpdateStudent: " + e);
            }
        }

        public void DeleteStudent(string student_id, ref List<string> errors)
        {
            var db_student = new student();

            try
            {
                db_student = this.context.students.Find(student_id);
                this.context.students.Remove(db_student);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in StudentRepository.DeleteStudent: " + e);
            }
        }

        public Student GetStudentDetail(string id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);
            Student student = null;

            try
            {
                var adapter = new SqlDataAdapter(GetStudentInfoProcedure, conn)
                {
                    SelectCommand = { CommandType = CommandType.StoredProcedure }
                };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));

                adapter.SelectCommand.Parameters["@student_id"].Value = id;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                if (dataSet.Tables[0].Rows.Count == 0)
                {
                    return null;
                }

                student = new Student
                              {
                                  StudentId = dataSet.Tables[0].Rows[0]["student_id"].ToString(),
                                  FirstName = dataSet.Tables[0].Rows[0]["first_name"].ToString(),
                                  LastName = dataSet.Tables[0].Rows[0]["last_name"].ToString(),
                                  Email = dataSet.Tables[0].Rows[0]["email"].ToString(),
                                  Password = dataSet.Tables[0].Rows[0]["password"].ToString()
                              };

                if (dataSet.Tables[1] != null)
                {
                    student.Enrolled = new List<Schedule>();
                    for (var i = 0; i < dataSet.Tables[1].Rows.Count; i++)
                    {
                        var schedule = new Schedule();
                        var course = new Course
                                         {
                                             CourseId = int.Parse(dataSet.Tables[1].Rows[i]["course_id"].ToString()),
                                             Title = dataSet.Tables[1].Rows[i]["course_title"].ToString(),
                                             Description =
                                                 dataSet.Tables[1].Rows[i]["course_description"].ToString()
                                         };
                        schedule.Course = course;

                        schedule.Quarter = dataSet.Tables[1].Rows[i]["quarter"].ToString();
                        schedule.Year = dataSet.Tables[1].Rows[i]["year"].ToString();
                        schedule.Session = dataSet.Tables[1].Rows[i]["session"].ToString();
                        schedule.ScheduleId = Convert.ToInt32(dataSet.Tables[1].Rows[i]["schedule_id"].ToString());
                        student.Enrolled.Add(schedule);
                    }
                }
            }
            catch (Exception e)
            {
                errors.Add("Error occured in StudentRepository.GetStudentDetail: " + e);
            }
            finally
            {
                conn.Dispose();
            }

            return student;
        }

        public List<Student> GetStudentList(ref List<string> errors)
        {
            List<POCO.Student> pocoStudentList = new List<POCO.Student>();
            List<student> db_studentList;
            try
            {
                db_studentList = this.context.students.ToList();

                foreach (student i_student in db_studentList)
                {
                    var tempPoco = new POCO.Student();
                    tempPoco.StudentId = i_student.student_id;
                    tempPoco.FirstName = i_student.first_name;
                    tempPoco.LastName = i_student.last_name;
                    tempPoco.StudentId = i_student.student_id;
                    tempPoco.Email = i_student.email;
                    pocoStudentList.Add(tempPoco);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error occured in StudentRepository.GetStudentList: " + e);
            }

            return pocoStudentList;
        }

        public void EnrollSchedule(string studentId, int scheduleId, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(InsertStudentScheduleProcedure, conn)
                                  {
                                      SelectCommand =
                                          {
                                              CommandType
                                                  =
                                                  CommandType
                                                  .StoredProcedure
                                          }
                                  };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@student_id"].Value = studentId;
                adapter.SelectCommand.Parameters["@schedule_id"].Value = scheduleId;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);
            }
            catch (Exception e)
            {
                errors.Add("Error occured in StudentRepository.EnrollSchedule : " + e);
            }
            finally
            {
                conn.Dispose();
            }
        }

        public void DropEnrolledSchedule(string studentId, int scheduleId, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(DeleteStudentScheduleProcedure, conn)
                                  {
                                      SelectCommand =
                                          {
                                              CommandType
                                                  =
                                                  CommandType
                                                  .StoredProcedure
                                          }
                                  };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@schedule_id", SqlDbType.Int));

                adapter.SelectCommand.Parameters["@student_id"].Value = studentId;
                adapter.SelectCommand.Parameters["@schedule_id"].Value = scheduleId;

                var dataSet = new DataSet();
                adapter.Fill(dataSet);
            }
            catch (Exception e)
            {
                errors.Add("Error occured in StudentRepository.DropEnrolledSchedule: " + e);
            }
            finally
            {
                conn.Dispose();
            }
        }

        public List<Enrollment> GetEnrollments(string studentId, ref List<string> errors)
        {
            IEnumerable<enrollment> db_EnrollmentList;
            List<Enrollment> poco_EnrollmentList = new List<Enrollment>();
            try
            {
                db_EnrollmentList = this.context.enrollments.Where(x => x.student_id == studentId);
                foreach (enrollment enrolledSchedule in db_EnrollmentList) 
                {
                    Enrollment poco_Enrollment = new Enrollment();
                    poco_Enrollment.Grade = enrolledSchedule.grade;
                    poco_Enrollment.ScheduleId = enrolledSchedule.schedule_id;
                    poco_Enrollment.StudentId = studentId;
                    poco_EnrollmentList.Add(poco_Enrollment);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error occured in StudentRepository.GetEnrollments: " + e);
            }

            return poco_EnrollmentList;
        }

        public void RequestPreReqOverride(int scheduleId, string studentId, ref List<string> errors)
        {
            preReq_Override db_PreReqRequest = new preReq_Override();

            try
            {
                db_PreReqRequest.schedule_id = scheduleId;
                db_PreReqRequest.student_id = studentId;
                db_PreReqRequest.approved = false;
                this.context.preReq_Override.Add(db_PreReqRequest);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in StudentRepository.RequestPreReqOverride: " + e);
            }
        }     

        public void GetStudentHistory(string studentId, ref List<string> errors)
        {
            IEnumerable<int> db_ScheduleIdList;
            List<course_schedule> db_CourseScheduleList = new List<course_schedule>();

            try
            {
                db_ScheduleIdList = this.context.enrollments.Where(x => x.student_id == studentId).Select(y => y.schedule_id);
                foreach (int scheduleId in db_ScheduleIdList)
                {
                    course_schedule tmpSchedule = new course_schedule();
                    tmpSchedule.schedule_id = scheduleId;
                    tmpSchedule = this.context.course_schedule.Find(tmpSchedule);
                    db_CourseScheduleList.Add(tmpSchedule);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }
    }
}
