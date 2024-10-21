using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EspoNew.Server.Models.EspoDbNew
{
    [Table("address_state", Schema = "Address")]
    public partial class AddressAddressState
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
        public string state_id { get; set; }

        [ConcurrencyCheck]
        public string state_name { get; set; }

        [ConcurrencyCheck]
        public string country_id { get; set; }

        public AddressAddressCountry address_country { get; set; }

        public ICollection<AddressAddressCity> Addressaddress_cities { get; set; }

        public ICollection<ContactsContact> Contactscontacts { get; set; }
    }
}