using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using website.Odin.Umbraco.ULinkedIn.Web.Models;

namespace website.Odin.Umbraco.ULinkedIn.Web.Repositories
{
    public class LinkedInGetSpecificCompanyUpdateRepository : ILinkedInGetSpecificCompanyUpdateRepository
    {
        private string _accessToken;
        private string _urlLinkedInEndpoint;

        public LinkedInGetSpecificCompanyUpdateRepository(string accessToken, string companyId, string updateKey)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
                throw new ArgumentNullException();

            if (string.IsNullOrWhiteSpace(companyId))
                throw new ArgumentNullException();

            if (string.IsNullOrWhiteSpace(updateKey))
                throw new ArgumentNullException();

            _accessToken = accessToken;
            _urlLinkedInEndpoint = $"https://api.linkedin.com/v1/companies/{companyId}/updates/key={updateKey}?format=json";
        }

        public Value Get()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_urlLinkedInEndpoint);
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
    }
}