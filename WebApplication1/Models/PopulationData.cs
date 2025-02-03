using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class PopulationData
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("data-availability")]
        public string DataAvailability { get; set; }

        [JsonPropertyName("var")]
        public List<VarData> Variables { get; set; }

        [JsonPropertyName("tahun")]
        public List<YearData> Tahun { get; set; }

        [JsonPropertyName("datacontent")]
        public Dictionary<string, double> DataContent { get; set; }
    }

    public class VarData
    {
        [JsonPropertyName("val")]
        public int Val { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }

    public class YearData
    {
        [JsonPropertyName("val")]
        public int Val { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }
    }
}
