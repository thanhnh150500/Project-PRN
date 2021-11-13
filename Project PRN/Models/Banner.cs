using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Project_PRN.Models
{
    public partial class Banner
    {
        public int Id { get; set; }
        public string BigImage { get; set; }
        public string IntroTitle { get; set; }
        public string Description { get; set; }
    }
}
