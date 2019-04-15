using System;
using System.Collections.Generic;

namespace dotnet_react.Models
{
    public partial class Transactions
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
