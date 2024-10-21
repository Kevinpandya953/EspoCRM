using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EspoNew.Server.Models.EspoDbNew
{
    [Table("document_lead", Schema = "Documents")]
    public partial class DocumentsDocumentLead
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
        public string document_lead_id { get; set; }

        [ConcurrencyCheck]
        public string document_id { get; set; }

        public DocumentsDocument document { get; set; }

        [ConcurrencyCheck]
        public string lead_id { get; set; }

        public LeadsLead lead { get; set; }

        [ConcurrencyCheck]
        public short? deleted { get; set; }
    }
}