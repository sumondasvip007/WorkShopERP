using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using DAL.ViewModel;

namespace DAL.Repository
{
    public class CustomRepository : IDisposable
    {
        private HanifWorkShop_DBEntity context;


        public CustomRepository(HanifWorkShop_DBEntity context)
        {
            this.context = context;
        }
      

        public List<VM_Employee> sp_EmployeeInformationForSpecificBusRoute(int routeId)
        {
            var employeeList =context.sp_EmployeeInformationForSpecificBusRoute(routeId)
                .Select(a => new VM_Employee()
                {

                    EmployeeId = a.EmployeeId,
                    EmployeeName = a.EmployeeName,
                    EmployeeEmail = a.EmployeeEmail,
                    EmployeeAddress = a.EmployeeAddress,
                    EmployeeNid = a.EmployeeNid,
                    ContactNumber = a.ContactNumber,
                    DesignationId = (int) a.DesignationId,
                    DesignationName = a.DesignationName,
                    Salary = (int) a.Salary
                  

                })
                .ToList();
            return employeeList;
        }

        public List<VM_BusInformation> sp_BusInfoForSpecificBusRegistrationNo(string RegistrationNo)
        {
            var busInfoList = context.sp_BusInfoForSpecificBusRegistrationNo(RegistrationNo)
                .Select(a => new VM_BusInformation()
                {

                   ModelNo = a.ModelNo,
                   ChassisNo = a.ChassisNo,
                   RouteName = a.RouteName
                })
                .ToList();
            return busInfoList;
        }

        public List<VM_BuyPartsFromSupplier> sp_PartsBuyHistoryFromDateToDateFromSupplier(int supplierId, DateTime fromDate, DateTime toDate)
        {
            var buyPartsInfoList = context.sp_PartsBuyHistoryFromDateToDateFromSupplier(supplierId, fromDate,toDate)
                .Select(a => new VM_BuyPartsFromSupplier()
                {

                   RegistrationNo = a.RegistrationNo,
                   PartsName = a.PartsName,
                   Quantity = Convert.ToInt32(a.Quantity),
                   UnitPrice = Convert.ToInt32(a.UnitPrice),
                   Price = Convert.ToInt32(a.Price)
                })
                .ToList();
            return buyPartsInfoList;
        }

        public List<VM_CostForBusRegistrationNo> sp_totalCostHistoryFromDateToDateForBusRegistrationNoFullFinal(string registrationNo, DateTime fromDate, DateTime toDate)
        {
            var totalCostInfoList = context.sp_totalCostHistoryFromDateToDateForBusRegistrationNoFullFinal(registrationNo, fromDate, toDate)
                .Select(a => new VM_CostForBusRegistrationNo()
                {

                    CompanyName =a.CompanyName,
                    PartsName = a.PartsName,
                    Quantity = Convert.ToInt32(a.Quantity),
                    UnitPrice = Convert.ToInt32(a.UnitPrice),
                    Price = Convert.ToInt32(a.Price),
                    Other = a.Other,
                    StoreName = a.StoreName
                })
                .ToList();
            return totalCostInfoList;
        }

        public List<VM_BusInformation> sp_BusInformationListForABusRoute(int routeId)
        {
            var busInfoList = context.sp_BusInformationListForABusRoute(routeId)
                .Select(a => new VM_BusInformation()
                {

                    RegistrationNo = a.RegistrationNo,
                    ModelNo = a.ModelNo,
                    ChassisNo = a.ChassisNo
                })
                .ToList();
            return busInfoList;
        }



        
        //=====================END==========================//
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
