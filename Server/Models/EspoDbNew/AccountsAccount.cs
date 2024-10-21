using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EspoNew.Server.Models.EspoDbNew
{
    [Table("account", Schema = "Accounts")]
    public partial class AccountsAccount
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
        public string account_id { get; set; }

        [ConcurrencyCheck]
        public string name { get; set; }

        [ConcurrencyCheck]
        public short? deleted { get; set; }

        [ConcurrencyCheck]
        public string website { get; set; }

        [ConcurrencyCheck]
        public string type { get; set; }

        [ConcurrencyCheck]
        public string industry { get; set; }

        [ConcurrencyCheck]
        public string sic_code { get; set; }

        [ConcurrencyCheck]
        public string billing_address_street { get; set; }

        [ConcurrencyCheck]
        public string billing_address_city { get; set; }

        [ConcurrencyCheck]
        public string billing_address_state { get; set; }

        [ConcurrencyCheck]
        public string billing_address_country { get; set; }

        [ConcurrencyCheck]
        public string billing_address_postal_code { get; set; }

        [ConcurrencyCheck]
        public string shipping_address_street { get; set; }

        [ConcurrencyCheck]
        public string shipping_address_city { get; set; }

        [ConcurrencyCheck]
        public string shipping_address_state { get; set; }

        [ConcurrencyCheck]
        public string shipping_address_country { get; set; }

        [ConcurrencyCheck]
        public string shipping_address_postal_code { get; set; }

        [ConcurrencyCheck]
        public string description { get; set; }

        [ConcurrencyCheck]
        public string campaign_id { get; set; }

        public CampaignCampaign campaign { get; set; }

        [ConcurrencyCheck]
        public string assigned_employee_id { get; set; }

        public EmployeesEmployee employee { get; set; }

        public ICollection<AccountsAccountContact> Accountsaccount_contacts { get; set; }

        public ICollection<AccountsAccountDocument> Accountsaccount_documents { get; set; }

        public ICollection<CallsCall> Callscalls { get; set; }

        public ICollection<CasesCase> Cases_cases { get; set; }

        public ICollection<ContactsContact> Contactscontacts { get; set; }

        public ICollection<EmailEmail> Emailemails { get; set; }

        public ICollection<MeetingsMeeting> Meetingsmeetings { get; set; }

        public ICollection<OpportunitiesOpportunity> Opportunitiesopportunities { get; set; }

        public ICollection<TasksTask> Taskstasks { get; set; }
    }
}