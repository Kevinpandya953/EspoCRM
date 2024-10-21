using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EspoNew.Server.Models.EspoDbNew
{
    [Table("contact_opportunity", Schema = "Contacts")]
    public partial class ContactsContactOpportunity
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
        public string contact_opportunity_id { get; set; }

        [ConcurrencyCheck]
        public string contact_id { get; set; }

        public ContactsContact contact { get; set; }

        [ConcurrencyCheck]
        public string opportunity_id { get; set; }

        public OpportunitiesOpportunity opportunity { get; set; }

        [ConcurrencyCheck]
        public string role { get; set; }

        [ConcurrencyCheck]
        public short? deleted { get; set; }
    }
}