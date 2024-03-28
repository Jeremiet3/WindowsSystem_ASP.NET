using Newtonsoft.Json;
using System.Collections.Generic;



namespace WindowsSystem_ASP.NET.BL
{

    public class BlTagsResponse
    {
        [JsonProperty("result")]
        public BlResult Result { get; set; }
    }

    public class BlResult
    {
        [JsonProperty("tags")]
        public List<TagItem> Tags { get; set; } = new List<TagItem>();
    }

    public class TagItem
    {
        [JsonProperty("confidence")]
        public double Confidence { get; set; }

        [JsonProperty("tag")]
        public TagDetail Tag { get; set; }
    }

    public class TagDetail
    {
        [JsonProperty("en")]
        public string En { get; set; }
    }
}
