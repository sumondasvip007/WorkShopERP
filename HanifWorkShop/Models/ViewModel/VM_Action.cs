using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HanifWorkShop.Models.ViewModel
{
    public class VM_Action
    {
        public int ActionId { get; set; }
        public string ActionName { get; set; }
        public string ActionDisplayName { get; set; }
        public string ActionUrl { get; set; }
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public bool IsInMenu { get; set; }
        public bool IsView { get; set; }
    }
}