using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EspoNew.Server.Models.EspoDbNew
{
    [Table("contact", Schema = "Contacts")]
    public partial class ContactsContact
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
        public string contact_id { get; set; }

        [ConcurrencyCheck]
        public short? deleted { get; set; }

        [ConcurrencyCheck]
        public string salutation_name { get; set; }

        [ConcurrencyCheck]
        public string first_name { get; set; }

        [ConcurrencyCheck]
        public string last_name { get; set; }

        [ConcurrencyCheck]
        public string description { get; set; }

        [ConcurrencyCheck]
        public short do_not_call { get; set; }

        [ConcurrencyCheck]
        public string address_street { get; set; }

        [ConcurrencyCheck]
        public string address_city_id { get; set; }

        public AddressAddressCity address_city { get; set; }

        [ConcurrencyCheck]
        public string address_state_id { get; set; }

        public AddressAddressState address_state { get; set; }

        [ConcurrencyCheck]
        public string address_country_id { get; set; }

        public AddressAddressCountry address_country { get; set; }

        [ConcurrencyCheck]
        public string address_postal_code { get; set; }

        [ConcurrencyCheck]
        public string middle_name { get; set; }

        [ConcurrencyCheck]
        public string account_id { get; set; }

        public AccountsAccount account { get; set; }

        [ConcurrencyCheck]
        public string campaign_id { get; set; }

        public CampaignCampaign campaign { get; set; }

        [ConcurrencyCheck]
        public string assigned_employee_id { get; set; }

        public ICollection<AccountsAccountContact> Accountsaccount_contacts { get; set; }

        public ICollection<CallsCallContact> Callscall_contacts { get; set; }

        public ICollection<CasesCase> Cases_cases { get; set; }

        public ICollection<CasesCaseContact> Casescase_contacts { get; set; }

        public ICollection<ContactsContactDocument> Contactscontact_documents { get; set; }

        public ICollection<ContactsContactMeeting> Contactscontact_meetings { get; set; }

        public ICollection<ContactsContactOpportunity> Contactscontact_opportunities { get; set; }

        public ICollection<EmployeesEmployee> Employeesemployees { get; set; }

        public ICollection<OpportunitiesOpportunity> Opportunitiesopportunities { get; set; }

        public ICollection<TasksTask> Taskstasks { get; set; }
    }
}