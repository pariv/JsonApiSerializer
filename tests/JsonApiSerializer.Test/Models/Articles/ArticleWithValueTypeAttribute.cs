using Newtonsoft.Json;

namespace JsonApiSerializer.Test.Models.Articles
{
    public class ArticleWithValueTypeAttribute
    {
        public string Type => "articles";

        public string Id { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public int Order { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int NotIncludedOrder { get; set; }
    }
}