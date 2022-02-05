using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresciptionTextSharp.Models
{
    public class Presciption
    {
        public string? SIG { get; set; }
        public string? Directions { get; set; }
        public string? Type { get; set; }
        public string? DX { get; set; }
        public string? Pharmacist { get; set; }
        public string? ID { get; set; }
    }
}
