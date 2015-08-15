namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using IRepository;
    using POCO;
    using System.Linq;

    public class StudentRepository : BaseRepository, IStudentRepository
    {
        private cse136Entities _context;

        public StudentRepository(cse136Entities _cse136Entities)
        {
            _context = _cse136Entities;
        }
        private const string InsertStudentInfoProcedure = "spInsertStudentInfo";
        private const string UpdateStudentInfoProcedure = "spUpdateStudentInfo";
        private const string DeleteStudentInfoProcedure = "spDeleteStudentInfo";
        private const string GetStudentListProcedure = "spGetStudentList";
        private const string GetStudentInfoProcedure = "spGetStudentInfo";
        private const string InsertStudentScheduleProcedure = "spInsertStudentSchedule";
        private const string DeleteStudentScheduleProcedure = "spDeleteStudentSchedule";

        public Student FindStudentById(int StudentId, ref List<string> errors)
        {
            student dbStudent;
            Student pocoStudent = new Student();

            try
            {
                dbStudent = _context.students.Find(StudentId);
                pocoStudent.StudentId = dbStudent.student_id;
                pocoStudent.FirstName = dbStudent.first_name;
                pocoStudent.LastName = dbStudent.last_name;
                pocoStudent.Password = dbStudent.password;
                pocoStudent.Email = dbStudent.email;
                //foreach (enrollment enrolledCourse in dbStudent.enrollments)
                //{
                //    pocoStudent.Enrolled.Add(enrolledCourse);
                //}
                //pocoStudent.Enrolled = dbStudent.enrollments;
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }

            return pocoStudent;
        }

        public student FindEntityStudentById(int StudentId, ref List<string> errors)
        {
            student dbStudent = new student();

            try
            {
                dbStudent = _context.students.Find(StudentId);
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }

            return dbStudent;
        }

        public void InsertStudent(Student _Student, ref List<string> errors)
        {
            var dbStudent = new student();

            try
            {
                dbStudent.student_id = _Student.StudentId;
                dbStudent.first_name = _Student.FirstName;
                dbStudent.last_name = _Student.LastName;
                dbStudent.password = _Student.Password;
                dbStudent.email = _Student.Email;
                _context.students.Add(dbStudent);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }

        //need to figure out how to do this method
        public void UpdateStudent(Student _Student, ref List<string> errors)
        {
            var dbStudent = new student();
            Student pocoStudent = new Student();

            try
            {

                //dbStudent = FindEntityStudentById(_Student.StudentId, ref List<string> errors);
                dbStudent.student_id = _Student.StudentId;
                dbStudent.first_name = _Student.FirstName;
                dbStudent.last_name = _Student.LastName;
                dbStudent.password = _Student.Password;
                dbStudent.email = _Student.Email;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }

        public void DeleteStudent(string id, ref List<string> errors)
        {
            var conn = new SqlConnection(ConnectionString);

            try
            {
                var adapter = new SqlDataAdapter(DeleteStudentInfoProcedure, conn)
                                  {
                                      SelectCommand =
                                          {
                                              CommandType =
                                                  CommandType
                                                  .StoredProcedure
                                          }
                                  };
                adapter.SelectCommand.Parameters.Add(new SqlParameter("@student_id", SqlDbType.VarChar, 20));

                adapter.SelectCommand.Parameters["@student_id"].Value = id;

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
            List<student> dbStudentList;
            try
            {
                dbStudentList = _context.students.ToList();

                foreach (student i_student in dbStudentList)
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

        public List<Enrollment> GetEnrollments(string studentId)
        {
            //// Not implemented yet. 136 TODO:
            throw new Exception();
        }
    }
}
