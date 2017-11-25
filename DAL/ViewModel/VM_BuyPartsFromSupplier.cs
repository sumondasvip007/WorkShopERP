using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
   public class VM_BuyPartsFromSupplier
    {
        public int BuyId { get; set; }
        public string RegistrationNo { get; set; }
        //public DateTime BuyDate { get; set; }
        public int PartsId { get; set; }
        public string PartsName { get; set; }
        public int SupplierId { get; set; }
        public string CompanyName { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int Price { get; set; }
        public string Other { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
    }
}
