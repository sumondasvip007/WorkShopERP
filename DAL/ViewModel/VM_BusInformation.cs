using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class VM_BusInformation
    {
        public int BusInformationId { get; set; }
        public string RegistrationNo { get; set; }
        public string ModelNo { get; set; }
        public string ChassisNo { get; set; }
        public int RouteId { get; set; }
        public string RouteName { get; set; }
    }
}
