namespace Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using IRepository;
    using POCO;

    public class TaRepository : BaseRepository, ITaRepository
    {
        private cse136Entities context;

        public TaRepository(cse136Entities entities)
        {
            this.context = entities;
        }

        public Ta FindTaByName(string ta_name, ref List<string> errors)
        {
            POCO.Ta pocoTa = new POCO.Ta();
            TeachingAssistant db_Ta;
            try
            {
                db_Ta = this.context.TeachingAssistants.Where(x => x.first == ta_name).First();
                if (db_Ta != null)
                {
                    pocoTa.TaId = db_Ta.ta_id;
                    pocoTa.TaType = db_Ta.ta_type_id.ToString();
                    pocoTa.FirstName = db_Ta.first;
                    pocoTa.LastName = db_Ta.last;
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }

            return pocoTa;
        }

        ////TODO :: shouldn't call this method unless we know course exists
        public Ta FindTaById(string ta_name, ref List<string> errors)
        {
            POCO.Ta pocoTa = new POCO.Ta();
            TeachingAssistant db_Ta;
            try
            {
                db_Ta = this.context.TeachingAssistants.Find(ta_name);
                if (db_Ta != null)
                {
                    pocoTa.TaId = db_Ta.ta_id;
                    pocoTa.TaType = db_Ta.ta_type_id.ToString();
                    pocoTa.FirstName = db_Ta.first;
                    pocoTa.LastName = db_Ta.last;
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }

            return pocoTa;
        }

        ////good method for validation when adding new course
        public bool IsDuplicateCourse(POCO.Ta ta, ref List<string> errors)
        {
            var db_Ta = new TeachingAssistant();

            try
            {
                db_Ta = this.context.TeachingAssistants.Find(db_Ta);

                if (db_Ta == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }

            return true;
        }

        ////Unsure If We need to get course before updating... TODO
        //// Not changing id - PK
        public void UpdateTa(POCO.Ta ta, ref List<string> errors)
        {
            var db_Ta = new TeachingAssistant();

            try
            {
                ////might have to retrieve course then update, but I dont think so
                db_Ta.ta_type_id = int.Parse(ta.TaType); // shouldn't the taType be stored as int in POCO as well ?
                db_Ta.first = ta.FirstName;
                db_Ta.last = ta.LastName;
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }

        public void AddTa(POCO.Ta ta, ref List<string> errors)
        {
            var db_Ta = new TeachingAssistant();

            try
            {
                db_Ta.ta_type_id = int.Parse(ta.TaType); // shouldn't the taType be stored as int in POCO as well ?
                db_Ta.first = ta.FirstName;
                db_Ta.last = ta.LastName;
                this.context.TeachingAssistants.Add(db_Ta);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }

        // Is this how remove should be done? Using preset?
        public void RemoveTa(POCO.Ta ta, ref List<string> errors)
        {
            var db_Ta = new TeachingAssistant();

            try
            {
                db_Ta.ta_type_id = int.Parse(ta.TaType); // shouldn't the taType be stored as int in POCO as well ?
                db_Ta.first = ta.FirstName;
                db_Ta.last = ta.LastName;
                this.context.TeachingAssistants.Remove(db_Ta);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error: " + e);
            }
        }

        public List<Ta> GetTaList(ref List<string> errors)
        {
            List<POCO.Ta> pocoTaList = new List<POCO.Ta>();
            List<TeachingAssistant> db_TaList;
            try
            {
                db_TaList = this.context.TeachingAssistants.ToList();

                foreach (TeachingAssistant i_ta in db_TaList)
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

        public bool IsDuplicateTa(Ta ta, ref List<string> errors)
        {
            return true;
        }
    }
}
