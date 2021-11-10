using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Project_PRN.Models
{
    public partial class Account
    {
        public Account()
        {
            Articles = new HashSet<Article>();
            Comments = new HashSet<Comment>();
        }

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
