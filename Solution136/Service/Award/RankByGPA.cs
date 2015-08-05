// <copyright file="RankByGPA.cs" company="University of California San Diego">
// University of California San Diego. CSE 136
// </copyright>
namespace Service.Award
{
    /// <summary>
    /// Rank by GPA implementation 
    /// </summary>
    public class RankByGPA : IRanking
    {
        /// <summary>
        /// Get rank score
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns>ranking score</returns>
        public int GetRankScore(string id)
        {
            // retrieve gpa data from database and calculate a ranking between 1-100
            // hardcode for now.
            return 100;
        }
    }
}