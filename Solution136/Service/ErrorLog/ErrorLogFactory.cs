// <copyright file="ErrorLogFactory.cs" company="University of California San Diego">
// University of California San Diego. CSE 136
// </copyright>
namespace Service.ErrorLog
{
    using System.Configuration;

    /// <summary>
    /// error log factory 
    /// </summary>
    public class ErrorLogFactory
    {
        /// <summary>
        /// log destination from configuration file
        /// </summary>
        private static readonly string LogDestination = ConfigurationManager.AppSettings["logDestination"];

        /// <summary>
        /// Gets or sets the log instance
        /// </summary>
        public IErrorLog LogInstance { get; set; }

        /// <summary>
        /// get error log instance
        /// </summary>
        /// <returns>error log</returns>
        public IErrorLog GetErrorLogInstance()
        {
            switch (LogDestination)
            {
                case "file":
                    this.LogInstance = new LogToFile();
                    break;
                case "db":
                    this.LogInstance = new LogToDb();
                    break;
            }

            return this.LogInstance;
        }
    }
}
