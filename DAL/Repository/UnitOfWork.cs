using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class UnitOfWork : IDisposable
    {
        private HanifWorkShop_DBEntity context = new HanifWorkShop_DBEntity();
        private GenericRepository<tblWorkShopUser> workShopUserRepository;
        private GenericRepository<AspNetUser> aspNetUserRepository;
        private GenericRepository<tblWorkShopInformation> workShopInformationRepository;
        private GenericRepository<tblModule> moduleRepository;
        private GenericRepository<tblAction> actionRepository;
        private GenericRepository<tblUserActionMapping> actionMappingRepository;
        private GenericRepository<tblDesignation> designationRepository;
        private GenericRepository<tblEmployeeInformation> employeeInformationRepository;
        private GenericRepository<tblRoute> routeRepository;
        private GenericRepository<tblBusInformation> busInformationRepository;
        private GenericRepository<tblSupplier> supplierRepository;
        private GenericRepository<tblPartsInfo> partsInfoRepository;
        private GenericRepository<tblBuyPartsFromSupplier> buyPartsFromSupplieRepository;
        private GenericRepository<tblStore> storeRepository;
        private GenericRepository<tblPartsTransfer> partsTransferRepository;
       



        private CustomRepository customRepository;
        public CustomRepository CustomRepository
        {
            get
            {

                if (this.customRepository == null)
                {
                    this.customRepository = new CustomRepository(context);
                }
                return customRepository;
            }
        }


        public GenericRepository<tblWorkShopUser> WorkShopUserRepository
        {
            get
            {

                if (this.workShopUserRepository == null)
                {
                    this.workShopUserRepository = new GenericRepository<tblWorkShopUser>(context);
                }
                return workShopUserRepository;
            }
        }

        public GenericRepository<AspNetUser> AspNetUserRepository
        {
            get
            {

                if (this.aspNetUserRepository == null)
                {
                    this.aspNetUserRepository = new GenericRepository<AspNetUser>(context);
                }
                return aspNetUserRepository;
            }
        }
        public GenericRepository<tblWorkShopInformation> WorkShopInformationRepository
        {
            get
            {

                if (this.workShopInformationRepository == null)
                {
                    this.workShopInformationRepository = new GenericRepository<tblWorkShopInformation>(context);
                }
                return workShopInformationRepository;
            }
        }
        public GenericRepository<tblModule> ModuleRepository
        {
            get
            {

                if (this.moduleRepository == null)
                {
                    this.moduleRepository = new GenericRepository<tblModule>(context);
                }
                return moduleRepository;
            }
        }
        public GenericRepository<tblAction> ActionRepository
        {
            get
            {

                if (this.actionRepository == null)
                {
                    this.actionRepository = new GenericRepository<tblAction>(context);
                }
                return actionRepository;
            }
        }

        public GenericRepository<tblUserActionMapping> UserActonMappingRepository
        {
            get
            {

                if (this.actionMappingRepository == null)
                {
                    this.actionMappingRepository = new GenericRepository<tblUserActionMapping>(context);
                }
                return actionMappingRepository;
            }
        }

        public GenericRepository<tblDesignation> DesignationRepository
        {
            get
            {

                if (this.designationRepository == null)
                {
                    this.designationRepository = new GenericRepository<tblDesignation>(context);
                }
                return designationRepository;
            }
        }
        public GenericRepository<tblEmployeeInformation> EmployeeInformationRepository
        {
            get
            {

                if (this.employeeInformationRepository == null)
                {
                    this.employeeInformationRepository = new GenericRepository<tblEmployeeInformation>(context);
                }
                return employeeInformationRepository;
            }
        }
        public GenericRepository<tblRoute> RouteRepository
        {
            get
            {

                if (this.routeRepository == null)
                {
                    this.routeRepository = new GenericRepository<tblRoute>(context);
                }
                return routeRepository;
            }
        }

        public GenericRepository<tblBusInformation> BusInformationRepository
        {
            get
            {

                if (this.busInformationRepository == null)
                {
                    this.busInformationRepository = new GenericRepository<tblBusInformation>(context);
                }
                return busInformationRepository;
            }
        }
        public GenericRepository<tblSupplier> SupplierRepository
        {
            get
            {

                if (this.supplierRepository == null)
                {
                    this.supplierRepository = new GenericRepository<tblSupplier>(context);
                }
                return supplierRepository;
            }
        }
        public GenericRepository<tblPartsInfo> PartsInfoRepository
        {
            get
            {

                if (this.partsInfoRepository == null)
                {
                    this.partsInfoRepository = new GenericRepository<tblPartsInfo>(context);
                }
                return partsInfoRepository;
            }
        }

        public GenericRepository<tblBuyPartsFromSupplier> BuyPartsFromSupplierRepository
        {
            get
            {

                if (this.buyPartsFromSupplieRepository == null)
                {
                    this.buyPartsFromSupplieRepository = new GenericRepository<tblBuyPartsFromSupplier>(context);
                }
                return buyPartsFromSupplieRepository;
            }
        }

        public GenericRepository<tblStore> StoreRepository
        {
            get
            {

                if (this.storeRepository == null)
                {
                    this.storeRepository = new GenericRepository<tblStore>(context);
                }
                return storeRepository;
            }
        }
        public GenericRepository<tblPartsTransfer> PartsTransferRepository
        {
            get
            {
                if (this.partsTransferRepository == null)
                {
                    this.partsTransferRepository = new GenericRepository<tblPartsTransfer>(context);
                }
                return partsTransferRepository;
            }
        }
        

        

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
