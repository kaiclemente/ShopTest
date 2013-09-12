using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ShopBackEnd.Model.DTO
{
    [DataContract]
    public class BaseDTO : IIdentifier
    {
        [DataMember]
        [JsonPropertyAttribute(NullValueHandling = NullValueHandling.Ignore)]
        public virtual int ID { get; set; }

    }
}
