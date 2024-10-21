using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EspoNew.Server.Models.EspoDbNew
{
    [Table("call", Schema = "Calls")]
    public partial class CallsCall
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
        public string call_id { get; set; }

        [ConcurrencyCheck]
        public string name { get; set; }

        [ConcurrencyCheck]
        public short? deleted { get; set; }

        [ConcurrencyCheck]
        public string status { get; set; }

        [Column(TypeName="datetime2")]
        [ConcurrencyCheck]
        public DateTime? date_start { get; set; }

        [Column(TypeName="datetime2")]
        [ConcurrencyCheck]
        public DateTime? date_end { get; set; }

        [ConcurrencyCheck]
        public string direction { get; set; }

        [ConcurrencyCheck]
        public string description { get; set; }

        [ConcurrencyCheck]
        public string parent_id { get; set; }

        [ConcurrencyCheck]
        public string parent_type { get; set; }

        [ConcurrencyCheck]
        public string account_id { get; set; }

        public AccountsAccount account { get; set; }

        [ConcurrencyCheck]
        public string assigned_employee_id { get; set; }

        public ICollection<CallsCallContact> Callscall_contacts { get; set; }

        public ICollection<CallsCallLead> Callscall_leads { get; set; }
    }
}