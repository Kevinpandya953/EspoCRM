using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EspoNew.Server.Models.EspoDbNew
{
    [Table("account_contact", Schema = "Accounts")]
    public partial class AccountsAccountContact
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
        public string account_contact_id { get; set; }

        [ConcurrencyCheck]
        public string account_id { get; set; }

        public AccountsAccount account { get; set; }

        [ConcurrencyCheck]
        public string contact_id { get; set; }

        public ContactsContact contact { get; set; }

        [ConcurrencyCheck]
        public string role { get; set; }

        [ConcurrencyCheck]
        public short? is_inactive { get; set; }

        [ConcurrencyCheck]
        public short? deleted { get; set; }
    }
}