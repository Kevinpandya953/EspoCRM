using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EspoNew.Server.Models.EspoDbNew
{
    [Table("target", Schema = "Target")]
    public partial class TargetTarget
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
        public string target_id { get; set; }

        [ConcurrencyCheck]
        public short? deleted { get; set; }

        [ConcurrencyCheck]
        public string salutation_name { get; set; }

        [ConcurrencyCheck]
        public string first_name { get; set; }

        [ConcurrencyCheck]
        public string last_name { get; set; }

        [ConcurrencyCheck]
        public string title { get; set; }

        [ConcurrencyCheck]
        public string account_name { get; set; }

        [ConcurrencyCheck]
        public string website { get; set; }

        [ConcurrencyCheck]
        public string address_street { get; set; }

        [ConcurrencyCheck]
        public string address_city { get; set; }

        [ConcurrencyCheck]
        public string address_state { get; set; }

        [ConcurrencyCheck]
        public string address_country { get; set; }

        [ConcurrencyCheck]
        public string address_postal_code { get; set; }

        [ConcurrencyCheck]
        public short do_not_call { get; set; }

        [ConcurrencyCheck]
        public string description { get; set; }

        [ConcurrencyCheck]
        public string middle_name { get; set; }

        [ConcurrencyCheck]
        public string assigned_employee_id { get; set; }
    }
}