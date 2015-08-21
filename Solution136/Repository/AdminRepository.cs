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

    public class AdminRepository : BaseRepository, IAdminRepository
    {
        private cse136Entities context;

        public AdminRepository(cse136Entities entities)
        {
            this.context = entities;
        }

        public Admin FindAdminById(int admin_id, ref List<string> errors)
        {
            POCO.Admin pocoAdmin = new POCO.Admin();
            admin db_Admin;
            try
            {
                db_Admin = this.context.admins.Find(admin_id);
                if (db_Admin != null)
                {
                    pocoAdmin.Id = db_Admin.admin_id;
                    pocoAdmin.FirstName = db_Admin.First;
                    pocoAdmin.LastName = db_Admin.Last;
                }
            }
            catch (Exception e)
            {
                errors.Add("Error occured in AdminRepository.FindAdminById: " + e);
            }

            return pocoAdmin;
        }

        ////good method for validation when adding new course
        public bool IsNotDuplicateAdmin(POCO.Admin adminPoco, ref List<string> errors)
        {
            var db_Admin = new admin();

            try
            {
                db_Admin = this.context.admins.Find(db_Admin);

                if (db_Admin == null)
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
                errors.Add("Error occured in AdminRepository.IsDuplicateAdmin: " + e);
            }

            return true;
        }

        public void UpdateAdmin(POCO.Admin adminPoco, ref List<string> errors)
        {
            var db_Admin = new admin();

            try
            {
                db_Admin = this.context.admins.Find(db_Admin);
                db_Admin.First = adminPoco.FirstName;
                db_Admin.Last = adminPoco.LastName;
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in AdminRepository.UpdateAdmin: " + e);
            }
        }

        public void AddAdmin(POCO.Admin pocoAdmin, ref List<string> errors)
        {
            admin db_Admin = new admin();
            try
            {
                db_Admin.First = pocoAdmin.FirstName;
                db_Admin.Last = pocoAdmin.LastName;
                this.context.admins.Add(db_Admin);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in AdminRepository.AddAdminById: " + e);
            }
        }

        public void RemoveAdmin(int admin_id, ref List<string> errors)
        {
            var db_Admin = new admin();

            try
            {
                db_Admin.admin_id = admin_id;
                db_Admin = this.context.admins.Remove(db_Admin);
                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                errors.Add("Error occured in TaRepository.FindTaById: " + e);
            }
        }

        public List<Admin> GetAdminList(ref List<string> errors)
        {
            List<POCO.Admin> pocoAdminList = new List<POCO.Admin>();
            IEnumerable<admin> db_AdminList;
            try
            {
                db_AdminList = this.context.admins;

                foreach (admin i_admin in db_AdminList)
                {
                    var tempPoco = new POCO.Admin();
                    tempPoco.Id = i_admin.admin_id;
                    tempPoco.FirstName = i_admin.First;
                    tempPoco.LastName = i_admin.Last;
                    pocoAdminList.Add(tempPoco);
                }
            }
            catch (Exception e)
            {
                errors.Add("Error occured in TaRepository.GetTaList: " + e);
            }

            return pocoAdminList;
        }
    }
}
