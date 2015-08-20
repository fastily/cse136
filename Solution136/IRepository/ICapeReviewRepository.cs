namespace IRepository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using POCO;

    public interface ICapeReviewRepository
    {
        void InsertCape(CapeReview cr, ref List<string> errors);

        void DeleteCapeReview(int cape_id, ref List<string> errors);

        //// CapeReview FindCapeReviewById(int cape_id, ref List<string> errors);

        CapeReview FindCapeReviewByCourseId(int cape_id, ref List<string> errors);

        ////find by schedule or course Id

        void UpdateCapeReview(CapeReview cr, ref List<string> errors);
    }
}
