using System;
using System.Collections.Generic;

namespace dotnet_react.Models
{
    public partial class Rates
    {
        public int Id { get; set; }
        public string FromCurr { get; set; }
        public string ToCurr { get; set; }
        public decimal Rate { get; set; }
    }
}
