using JsonApiSerializer.JsonApi;
using JsonApiSerializer.JsonApi.WellKnown;
using Newtonsoft.Json;

namespace JsonApiSerializer.Test.Models.Articles
{
    public class PersonWithValueTypeId
    {
        public string Type { get; set; } = "people";

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Id { get; set; }

        [JsonProperty(propertyName: "first-name")]
        public string FirstName { get; set; }

        [JsonProperty(propertyName: "last-name")]
        public string LastName { get; set; }

        [JsonIgnore]
        public string Twitter { get; set; }

        public Links Links { get; set; }

        public Method Method { get; set; }

        public Meta Meta { get; set; }

    }
}