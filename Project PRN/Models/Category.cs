﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Project_PRN.Models
{
    public partial class Category
    {
        public Category()
        {
            Articles = new HashSet<Article>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
