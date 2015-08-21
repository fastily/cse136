namespace IRepository
{
    using System.Collections.Generic;
    using POCO;

    public interface IAdminRepository
    {
        Admin FindAdminById(int admin_id, ref List<string> errors);

        bool IsNotDuplicateAdmin(Admin admin, ref List<string> errors);

        void UpdateAdmin(Admin admin, ref List<string> errors);

        void AddAdmin(Admin admin, ref List<string> errors);

        void RemoveAdmin(int admin_id, ref List<string> errors);

        List<Admin> GetAdminList(ref List<string> errors);
    }
}
