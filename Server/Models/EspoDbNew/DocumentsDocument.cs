using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EspoNew.Server.Models.EspoDbNew
{
    [Table("document", Schema = "Documents")]
    public partial class DocumentsDocument
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
        public string document_id { get; set; }

        [ConcurrencyCheck]
        public string name { get; set; }

        [ConcurrencyCheck]
        public short? deleted { get; set; }

        [ConcurrencyCheck]
        public string status { get; set; }

        [ConcurrencyCheck]
        public string type { get; set; }

        [ConcurrencyCheck]
        public DateTime? publish_date { get; set; }

        [ConcurrencyCheck]
        public DateTime? expiration_date { get; set; }

        [ConcurrencyCheck]
        public string description { get; set; }

        [ConcurrencyCheck]
        public string file_id { get; set; }

        [ConcurrencyCheck]
        public string assigned_employee_id { get; set; }

        public EmployeesEmployee employee { get; set; }

        [ConcurrencyCheck]
        public string folder_id { get; set; }

        public ICollection<AccountsAccountDocument> Accountsaccount_documents { get; set; }

        public ICollection<ContactsContactDocument> Contactscontact_documents { get; set; }

        public ICollection<DocumentsDocumentLead> Documentsdocument_leads { get; set; }
    }
}