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
                errors.Add("Error occured in TaRepository.FindTaByName: " + e);
            }

            return pocoTa;
        }

        public Ta FindTaById(int ta_id, ref List<string> errors)
        {
            POCO.Ta pocoTa = new POCO.Ta();
            TeachingAssistant db_Ta;
            try
            {
                db_Ta = this.context.TeachingAssistants.Find(ta_id);
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
                errors.Add("Error occured in TaRepository.FindTaById: " + e);
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
                errors.Add("Error occured in TaRepository.IsDuplicateCourse: " + e);
            }

            return true;
        }

        public void UpdateTa(POCO.Ta ta, ref List<string> errors)
        {
            var db_Ta = new TeachingAssistant();

            try
            {
                db_Ta = this.context.TeachingAssistants.Find(ta.TaId);
                db_Ta.ta_type_id = int.Parse(ta.TaType);
                db_Ta.first = ta.FirstName;
                db_Ta.last = ta.LastName;
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in TaRepository.UpdateTa: " + e);
            }
        }

        public void AddTa(POCO.Ta ta, ref List<string> errors)
        {
            var db_Ta = new TeachingAssistant();

            try
            {
                db_Ta.ta_type_id = int.Parse(ta.TaType);
                db_Ta.first = ta.FirstName;
                db_Ta.last = ta.LastName;
                this.context.TeachingAssistants.Add(db_Ta);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in TaRepository.AddTa: " + e);
            }
        }

        public void RemoveTa(int ta_id, ref List<string> errors)
        {
            var db_Ta = new TeachingAssistant();

            try
            {
                db_Ta = this.context.TeachingAssistants.Find(ta_id);
                db_Ta = this.context.TeachingAssistants.Remove(db_Ta);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in TaRepository.RemoveTa: " + e);
            }
        }

        public List<Ta> GetTaList(ref List<string> errors)
        {
            List<POCO.Ta> pocoTaList = new List<POCO.Ta>();
            List<TeachingAssistant> db_TaList;
            try
            {
                db_TaList = this.context.TeachingAssistants.Include("TeachingAssistantType").ToList();

                foreach (TeachingAssistant i_ta in db_TaList)
                {
                    var tempPoco = new POCO.Ta();
                    tempPoco.TaId = i_ta.ta_id;
                    tempPoco.TaType = i_ta.TeachingAssistantType.ta_type_desc;
                    tempPoco.FirstName = i_ta.first;
                    tempPoco.LastName = i_ta.last;
                    pocoTaList.Add(tempPoco);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error occured in TaRepository.GetTaList: " + e);
            }

            return pocoTaList;
        }

        public bool IsNotDuplicateTa(Ta ta, ref List<string> errors)
        {
            var db_Ta = new TeachingAssistant();

            try
            {
                var ta_Type = int.Parse(ta.TaType);
                var isDuplicate = this.context.TeachingAssistants.Where(
                    x => x.first == ta.FirstName &&
                    x.last == ta.LastName &&
                    x.ta_type_id == ta_Type).Count() > 0;

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
                errors.Add("Error occured in TaRepository.IsDuplicateTa: " + e);
            }

            return false;
        }
    }
}
