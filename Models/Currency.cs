using System;
using System.ComponentModel.DataAnnotations;

namespace CBRCurrency.Models
{
    public class Currency
    {
        [Key]
        public string CharCode { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public int Nominal { get; set; }
        public DateTime DateTime { get; set; }
    }
}
