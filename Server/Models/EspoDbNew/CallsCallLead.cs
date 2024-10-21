using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EspoNew.Server.Models.EspoDbNew
{
    [Table("call_lead", Schema = "Calls")]
    public partial class CallsCallLead
    {

        [NotMapped]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("@odata.etag")]
        public string ETag
        {
            get;
            set;
        }

        [Key]
        [Required]
        public string call_lead_id { get; set; }

        [ConcurrencyCheck]
        public string call_id { get; set; }

        public CallsCall call { get; set; }

        [ConcurrencyCheck]
        public string lead_id { get; set; }

        public LeadsLead lead { get; set; }

        [ConcurrencyCheck]
        public string status { get; set; }

        [ConcurrencyCheck]
        public short? deleted { get; set; }
    }
}