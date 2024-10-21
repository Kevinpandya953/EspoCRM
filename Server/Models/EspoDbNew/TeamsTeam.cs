using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EspoNew.Server.Models.EspoDbNew
{
    [Table("team", Schema = "Teams")]
    public partial class TeamsTeam
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
        public string team_id { get; set; }

        [ConcurrencyCheck]
        public string name { get; set; }

        [ConcurrencyCheck]
        public short? deleted { get; set; }

        [ConcurrencyCheck]
        public string position_list { get; set; }

        [ConcurrencyCheck]
        public string layout_set_id { get; set; }

        [ConcurrencyCheck]
        public string working_time_calendar_id { get; set; }
    }
}