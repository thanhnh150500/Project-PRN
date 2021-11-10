﻿using System;
using System.Collections.Generic;

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

        public virtual Controller Controller { get; set; }
        public virtual ICollection<RoleAction> RoleActions { get; set; }
    }
}
