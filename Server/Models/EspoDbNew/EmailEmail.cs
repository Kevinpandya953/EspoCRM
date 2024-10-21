using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EspoNew.Server.Models.EspoDbNew
{
    [Table("email", Schema = "Email")]
    public partial class EmailEmail
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
        public string email_id { get; set; }

        [ConcurrencyCheck]
        public string name { get; set; }

        [ConcurrencyCheck]
        public short? deleted { get; set; }

        [ConcurrencyCheck]
        public string from_string { get; set; }

        [ConcurrencyCheck]
        public string reply_to_string { get; set; }

        [ConcurrencyCheck]
        public string address_name_map { get; set; }

        [ConcurrencyCheck]
        public short is_replied { get; set; }

        [ConcurrencyCheck]
        public string message_id { get; set; }

        [ConcurrencyCheck]
        public string message_id_internal { get; set; }

        [ConcurrencyCheck]
        public string body_plain { get; set; }

        [ConcurrencyCheck]
        public string body { get; set; }

        [ConcurrencyCheck]
        public short is_html { get; set; }

        [ConcurrencyCheck]
        public string status { get; set; }

        [ConcurrencyCheck]
        public short has_attachment { get; set; }

        [Column(TypeName="datetime2")]
        [ConcurrencyCheck]
        public DateTime? date_sent { get; set; }

        [Column(TypeName="datetime2")]
        [ConcurrencyCheck]
        public DateTime? delivery_date { get; set; }

        [ConcurrencyCheck]
        public short is_system { get; set; }

        [ConcurrencyCheck]
        public string ics_contents { get; set; }

        [ConcurrencyCheck]
        public string ics_event_uid { get; set; }

        [ConcurrencyCheck]
        public string from_email_address_id { get; set; }

        [ConcurrencyCheck]
        public string parent_id { get; set; }

        [ConcurrencyCheck]
        public string parent_type { get; set; }

        [ConcurrencyCheck]
        public string sent_by_id { get; set; }

        [ConcurrencyCheck]
        public string assigned_employee_id { get; set; }

        [ConcurrencyCheck]
        public string replied_id { get; set; }

        [ConcurrencyCheck]
        public string created_event_id { get; set; }

        [ConcurrencyCheck]
        public string created_event_type { get; set; }

        [ConcurrencyCheck]
        public string group_folder_id { get; set; }

        [ConcurrencyCheck]
        public string account_id { get; set; }

        public AccountsAccount account { get; set; }

        public ICollection<TasksTask> Taskstasks { get; set; }
    }
}