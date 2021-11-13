using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


#nullable disable

namespace Project_PRN.Models
{
    public partial class Action
    {
        public Action()
        {
            RoleActions = new HashSet<RoleAction>();
        }

        public int ActionId { get; set; }
        public string ActionName { get; set; }
        public int ControllerId { get; set; }

        [ForeignKey("ControllerId")]
        public virtual Controller Controller { get; set; }
        public virtual ICollection<RoleAction> RoleActions { get; set; }
    }
}
