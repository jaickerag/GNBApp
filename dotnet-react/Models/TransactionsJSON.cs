using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace dotnet_react.Models
{
    [DataContract]
    public partial class TransactionsJSON
    {
        [DataMember(Name="sku")]
        public string Sku { get; set; }
        [DataMember(Name="amount")]
        public decimal Amount { get; set; }
        [DataMember(Name="currency")]
        public string Currency { get; set; }
    }
}
