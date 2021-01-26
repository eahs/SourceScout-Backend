using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ADSBackend.Models
{
    public class PostTag
    {
        public int PostId { get; set; }
        [JsonIgnore]
        public Post Post { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
