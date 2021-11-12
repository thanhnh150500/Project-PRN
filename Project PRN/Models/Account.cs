using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

<<<<<<< HEAD
        [Required(ErrorMessage = "Must input Username.")]
        [RegularExpression(@"^.{3,}$", ErrorMessage = "Minimum 3 characters required for Username.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Must input Password.")]
        [RegularExpression(@"^.{3,}$", ErrorMessage = "Minimum 3 characters required for Password.")]
=======
        public string Username { get; set; }
       public string Username { get; set; }
>>>>>>> dea7f14e2a3dbe6aa7d4cf62e672f39d146e4612
        public string Password { get; set; }

        //ref: https://stackoverflow.com/questions/21746910/compare-password-and-confirm-password-in-asp-net-mvc
        [NotMapped] // Does not effect with your database
        [Compare("Password", ErrorMessage = "Confirm password not fit")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Must input Fullname.")]
        [RegularExpression(@"^\d+([A-Z][a-z]+\s{1})+$", ErrorMessage = "Wrong format value for Fullname.")]
        public string Fullname { get; set; }

        public string Avatar { get; set; }

        [Required(ErrorMessage = "Must input Email.")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Wrong format value for Email.")]
        public string Email { get; set; }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
