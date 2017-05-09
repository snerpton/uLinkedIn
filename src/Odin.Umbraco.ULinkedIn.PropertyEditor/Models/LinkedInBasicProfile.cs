using Newtonsoft.Json;

namespace website.Odin.Umbraco.ULinkedIn.PropertyEditor.Models
{
    public class LinkedInBasicProfile
    {
        [JsonProperty(propertyName: "first-name")]
        public string FirstName { get; set; }

        [JsonProperty(propertyName: "headline")]
        public string Headline { get; set; }

        [JsonProperty(propertyName: "id")]
        public string Id { get; set; }

        [JsonProperty(propertyName: "last-name")]
        public string LastName { get; set; }
    }
}