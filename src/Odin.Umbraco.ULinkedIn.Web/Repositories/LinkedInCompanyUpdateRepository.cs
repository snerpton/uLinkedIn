using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using website.Odin.Umbraco.ULinkedIn.Web.Models;

namespace Odin.Umbraco.ULinkedIn.Web.Repositories
{
    public class LinkedInCompanyUpdateRepository : ILinkedInCompanyUpdateRepository
    {
        private string _accessToken;
        private string _urlLinkedInEndpointUpdateMultipleBase;
        private string _urlLinkedInEndpointUpdateSingleBase;
        private LinkedInCompanyUpdates _linkedInCompanyUpdates;
        
        public LinkedInCompanyUpdateRepository(string accessToken, string companyId)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
                throw new ArgumentNullException();

            if (string.IsNullOrWhiteSpace(companyId))
                throw new ArgumentNullException();

            _accessToken = accessToken;
            _urlLinkedInEndpointUpdateMultipleBase = $"https://api.linkedin.com/v1/companies/{companyId}/updates?format=json";
            _urlLinkedInEndpointUpdateSingleBase = $"https://api.linkedin.com/v1/companies/{companyId}/updates/key=[UPDATE_KEY_TO_REPLACE]?format=json";
            _linkedInCompanyUpdates
                = new LinkedInCompanyUpdates()
                {
                    _total = 0,
                    values = new Value[] { }
                };
        }
        
        public Value FindById(string updateKey)
        {
            if (string.IsNullOrWhiteSpace(updateKey))
                throw new ArgumentNullException();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_urlLinkedInEndpointUpdateSingleBase.Replace("[UPDATE_KEY_TO_REPLACE]", updateKey));
            request.Headers.Add("Authorization:Bearer " + _accessToken);

            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return JsonConvert.DeserializeObject<Value>(reader.ReadToEnd());
                }
            }
            catch (WebException ex)
            {
                string errorText = string.Empty;
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    errorText = reader.ReadToEnd();
                    //ToDo: log error
                    // log errorText
                }
                throw new Exception(string.IsNullOrWhiteSpace(errorText) ? "There was an error!" : errorText);
            }
        }

        public LinkedInCompanyUpdates ListAll(int count = 10)
        {
            string urlLinkedInEndpoint = _urlLinkedInEndpointUpdateMultipleBase;

            return Get(urlLinkedInEndpoint, count);
        }

        public LinkedInCompanyUpdates ListAllJobPostings(int count = 10)
        {
            string urlLinkedInEndpoint = _urlLinkedInEndpointUpdateMultipleBase + "&event-type=job-posting";

            return Get(urlLinkedInEndpoint, count);
        }

        public LinkedInCompanyUpdates ListAllNewProducts(int count = 10)
        {
            //string urlLinkedInEndpoint = _urlLinkedInEndpointBase + "&event-type=new-product";

            //return Get(urlLinkedInEndpoint);
            throw new NotImplementedException();
        }

        public LinkedInCompanyUpdates ListAllStatusUpdates(int count = 10)
        {
            string urlLinkedInEndpoint = _urlLinkedInEndpointUpdateMultipleBase + "&event-type=status-update";

            return Get(urlLinkedInEndpoint, count);
        }

        private LinkedInCompanyUpdates Get(string urlLinkedInEndpoint, int count)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlLinkedInEndpoint + "&count=" + count);
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
