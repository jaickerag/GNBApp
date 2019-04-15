using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace dotnet_react.Models
{
    [DataContract]
    public partial class RatesJSON
    {   [DataMember(Name="from")]
        public string from { get; set; }
        [DataMember(Name="to")]
        public string to { get; set; }
        [DataMember(Name="rate")]
        public decimal Rate { get; set; }
    }
}
