using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EspoNew.Server.Models.EspoDbNew
{
    [Table("account_document", Schema = "Accounts")]
    public partial class AccountsAccountDocument
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
        public string account_document_id { get; set; }

        [ConcurrencyCheck]
        public string account_id { get; set; }

        public AccountsAccount account { get; set; }

        [ConcurrencyCheck]
        public string document_id { get; set; }

        public DocumentsDocument document { get; set; }

        [ConcurrencyCheck]
        public short? deleted { get; set; }
    }
}