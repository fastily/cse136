// <copyright file="Resolver.cs" company="University of California San Diego">
// University of California San Diego. CSE 136
// </copyright>
namespace Service.Award
{
    /// <summary>
    /// Resolver class for dependency injection example
    /// </summary>
    public class Resolver
    {
        /// <summary>
        /// Choose ranking
        /// </summary>
        /// <param name="studentType">student type</param>
        /// <returns>ranking implementation</returns>
        public IRanking ChooseRanking(string studentType)
        {
            if (studentType == "grad")
            {
                return new RankByInnovation();
            }

            return new RankByGPA();
        }
    }
}