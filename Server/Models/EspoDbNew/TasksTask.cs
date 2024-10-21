using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EspoNew.Server.Models.EspoDbNew
{
    [Table("task", Schema = "Tasks")]
    public partial class TasksTask
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
        public string task_id { get; set; }

        [ConcurrencyCheck]
        public string name { get; set; }

        [ConcurrencyCheck]
        public short? deleted { get; set; }

        [ConcurrencyCheck]
        public string status { get; set; }

        [ConcurrencyCheck]
        public string priority { get; set; }

        [Column(TypeName="datetime2")]
        [ConcurrencyCheck]
        public DateTime? date_start { get; set; }

        [Column(TypeName="datetime2")]
        [ConcurrencyCheck]
        public DateTime? date_end { get; set; }

        [ConcurrencyCheck]
        public DateTime? date_start_date { get; set; }

        [ConcurrencyCheck]
        public DateTime? date_end_date { get; set; }

        [Column(TypeName="datetime2")]
        [ConcurrencyCheck]
        public DateTime? date_completed { get; set; }

        [ConcurrencyCheck]
        public string description { get; set; }

        [ConcurrencyCheck]
        public string parent_id { get; set; }

        [ConcurrencyCheck]
        public string parent_type { get; set; }

        [ConcurrencyCheck]
        public string account_id { get; set; }

        public AccountsAccount account { get; set; }

        [ConcurrencyCheck]
        public string contact_id { get; set; }

        public ContactsContact contact { get; set; }

        [ConcurrencyCheck]
        public string assigned_employee_id { get; set; }

        public EmployeesEmployee employee { get; set; }

        [ConcurrencyCheck]
        public string email_id { get; set; }

        public EmailEmail email { get; set; }
    }
}