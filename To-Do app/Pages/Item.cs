using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using To_Do_app.Services;

namespace To_Do_app.Pages
{
    public class Item
    {
        public string id { get; set; }
        [JsonPropertyName("uid")]
        public string UID { get; set; }  // Ensure Firebase field 'uid' maps to 'UID'

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        [JsonConverter(typeof(JsonDateTimeConverter))] // Custom converter for DateTime
        public DateTime CreatedAt { get; set; }

        [JsonConverter(typeof(JsonDateTimeConverter))] // Custom converter for DateTime
        public DateTime Deadline { get; set; }
    }
}
