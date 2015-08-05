// <copyright file="RankByInnovation.cs" company="University of California San Diego">
// University of California San Diego. CSE 136
// </copyright>
namespace Service.Award
{
    /// <summary>
    /// Rank by innovation implementation
    /// </summary>
    public class RankByInnovation : IRanking
    {
        /// <summary>
        /// Get ranking score
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns>ranking score</returns>
        public int GetRankScore(string id)
        {
            // retrieve innovation info from database & calculate innovation from 1 to 100.
            // hardcode for now.
            return 90;
        }
    }
}