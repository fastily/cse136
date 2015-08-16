namespace Repository
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
                ////foreach (enrollment enrolledCourse in dbStudent.enrollments)
                ////{
                ////    pocoStudent.Enrolled.Add(enrolledCourse);
                ////}
                ////pocoStudent.Enrolled = dbStudent.enrollments;
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
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
                errors.Add("Error: " + e);
            }
        }

        public void UpdateStudent(Student s, ref List<string> errors)
        {
            var db_student = new student();
            Student pocoStudent = new Student();

            try
            {
                db_student.student_id = s.StudentId;
                db_student = this.context.students.Find(db_student);
                db_student.first_name = s.FirstName;
                db_student.last_name = s.LastName;
                db_student.password = s.Password;
                db_student.email = s.Email;
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }

        public void DeleteStudent(string student_id, ref List<string> errors)
        {
            var db_student = new student();

            try
            {
                db_student.student_id = student_id;
                this.context.students.Remove(db_student);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
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
                                             CourseId = dataSet.Tables[1].Rows[i]["course_id"].ToString(),
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
                errors.Add("Error: " + e);
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
                errors.Add("Error: " + e);
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
                errors.Add("Error: " + e);
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
                errors.Add("Error: " + e);
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
                errors.Add("Error: " + e);
            }

            return poco_EnrollmentList;
        }

        public void RequestPreReqOverride(int scheduleId, string studentId, ref List<string> errors)
        {
            preReq_Override db_PreReqRequest = new preReq_Override();

            try
            {

            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }

        public void GetStudentHistory(){

        }
    }
}
