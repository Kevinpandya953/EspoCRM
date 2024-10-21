using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EspoNew.Server.Models.EspoDbNew
{
    [Table("campaign", Schema = "Campaign")]
    public partial class CampaignCampaign
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
        public string campaign_id { get; set; }

        [ConcurrencyCheck]
        public string name { get; set; }

        [ConcurrencyCheck]
        public short? deleted { get; set; }

        [ConcurrencyCheck]
        public string status { get; set; }

        [ConcurrencyCheck]
        public string type { get; set; }

        [ConcurrencyCheck]
        public DateTime? start_date { get; set; }

        [ConcurrencyCheck]
        public DateTime? end_date { get; set; }

        [ConcurrencyCheck]
        public string description { get; set; }

        [ConcurrencyCheck]
        public double? budget { get; set; }

        [ConcurrencyCheck]
        public short mail_merge_only_with_address { get; set; }

        [ConcurrencyCheck]
        public string budget_currency { get; set; }

        [ConcurrencyCheck]
        public string assigned_employee_id { get; set; }

        [ConcurrencyCheck]
        public string contacts_template_id { get; set; }

        [ConcurrencyCheck]
        public string leads_template_id { get; set; }

        [ConcurrencyCheck]
        public string accounts_template_id { get; set; }

        [ConcurrencyCheck]
        public string users_template_id { get; set; }

        public ICollection<AccountsAccount> Accountsaccounts { get; set; }

        public ICollection<ContactsContact> Contactscontacts { get; set; }

        public ICollection<LeadsLead> Leadsleads { get; set; }

        public ICollection<OpportunitiesOpportunity> Opportunitiesopportunities { get; set; }
    }
}