using System.Collections.Generic;
using POCO;

namespace IRepository
{
    interface ITaRepository
    {
        Ta FindTaByName(string _TaName, ref List<string> errors);
        Ta FindTaById(string _TaId, ref List<string> errors);

        bool IsDuplicateTa(Ta _Ta, ref List<string> errors);
        void UpdateTa(Ta _Ta, ref List<string> errors);
        void AddTa(Ta _Ta, ref List<string> errors);
        void RemoveTa(Ta _Ta, ref List<string> errors);

        List<Ta> GetTaList(ref List<string> errors);
    }
}
