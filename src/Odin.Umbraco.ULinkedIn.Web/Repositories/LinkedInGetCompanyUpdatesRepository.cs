using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using website.Odin.Umbraco.ULinkedIn.Web.Models;

namespace website.Odin.Umbraco.ULinkedIn.Web.Repositories
{
    public class LinkedInGetCompanyUpdatesRepository : ILinkedInGetCompanyUpdatesRepository
    {
        private string _accessToken;
        private string _urlLinkedInEndpointBase;
        private LinkedInCompanyUpdates _linkedInCompanyUpdates;


        public LinkedInGetCompanyUpdatesRepository(string accessToken, string companyId)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
                throw new ArgumentNullException();

            if (string.IsNullOrWhiteSpace(companyId))
                throw new ArgumentNullException();

            _accessToken = accessToken;
            _urlLinkedInEndpointBase = $"https://api.linkedin.com/v1/companies/{companyId}/updates?format=json";
            _linkedInCompanyUpdates
                = new LinkedInCompanyUpdates()
                {
                    _total = 0,
                    values = new Value[] { }
                };
        }
        

        public LinkedInCompanyUpdates GetAll()
        {
            string urlLinkedInEndpoint = _urlLinkedInEndpointBase;

            return Get(urlLinkedInEndpoint);
        }

        public LinkedInCompanyUpdates GetJobPostings()
        {
            string urlLinkedInEndpoint = _urlLinkedInEndpointBase + "&event-type=job-posting";

            return Get(urlLinkedInEndpoint);
        }

        public LinkedInCompanyUpdates GetNewProduct()
        {
            //string urlLinkedInEndpoint = _urlLinkedInEndpointBase + "&event-type=new-product";

            //return Get(urlLinkedInEndpoint);
            throw new NotImplementedException();
        }

        public LinkedInCompanyUpdates GetStatusUpdate()
        {
            string urlLinkedInEndpoint = _urlLinkedInEndpointBase + "&event-type=status-update";

            return Get(urlLinkedInEndpoint);
        }

        private LinkedInCompanyUpdates Get(string urlLinkedInEndpoint)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlLinkedInEndpoint);
            request.Headers.Add("Authorization:Bearer " + _accessToken);

            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return JsonConvert.DeserializeObject<LinkedInCompanyUpdates>(reader.ReadToEnd());
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    //ToDo: log error and check for 401
                    // log errorText
                }
                //throw;
                return _linkedInCompanyUpdates;
            }
        }
    }
}