using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using IRepository;
    using POCO;

    class TaRepository : BaseRepository, ITaRepository
    {
        private cse136Entities _context;

        public TaRepository(cse136Entities _cse136Entities)
        {
            _context = _cse136Entities;
        }

        public Ta FindTaByName(string _TaName, ref List<string> errors)
        {
            POCO.Ta pocoTa = new POCO.Ta();
            TeachingAssistant dbTa;
            try
            {
                dbTa = _context.TeachingAssistants.Where(x => x.first == _TaName).First();
                if (dbTa != null)
                {
                    pocoTa.TaId = dbTa.ta_id;
                    pocoTa.TaType = dbTa.ta_type_id.ToString();
                    pocoTa.FirstName = dbTa.first;
                    pocoTa.LastName = dbTa.last;
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }

            return pocoTa;
        }

        //TODO :: shouldn't call this method unless we know course exists
        public Ta FindTaById(string _TaName, ref List<string> errors)
        {
            POCO.Ta pocoTa = new POCO.Ta();
            TeachingAssistant dbTa;
            try
            {
                dbTa = _context.TeachingAssistants.Find(_TaName);
                if (dbTa != null)
                {
                    pocoTa.TaId = dbTa.ta_id;
                    pocoTa.TaType = dbTa.ta_type_id.ToString();
                    pocoTa.FirstName = dbTa.first;
                    pocoTa.LastName = dbTa.last;
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }

            return pocoTa;
        }

        //good method for validation when adding new course
        public bool IsDuplicateCourse(POCO.Ta _ta, ref List<string> errors)
        {
            var dbTa = new TeachingAssistant();

            try
            {
                dbTa = _context.TeachingAssistants.Find(dbTa);

                if (dbTa == null)
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
        public void UpdateTa(POCO.Ta  _ta, ref List<string> errors)
        {
            var dbTa = new TeachingAssistant();

            try
            {
                //might have to retrieve course then update, but I dont think so
                dbTa.ta_type_id = Int32.Parse(_ta.TaType); // shouldn't the taType be stored as int in POCO as well ?
                dbTa.first = _ta.FirstName;
                dbTa.last = _ta.LastName;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }

        public void AddTa(POCO.Ta _ta, ref List<string> errors)
        {
            var dbTa = new TeachingAssistant();

            try
            {
                dbTa.ta_type_id = Int32.Parse(_ta.TaType); // shouldn't the taType be stored as int in POCO as well ?
                dbTa.first = _ta.FirstName;
                dbTa.last = _ta.LastName;
                _context.TeachingAssistants.Add(dbTa);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }

        // Is this how remove should be done? Using preset?
        public void RemoveTa(POCO.Ta  _ta, ref List<string> errors)
        {
            var dbTa = new TeachingAssistant();

            try
            {
                dbTa.ta_type_id = Int32.Parse(_ta.TaType); // shouldn't the taType be stored as int in POCO as well ?
                dbTa.first = _ta.FirstName;
                dbTa.last = _ta.LastName;
                _context.TeachingAssistants.Remove(dbTa);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }

        public List<Ta> GetTaList(ref List<string> errors)
        {
            List<POCO.Ta> pocoTaList = new List<POCO.Ta>();
            List<TeachingAssistant> dbTaList;
            try
            {
                dbTaList = _context.TeachingAssistants.ToList();

                foreach (TeachingAssistant i_ta in dbTaList)
                {
                    var tempPoco = new POCO.Ta();
                    tempPoco.TaId = i_ta.ta_id;
                    tempPoco.TaType = i_ta.ta_type_id.ToString();
                    tempPoco.FirstName = i_ta.first;
                    tempPoco.LastName = i_ta.last;
                    pocoTaList.Add(tempPoco);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }

            return pocoTaList;
        }

        public bool IsDuplicateTa(Ta _Ta, ref List<string> errors)
        {
            return true;
        }
    }
}
