namespace WebApi136.Areas.HelpPage
{
    using System.Text;
    using System.Web;
    using System.Web.Http.Description;

    public static class ApiDescriptionExtensions
    {
        public static string GetFriendlyId(this ApiDescription description)
        {
            var path = description.RelativePath;
            var urlParts = path.Split('?');
            var localPath = urlParts[0];
            string queryKeyString = null;
            if (urlParts.Length > 1)
            {
                var query = urlParts[1];
                var queryKeys = HttpUtility.ParseQueryString(query).AllKeys;
                queryKeyString = string.Join("_", queryKeys);
            }

            var friendlyPath = new StringBuilder();
            friendlyPath.AppendFormat("{0}-{1}", description.HttpMethod.Method, localPath.Replace("/", "-").Replace("{", string.Empty).Replace("}", string.Empty));
            if (queryKeyString != null)
            {
                friendlyPath.AppendFormat("_{0}", queryKeyString);
            }

            return friendlyPath.ToString();
        }
    }
}