using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
   public class VM_PartsTransfer
    {
        public int PartsTransferId { get; set; }
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public DateTime Date { get; set; }
        public int PartsId { get; set; }
        public string PartsName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Price { get; set; }
        public bool isIn { get; set; }
        public bool isOut { get; set; }
   
    }
}
