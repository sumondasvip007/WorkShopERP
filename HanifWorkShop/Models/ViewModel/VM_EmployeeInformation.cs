using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HanifWorkShop.Models.ViewModel
{
    public class VM_EmployeeInformation
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeAddress { get; set; }
        public string ContactNumber { get; set; }
        public string EmployeeNid { get; set; }
        public string EmployeeNidImage { get; set; }    
        public string EmployeeEmail { get; set; }
        public string EmployeeImage { get; set; }
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public int Salary { get; set; }
        public int RouteId { get; set; }
        public string RouteName { get; set; }
        public string DrivingLicenseImage { get; set; } 
    }
}