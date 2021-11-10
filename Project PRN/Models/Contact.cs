using System;
using System.Collections.Generic;

#nullable disable

namespace Project_PRN.Models
{
    public partial class Contact
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string MapUrl { get; set; }
    }
}
