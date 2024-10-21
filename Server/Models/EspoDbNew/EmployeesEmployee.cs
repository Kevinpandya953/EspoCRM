using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EspoNew.Server.Models.EspoDbNew
{
    [Table("employee", Schema = "Employees")]
    public partial class EmployeesEmployee
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
        public string employee_id { get; set; }

        [ConcurrencyCheck]
        public short? deleted { get; set; }

        [ConcurrencyCheck]
        public string employee_name { get; set; }

        [ConcurrencyCheck]
        public string type { get; set; }

        [ConcurrencyCheck]
        public string password { get; set; }

        [ConcurrencyCheck]
        public string auth_method { get; set; }

        [ConcurrencyCheck]
        public string api_key { get; set; }

        [ConcurrencyCheck]
        public string salutation_name { get; set; }

        [ConcurrencyCheck]
        public string first_name { get; set; }

        [ConcurrencyCheck]
        public string last_name { get; set; }

        [ConcurrencyCheck]
        public short? is_active { get; set; }

        [ConcurrencyCheck]
        public string title { get; set; }

        [ConcurrencyCheck]
        public string avatar_color { get; set; }

        [ConcurrencyCheck]
        public string gender { get; set; }

        [ConcurrencyCheck]
        public string delete_id { get; set; }

        [ConcurrencyCheck]
        public string middle_name { get; set; }

        [ConcurrencyCheck]
        public string default_team_id { get; set; }

        [ConcurrencyCheck]
        public string contact_id { get; set; }

        public ContactsContact contact { get; set; }

        [ConcurrencyCheck]
        public string avatar_id { get; set; }

        [ConcurrencyCheck]
        public string dashboard_template_id { get; set; }

        [ConcurrencyCheck]
        public string working_time_calendar_id { get; set; }

        [ConcurrencyCheck]
        public string layout_set_id { get; set; }

        public ICollection<AccountsAccount> Accountsaccounts { get; set; }

        public ICollection<DocumentsDocument> Documentsdocuments { get; set; }

        public ICollection<OpportunitiesOpportunity> Opportunitiesopportunities { get; set; }

        public ICollection<TasksTask> Taskstasks { get; set; }
    }
}