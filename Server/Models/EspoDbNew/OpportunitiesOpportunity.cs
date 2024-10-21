using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EspoNew.Server.Models.EspoDbNew
{
    [Table("opportunity", Schema = "Opportunities")]
    public partial class OpportunitiesOpportunity
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
        public string opportunity_id { get; set; }

        [ConcurrencyCheck]
        public string name { get; set; }

        [ConcurrencyCheck]
        public short? deleted { get; set; }

        [ConcurrencyCheck]
        public double? amount { get; set; }

        [ConcurrencyCheck]
        public string stage { get; set; }

        [ConcurrencyCheck]
        public string last_stage { get; set; }

        [ConcurrencyCheck]
        public int? probability { get; set; }

        [ConcurrencyCheck]
        public string lead_source { get; set; }

        [ConcurrencyCheck]
        public DateTime? close_date { get; set; }

        [ConcurrencyCheck]
        public string description { get; set; }

        [ConcurrencyCheck]
        public string amount_currency { get; set; }

        [ConcurrencyCheck]
        public string account_id { get; set; }

        public AccountsAccount account { get; set; }

        [ConcurrencyCheck]
        public string contact_id { get; set; }

        public ContactsContact contact { get; set; }

        [ConcurrencyCheck]
        public string campaign_id { get; set; }

        public CampaignCampaign campaign { get; set; }

        [ConcurrencyCheck]
        public string assigned_employee_id { get; set; }

        public EmployeesEmployee employee { get; set; }

        public ICollection<ContactsContactOpportunity> Contactscontact_opportunities { get; set; }
    }
}