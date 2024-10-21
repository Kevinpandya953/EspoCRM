using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EspoNew.Server.Models.EspoDbNew
{
    [Table("address_city", Schema = "Address")]
    public partial class AddressAddressCity
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
        public string city_id { get; set; }

        [ConcurrencyCheck]
        public string city_name { get; set; }

        [ConcurrencyCheck]
        public string state_id { get; set; }

        public AddressAddressState address_state { get; set; }

        public ICollection<ContactsContact> Contactscontacts { get; set; }
    }
}