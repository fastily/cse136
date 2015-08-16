namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using IRepository;
    using POCO;

    class InstructorRepository : BaseRepository, IInstructorRepository
    {
        private cse136Entities _context;

        public InstructorRepository(cse136Entities _cse136Entities)
        {
            _context = _cse136Entities;
        }

        public Instructor FindInstructorByName(string _InstName, ref List<string> errors)
        {
            POCO.Instructor pocoInstructor = new POCO.Instructor();
            instructor dbInstructor;
            try
            {
                dbInstructor = _context.instructors.Where(x => x.first_name == _InstName).First();
                if (dbInstructor != null)
                {
                    pocoInstructor.InstructorId = dbInstructor.instructor_id;
                    pocoInstructor.FirstName = dbInstructor.first_name;
                    pocoInstructor.LastName = dbInstructor.last_name;
                    pocoInstructor.Title = dbInstructor.title;
                    pocoInstructor.Email = dbInstructor.email;
                    pocoInstructor.Password = dbInstructor.password;
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }

            return pocoInstructor;
        }

        //TODO :: shouldn't call this method unless we know course exists
        public Instructor FindInstructorById(string _InstName, ref List<string> errors)
        {
            POCO.Instructor pocoInstructor = new POCO.Instructor();
            instructor dbInstructor;
            try
            {
                dbInstructor = _context.instructors.Find(_InstName);
                if (dbInstructor != null)
                {
                    pocoInstructor.InstructorId = dbInstructor.instructor_id;
                    pocoInstructor.FirstName = dbInstructor.first_name;
                    pocoInstructor.LastName = dbInstructor.last_name;
                    pocoInstructor.Title = dbInstructor.title;
                    pocoInstructor.Email = dbInstructor.email;
                    pocoInstructor.Password = dbInstructor.password;
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }

            return pocoInstructor;
        }

        //good method for validation when adding new course
        public bool IsDuplicateInstructor(POCO.Instructor _instructor, ref List<string> errors)
        {
            var dbInstructor = new instructor();

            try
            {
                dbInstructor = _context.instructors.Find(dbInstructor);

                if (dbInstructor == null)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }

            return true;

        }

        //Unsure If We need to get course before updating... TODO
        // Not changing id - PK
        public void UpdateInstructor(POCO.Instructor _instructor, ref List<string> errors)
        {
            var dbInstructor = new instructor();

            try
            {
                //might have to retrieve course then update, but I dont think so
                dbInstructor.instructor_id = _instructor.InstructorId;
                dbInstructor.first_name = _instructor.FirstName;
                dbInstructor.last_name = _instructor.LastName;
                dbInstructor.title = _instructor.Title;
                dbInstructor.email = _instructor.Email;
                dbInstructor.password = _instructor.Password;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }

        public void AddInstructor(POCO.Instructor _instructor, ref List<string> errors)
        {
            var dbInstructor = new instructor();

            try
            {
                dbInstructor.instructor_id = _instructor.InstructorId; 
                dbInstructor.first_name = _instructor.FirstName;
                dbInstructor.last_name = _instructor.LastName;
                dbInstructor.title = _instructor.Title;
                dbInstructor.email = _instructor.Email;
                dbInstructor.password = _instructor.Password;
                _context.instructors.Add(dbInstructor);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }

        // Is this how remove should be done? Using preset?
        public void RemoveInstructor(POCO.Instructor _instructor, ref List<string> errors)
        {
            var dbInstructor = new instructor();

            try
            {
                dbInstructor.instructor_id = _instructor.InstructorId;
                dbInstructor.first_name = _instructor.FirstName;
                dbInstructor.last_name = _instructor.LastName;
                dbInstructor.title = _instructor.Title;
                dbInstructor.email = _instructor.Email;
                dbInstructor.password = _instructor.Password;
                _context.instructors.Remove(dbInstructor);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }

        public List<Instructor> GetInstructorList(ref List<string> errors)
        {
            List<POCO.Instructor> pocoInstructorList = new List<POCO.Instructor>();
            List<instructor> dbInstructorList;
            try
            {
                dbInstructorList = _context.instructors.ToList();

                foreach (instructor i_instructor in dbInstructorList)
                {
                    var tempPoco = new POCO.Instructor();
                    tempPoco.InstructorId = i_instructor.instructor_id;
                    tempPoco.FirstName = i_instructor.first_name;
                    tempPoco.LastName = i_instructor.last_name;
                    tempPoco.Title = i_instructor.title;
                    tempPoco.Email = i_instructor.email;
                    tempPoco.Password= i_instructor.password;
                    
                    pocoInstructorList.Add(tempPoco);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }

            return pocoInstructorList;
        }
    }
}
