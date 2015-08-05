// <copyright file="LogToFile.cs" company="University of California San Diego">
// University of California San Diego. CSE 136
// </copyright>
namespace Service.ErrorLog
{
    using System.Collections.Generic;

    /// <summary>
    /// log to file object
    /// </summary>
    public class LogToFile : IErrorLog
    {
        /// <summary>
        /// log actual error
        /// </summary>
        /// <param name="errorList">error list</param>
        public void LogError(List<string> errorList)
        {
            // 136 Students TODO: Write to local file system somewhere
        }
    }
}
