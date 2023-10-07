using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SaiKumarDataBeatAPI.Models
{
    public class Articals
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("journal")]
        public string journal { get; set; }

        [JsonPropertyName("eissn")]
        public string eissn { get; set; }

        [JsonPropertyName("publication_date")]
        public DateTime publication_date { get; set; }

        [JsonPropertyName("article_type")]
        public string article_type { get; set; }

        [JsonPropertyName("author_display")]
        public List<string> author_display { get; set; }

        [JsonPropertyName("abstract")]
        public List<string> abstract_data { get; set; }

        [JsonPropertyName("title_display")]
        public string title_display { get; set; }

        [JsonPropertyName("score")]
        public float score { get; set; }

        public int SearchResult_ID { get; set; }
    }
}
