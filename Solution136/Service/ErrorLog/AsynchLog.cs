// <copyright file="AsynchLog.cs" company="University of California San Diego">
// University of California San Diego. CSE 136
// </copyright>
namespace Service.ErrorLog
{
    using System.Collections.Generic;

    /// <summary>
    /// asynchronous log class definition
    /// </summary>
    public class AsynchLog
    {
        /// <summary>
        /// delete method definition for asynchronous call
        /// </summary>
        /// <param name="errors">error list</param>
        public delegate void MethodDelegate(List<string> errors);

        /// <summary>
        /// Low now method
        /// </summary>
        /// <param name="errors">error list</param>
        public static void LogNow(List<string> errors)
        {
            var log = new ErrorLogFactory().GetErrorLogInstance();
            var callGenerateFileAsync = new MethodDelegate(log.LogError);
            callGenerateFileAsync.BeginInvoke(errors, null, null);
        }
    }
}
