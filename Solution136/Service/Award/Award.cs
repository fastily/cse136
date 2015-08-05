// <copyright file="Award.cs" company="University of California San Diego">
// University of California San Diego. CSE 136
// </copyright>
namespace Service.Award
{
    /// <summary>
    /// Award class
    /// </summary>
    public class Award
    {
        /// <summary>
        /// private variable for the ranking
        /// </summary>
        private readonly IRanking rank;

        /// <summary>
        /// Initializes a new instance of the <see cref="Award" /> class.
        /// </summary>
        /// <param name="ranking">ranking object</param>
        public Award(IRanking ranking)
        {
            this.rank = ranking;
        }

        /// <summary>
        /// Award scholarship
        /// </summary>
        /// <param name="student_id">student id</param>
        public void AwardScholarship(string student_id)
        {
            var score = this.rank.GetRankScore(student_id);

            if (score > 70)
            {
                // put some money in student's account
            }
        }
    }
}