// <copyright file="LogToDB.cs" company="University of California San Diego">
// University of California San Diego. CSE 136
// </copyright>
namespace Service.ErrorLog
{
    using System.Collections.Generic;

    /// <summary>
    /// log to the database
    /// </summary>
    public class LogToDb : IErrorLog
    {
        /// <summary>
        /// log error method implementation
        /// </summary>
        /// <param name="errorList">error list</param>
        public void LogError(List<string> errorList)
        {
            // 136 Students TODO: write to database table.
        }
    }
}
