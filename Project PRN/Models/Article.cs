using System;
using System.Collections.Generic;

#nullable disable

namespace Project_PRN.Models
{
    public partial class Article
    {
        public Article()
        {
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public DateTime ReleasedDate { get; set; }
        public int View { get; set; }
        public string CreatedAccountUsername { get; set; }
        public string ShortContent { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Account CreatedAccountUsernameNavigation { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
