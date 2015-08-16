namespace IRepository
{
    using System.Collections.Generic;
    using POCO;

    public interface ITaRepository
    {
        Ta FindTaByName(string ta_name, ref List<string> errors);

        Ta FindTaById(string ta_id, ref List<string> errors);

        bool IsDuplicateTa(Ta ta, ref List<string> errors);

        void UpdateTa(Ta ta, ref List<string> errors);
        
        void AddTa(Ta ta, ref List<string> errors);

        void RemoveTa(int ta_id, ref List<string> errors);

        List<Ta> GetTaList(ref List<string> errors);
    }
}
