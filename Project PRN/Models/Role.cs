using System;
using System.Collections.Generic;

#nullable disable

namespace Project_PRN.Models
{
    public partial class Role
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
            RoleActions = new HashSet<RoleAction>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<RoleAction> RoleActions { get; set; }
    }
}
