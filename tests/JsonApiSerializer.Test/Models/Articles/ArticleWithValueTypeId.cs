using System.Collections.Generic;
using JsonApiSerializer.JsonApi;
using Newtonsoft.Json;

namespace JsonApiSerializer.Test.Models.Articles
{
    public class ArticleWithValueTypeId
    {
        public string Type { get; set; } = "articles";

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Id { get; set; }

        public string Title { get; set; }

        public PersonWithValueTypeId Author { get; set; }

        public List<Comment> Comments { get; set; }

        public Links Links { get; set; }

    }
}