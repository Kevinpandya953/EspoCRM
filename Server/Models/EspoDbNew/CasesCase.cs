using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EspoNew.Server.Models.EspoDbNew
{
    [Table("case", Schema = "Cases")]
    public partial class CasesCase
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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public string case_id { get; set; }

        [ConcurrencyCheck]
        public string name { get; set; }

        [ConcurrencyCheck]
        public short? deleted { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long number { get; set; }

        [ConcurrencyCheck]
        public string status { get; set; }

        [ConcurrencyCheck]
        public string priority { get; set; }

        [ConcurrencyCheck]
        public string type { get; set; }

        [ConcurrencyCheck]
        public string description { get; set; }

        [ConcurrencyCheck]
        public string account_id { get; set; }

        public AccountsAccount account { get; set; }

        [ConcurrencyCheck]
        public string lead_id { get; set; }

        public LeadsLead lead { get; set; }

        [ConcurrencyCheck]
        public string contact_id { get; set; }

        public ContactsContact contact { get; set; }

        [ConcurrencyCheck]
        public string inbound_email_id { get; set; }

        [ConcurrencyCheck]
        public string assigned_employee_id { get; set; }

        public ICollection<CasesCaseContact> Casescase_contacts { get; set; }
    }
}