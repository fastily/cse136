// <copyright file="IErrorLog.cs" company="University of California San Diego">
// University of California San Diego. CSE 136
// </copyright>
namespace Service.ErrorLog
{
    using System.Collections.Generic;

    /// <summary>
    /// error log interface
    /// </summary>
    public interface IErrorLog
    {
        /// <summary>
        /// log error method
        /// </summary>
        /// <param name="errorList">error list</param>
        void LogError(List<string> errorList);
    }
}
