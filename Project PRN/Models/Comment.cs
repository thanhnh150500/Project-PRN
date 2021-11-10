using System;
using System.Collections.Generic;

#nullable disable

namespace Project_PRN.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int ArticleId { get; set; }
        public string AccountUsername { get; set; }

        public virtual Account AccountUsernameNavigation { get; set; }
        public virtual Article Article { get; set; }
    }
}
