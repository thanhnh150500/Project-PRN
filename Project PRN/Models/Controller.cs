using System;
using System.Collections.Generic;

#nullable disable

namespace Project_PRN.Models
{
    public partial class Controller
    {
        public Controller()
        {
            Actions = new HashSet<Action>();
        }

        public int ControllerId { get; set; }
        public string ControllerName { get; set; }

        public virtual ICollection<Action> Actions { get; set; }
    }
}
