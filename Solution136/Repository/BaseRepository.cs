namespace Repository
{
    using System.Configuration;

    public class BaseRepository
    {
        public static string ConnectionString 
        {
            get 
            {
                return ConfigurationManager.AppSettings["dsn"];
            }
        }
    }
}
