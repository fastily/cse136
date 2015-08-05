namespace WebApi136.Areas.HelpPage.Models
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Net.Http.Headers;
    using System.Web.Http.Description;

    public class HelpPageApiModel
    {
        public HelpPageApiModel()
        {
            this.SampleRequests = new Dictionary<MediaTypeHeaderValue, object>();
            this.SampleResponses = new Dictionary<MediaTypeHeaderValue, object>();
            this.ErrorMessages = new Collection<string>();
        }

        public ApiDescription ApiDescription { get; set; }

        public IDictionary<MediaTypeHeaderValue, object> SampleRequests { get; private set; }

        public IDictionary<MediaTypeHeaderValue, object> SampleResponses { get; private set; }

        public Collection<string> ErrorMessages { get; private set; }
    }
}