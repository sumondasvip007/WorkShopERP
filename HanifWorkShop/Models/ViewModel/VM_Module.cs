using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

namespace HanifWorkShop.Models.ViewModel
{
    public class VM_Module
    {
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public int ModuleOrder { get; set; }
        public string ModuleIcon { get; set; }
       
    }
}