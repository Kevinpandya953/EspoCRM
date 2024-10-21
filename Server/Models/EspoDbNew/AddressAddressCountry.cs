using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EspoNew.Server.Models.EspoDbNew
{
    [Table("address_country", Schema = "Address")]
    public partial class AddressAddressCountry
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
        public string country_id { get; set; }

        [ConcurrencyCheck]
        public string country_name { get; set; }

        public ICollection<AddressAddressState> Addressaddress_states { get; set; }

        public ICollection<ContactsContact> Contactscontacts { get; set; }
    }
}