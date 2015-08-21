namespace Service
{
    using System;
    using System.Collections.Generic;
    using IRepository;
    using POCO;

    public class AdminService
    {
        private readonly IAdminRepository repository;

        public AdminService(IAdminRepository repository)
        {
            this.repository = repository;
        }

        public void UpdateAdmin(Admin adminPoco, ref List<string> errors)
        {
            if (adminPoco == null)
            {
                errors.Add("Admin cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(adminPoco.FirstName))
            {
                errors.Add("Admin first name cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(adminPoco.LastName))
            {
                errors.Add("Admin last name cannot be null");
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(adminPoco.Id.ToString()))
            {
                errors.Add("Admin Id cannot be null");
                throw new ArgumentException();
            }

            this.repository.UpdateAdmin(adminPoco, ref errors);
        }

        public Admin GetAdminById(string adminId, ref List<string> errors)
        {
            if (string.IsNullOrEmpty(adminId))
            {
                errors.Add("Invalid admin_id");
                throw new ArgumentException();
            }

            return this.repository.FindAdminById(int.Parse(adminId), ref errors);
        }

        public List<Admin> GetAdminList(ref List<string> errors)
        {
            return this.repository.GetAdminList(ref errors);
        }      
    }
}
