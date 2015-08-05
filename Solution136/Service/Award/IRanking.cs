// <copyright file="IRanking.cs" company="University of California San Diego">
// University of California San Diego. CSE 136
// </copyright>
namespace Service.Award
{
    /// <summary>
    /// interface for ranking
    /// </summary>
    public interface IRanking
    {
        /// <summary>
        /// Get rank score
        /// </summary>
        /// <param name="studentId">student id</param>
        /// <returns>ranking score</returns>
        int GetRankScore(string studentId);
    }
}
