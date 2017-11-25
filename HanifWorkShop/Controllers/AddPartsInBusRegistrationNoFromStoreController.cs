using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Repository;
using DAL.ViewModel;
using HanifWorkShop.Utility;
using Microsoft.Ajax.Utilities;
using VM_BusInformation = HanifWorkShop.Models.ViewModel.VM_BusInformation;

namespace HanifWorkShop.Controllers
{
    public class AddPartsInBusRegistrationNoFromStoreController : Controller
    {
        UnitOfWork unitOfWork=new UnitOfWork();
        // GET: AddPartsInBusRegistrationNoFromStore

        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult AddPartsInBusRegistrationNoFromStore()
        {
            return View();
        }

        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetAllRegistrationNo()
        {
            try
            {
                var busInformationList = (from a in unitOfWork.BusInformationRepository.Get()
                                          select new VM_BusInformation()
                                          {
                                              RegistrationNo = a.RegistrationNo
                                          }).ToList();

                return Json(new { success = true, result = busInformationList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetAllStore()
        {
            try
            {
                var storeList = (from a in unitOfWork.StoreRepository.Get()
                                          select new VM_Store()
                                          {
                                              StoreId = a.StoreId,
                                              StoreName = a.StoreName
                                          }).ToList();

                return Json(new { success = true, result = storeList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        [SessionManger.CheckUserSession]
        [Authorize]

        public JsonResult GetAllPartsByStore(int? id)
        {
            try
            {
          
                var storeInfo = unitOfWork.StoreRepository.Get().Where(s => s.StoreId == id).FirstOrDefault();

                var partsId = unitOfWork.PartsTransferRepository.Get().Where(a => a.StoreId == storeInfo.StoreId).DistinctBy(a=>a.tblPartsInfo.PartsId).ToList();
               

                //var partsId = unitOfWork.PartsTransferRepository.Get().Where(a =>
                //{
                //    return storeInfo != null && a.StoreId == storeInfo.StoreId;
                //}).ToList().DistinctBy(a => a.PartsId);
            
                //aProduct.OpeningProduct = getProductAvilableQty;


                var partsList = new List<Vm_PartsTransfetToBusRegistrationNoFromStore>();
                foreach (var aParts in partsId)
                {
                    if (aParts != null)
                    {
                        Vm_PartsTransfetToBusRegistrationNoFromStore newParts= new Vm_PartsTransfetToBusRegistrationNoFromStore();
                        var partsInfo = unitOfWork.PartsInfoRepository.GetByID(aParts.PartsId);

                        if (partsInfo != null)
                        {

                            //int partsIn = (int)partsId.Where(x => x.isIn == true && x.PartsId==partsInfo.PartsId).Select(x => x.Quantity).Sum();
                            //int partsOut = (int)partsId.Where(x => x.isOut == true && x.PartsId == partsInfo.PartsId).Select(x => x.Quantity).Sum();


                            int partsIn = (int)unitOfWork.PartsTransferRepository.Get().Where(x => x.isIn == true && x.PartsId == partsInfo.PartsId).Select(x => x.Quantity).Sum();
                            int partsOut = (int)unitOfWork.PartsTransferRepository.Get().Where(x => x.isOut == true && x.PartsId == partsInfo.PartsId).Select(x => x.Quantity).Sum();

                            int getProductAvilableQty = partsIn - partsOut;

                            if (getProductAvilableQty > 0)
                            {
                                newParts.StoreQuantity = Convert.ToInt32(aParts.Quantity);
                                newParts.PartsId = partsInfo.PartsId;
                                newParts.PartsName = partsInfo.PartsName;
                                newParts.UnitPrice = Convert.ToInt32(partsInfo.BasePrice);
                                newParts.AvailableQuatity = getProductAvilableQty;
                                newParts.Quantity = null ;
                                partsList.Add(newParts);
                            }
                        }
                    }
                }
                return Json(new { result = partsList, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult AddPartsInBusRegistrationNoFromStore(string registrationNo, DateTime buyDate, int storeId,List<Vm_PartsTransfetToBusRegistrationNoFromStore> partsList)
        {

            if (ModelState.IsValid)
            {
                try
                {
                   
                    foreach (Vm_PartsTransfetToBusRegistrationNoFromStore addPartFromStore in partsList)
                    {
                        tblBuyPartsFromSupplier addPartsFromStore = new tblBuyPartsFromSupplier();

                            addPartsFromStore.RegistrationNo = registrationNo;
                            addPartsFromStore.BuyDate = buyDate;
                            addPartsFromStore.PartsId = addPartFromStore.PartsId;
                            addPartsFromStore.SupplierId = null;
                            addPartsFromStore.Quantity = addPartFromStore.Quantity;
                            addPartsFromStore.UnitPrice = addPartFromStore.UnitPrice;
                            addPartsFromStore.Other = null;
                            addPartsFromStore.StoreId = storeId;
                            addPartsFromStore.Price = addPartFromStore.Quantity*addPartFromStore.UnitPrice;
                            addPartsFromStore.WorkShopId =
                                Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                            addPartsFromStore.CreatedBy = SessionManger.LoggedInUser(Session);
                            addPartsFromStore.CreatedDateTime = DateTime.Now;
                            addPartsFromStore.EditedBy = null;
                            addPartsFromStore.EditedDateTime = null;

                            unitOfWork.BuyPartsFromSupplierRepository.Insert(addPartsFromStore);
                            unitOfWork.Save();


                         tblPartsTransfer aPartsTransfer = new tblPartsTransfer();

                        aPartsTransfer.StoreId = storeId;
                        aPartsTransfer.Date = buyDate;
                        aPartsTransfer.PartsId = addPartFromStore.PartsId;
                        aPartsTransfer.Quantity = addPartFromStore.Quantity;
                        aPartsTransfer.UnitPrice = addPartFromStore.UnitPrice;
                        aPartsTransfer.Price = addPartFromStore.Quantity * addPartFromStore.UnitPrice;
                        aPartsTransfer.isIn = false;
                        aPartsTransfer.isOut = true;
                        aPartsTransfer.WorkShopId =
                            Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                        aPartsTransfer.CreatedBy = SessionManger.LoggedInUser(Session);
                        aPartsTransfer.CreatedDateTime = DateTime.Now;
                        aPartsTransfer.EditedBy = null;
                        aPartsTransfer.EditedDateTime = null;


                        unitOfWork.PartsTransferRepository.Insert(aPartsTransfer);
                        unitOfWork.Save();


                        }

                   

                    return Json(new { success = true, successMessage = " Added Successfully!" }, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {
                    return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, errorMessage = "Model is not valid" }, JsonRequestBehavior.AllowGet);
            }

        }



        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetPartsAvailableQuantity(
            int storeId, int partsId)
        {
            try
            {
                int getPartsAvilableQty = PartsAvailableQuantity(storeId, partsId);

         
                return Json(new { result = getPartsAvilableQty, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [Authorize]
        [SessionManger.CheckUserSession]
        private int PartsAvailableQuantity(
       int storeId, int partsId)
        {
            try
            {
                var store = unitOfWork.StoreRepository.GetByID(storeId);
                var product =
                    unitOfWork.PartsTransferRepository.Get()
                        .Where(x => x.PartsId == partsId && x.StoreId == store.StoreId)
                        .ToList();

                int ProductIn = (int)product.Where(x => x.isIn == true).Select(x => x.Quantity).Sum();
                int ProductOut = (int)product.Where(x => x.isOut == true).Select(x => x.Quantity).Sum();

                int getPartsAvilableQty = ProductIn - ProductOut;

           
                return getPartsAvilableQty;
            }
            catch (Exception exception)
            {
                return 0;
            }

        }
    }
}