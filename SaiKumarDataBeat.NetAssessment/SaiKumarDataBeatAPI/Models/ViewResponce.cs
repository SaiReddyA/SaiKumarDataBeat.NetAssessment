using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SaiKumarDataBeatAPI.Models
{
    public class ViewResponce
    {
        [JsonPropertyName("numFound")]
        public int numFound { get; set; }

        [JsonPropertyName("start")]
        public int start { get; set; }

        [JsonPropertyName("maxScore")]
        public double maxScore { get; set; } // Use double for maxScore as it appears to be a floating-point value

        [JsonPropertyName("docs")]
        public List<Articals> docs { get; set; }
    }

    public class Jsondata
    {
        [JsonPropertyName("response")] 
        public ViewResponce ViewResponce { get; set; }
    }
}
