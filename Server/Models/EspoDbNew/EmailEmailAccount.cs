using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EspoNew.Server.Models.EspoDbNew
{
    [Table("email_account", Schema = "Email")]
    public partial class EmailEmailAccount
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
        public string email_account_id { get; set; }

        [ConcurrencyCheck]
        public string name { get; set; }

        [ConcurrencyCheck]
        public short? deleted { get; set; }

        [ConcurrencyCheck]
        public string email_address { get; set; }

        [ConcurrencyCheck]
        public string status { get; set; }

        [ConcurrencyCheck]
        public string host { get; set; }

        [ConcurrencyCheck]
        public int? port { get; set; }

        [ConcurrencyCheck]
        public string security { get; set; }

        [ConcurrencyCheck]
        public string username { get; set; }

        [ConcurrencyCheck]
        public string password { get; set; }

        [ConcurrencyCheck]
        public string monitored_folders { get; set; }

        [ConcurrencyCheck]
        public string sent_folder { get; set; }

        [ConcurrencyCheck]
        public short store_sent_emails { get; set; }

        [ConcurrencyCheck]
        public short keep_fetched_emails_unread { get; set; }

        [ConcurrencyCheck]
        public DateTime? fetch_since { get; set; }

        [ConcurrencyCheck]
        public string fetch_data { get; set; }

        [Column(TypeName="datetime2")]
        [ConcurrencyCheck]
        public DateTime? connected_at { get; set; }

        [ConcurrencyCheck]
        public short use_imap { get; set; }

        [ConcurrencyCheck]
        public short use_smtp { get; set; }

        [ConcurrencyCheck]
        public string smtp_host { get; set; }

        [ConcurrencyCheck]
        public int? smtp_port { get; set; }

        [ConcurrencyCheck]
        public short smtp_auth { get; set; }

        [ConcurrencyCheck]
        public string smtp_security { get; set; }

        [ConcurrencyCheck]
        public string smtp_username { get; set; }

        [ConcurrencyCheck]
        public string smtp_password { get; set; }

        [ConcurrencyCheck]
        public string smtp_auth_mechanism { get; set; }

        [ConcurrencyCheck]
        public string imap_handler { get; set; }

        [ConcurrencyCheck]
        public string smtp_handler { get; set; }

        [ConcurrencyCheck]
        public string email_folder_id { get; set; }

        [ConcurrencyCheck]
        public string assigned_employee_id { get; set; }
    }
}