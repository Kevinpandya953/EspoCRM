using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EspoNew.Server.Models.EspoDbNew
{
    [Table("lead", Schema = "Leads")]
    public partial class LeadsLead
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
        public string lead_id { get; set; }

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
        public string status { get; set; }

        [ConcurrencyCheck]
        public string source { get; set; }

        [ConcurrencyCheck]
        public string industry { get; set; }

        [ConcurrencyCheck]
        public double? opportunity_amount { get; set; }

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

        [Column(TypeName="datetime2")]
        [ConcurrencyCheck]
        public DateTime? converted_at { get; set; }

        [ConcurrencyCheck]
        public string account_name { get; set; }

        [ConcurrencyCheck]
        public string middle_name { get; set; }

        [ConcurrencyCheck]
        public string opportunity_amount_currency { get; set; }

        [ConcurrencyCheck]
        public string assigned_employee_id { get; set; }

        [ConcurrencyCheck]
        public string campaign_id { get; set; }

        public CampaignCampaign campaign { get; set; }

        [ConcurrencyCheck]
        public string created_account_id { get; set; }

        [ConcurrencyCheck]
        public string created_contact_id { get; set; }

        [ConcurrencyCheck]
        public string created_opportunity_id { get; set; }

        public ICollection<CallsCallLead> Callscall_leads { get; set; }

        public ICollection<CasesCase> Cases_cases { get; set; }

        public ICollection<DocumentsDocumentLead> Documentsdocument_leads { get; set; }
    }
}