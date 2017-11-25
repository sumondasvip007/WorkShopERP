using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
   public class Vm_PartsTransfetToBusRegistrationNoFromStore
    {
        public int BuyId { get; set; }
        public string RegistrationNo { get; set; }
        //public DateTime BuyDate { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public int PartsId { get; set; }
        public string PartsName { get; set; }
        public int? Quantity { get; set; }
        public int? StoreQuantity { get; set; }
        public int UnitPrice { get; set; }
        public int Price { get; set; }
        public int AvailableQuatity { get; set; }
        public int PartIndex { get; set; }
        public int QuatityIndex { get; set; }

        //public tblPartsInfo PartsInformation { get; set; }
        //public Vm_PartsTransfetToBusRegistrationNoFromStore()
        //{
        //    PartsInformation =  new tblPartsInfo();
        //}
    }
}
