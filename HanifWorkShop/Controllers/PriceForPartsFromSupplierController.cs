using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Repository;
using DAL.ViewModel;
using HanifWorkShop.Utility;
using VM_BusInformation = HanifWorkShop.Models.ViewModel.VM_BusInformation;

namespace HanifWorkShop.Controllers
{
    public class PriceForPartsFromSupplierController : Controller
    {
        UnitOfWork unitOfWork=new UnitOfWork();
        // GET: PriceForPartsFromSupplier

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
        public JsonResult GetAllSuppliers()
        {
            try
            {
                var supplierList = (from a in unitOfWork.SupplierRepository.Get()
                                    select new VM_Supplier()
                                    {
                                        SupplierId = a.SupplierId,
                                        CompanyName = a.CompanyName,
                                        Address = a.Address,
                                        ContactNo = a.ContactNo

                                    }).ToList();
                return Json(new { success = true, result = supplierList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        [Authorize]
        [SessionManger.CheckUserSession]

        public ActionResult SearchBusInformationForSpecificBusRegistrationNo(string RegistrationNo)
        {
            try
            {
                List<DAL.ViewModel.VM_BusInformation> busInfoList = unitOfWork.CustomRepository.sp_BusInfoForSpecificBusRegistrationNo(RegistrationNo);

                if (busInfoList.Any())
                {
                    return Json(new { success = true, result = busInfoList }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, errorMessage = "No Bus found." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult AddPriceForPartsFromSupplier()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [SessionManger.CheckUserSession]
        //public ActionResult AddPriceForPartsFromSupplier(string registrationNo, DateTime buyDate, IEnumerable<VM_BuyPartsFromSupplier> buyPartsFromSupplier, string other, int? price)
        //{

 
          

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            if (buyPartsFromSupplier==null)
        //            {

        //                tblBuyPartsFromSupplier bBuyPartsFromSupplier = new tblBuyPartsFromSupplier();

        //                bBuyPartsFromSupplier.RegistrationNo = registrationNo;
        //                bBuyPartsFromSupplier.BuyDate = buyDate;
        //                bBuyPartsFromSupplier.PartsId = null;
        //                bBuyPartsFromSupplier.SupplierId = null;
        //                bBuyPartsFromSupplier.Quantity = null;
        //                bBuyPartsFromSupplier.UnitPrice = null;
        //                bBuyPartsFromSupplier.Other = other;
        //                bBuyPartsFromSupplier.Price = price;
        //                bBuyPartsFromSupplier.WorkShopId =
        //                    Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
        //                bBuyPartsFromSupplier.CreatedBy = SessionManger.LoggedInUser(Session);
        //                bBuyPartsFromSupplier.CreatedDateTime = DateTime.Now;
        //                bBuyPartsFromSupplier.EditedBy = null;
        //                bBuyPartsFromSupplier.EditedDateTime = null;

        //                unitOfWork.BuyPartsFromSupplierRepository.Insert(bBuyPartsFromSupplier);
        //                unitOfWork.Save();

        //            }
                  
        //            else
        //            {
        //                tblBuyPartsFromSupplier aBuyPartsFromSupplier = new tblBuyPartsFromSupplier();
        //                foreach (VM_BuyPartsFromSupplier buyPartsFromSuppliers in buyPartsFromSupplier)
        //                {



        //                    aBuyPartsFromSupplier.RegistrationNo = registrationNo;
        //                    aBuyPartsFromSupplier.BuyDate = buyDate;
        //                    aBuyPartsFromSupplier.PartsId = buyPartsFromSuppliers.PartsId;
        //                    aBuyPartsFromSupplier.SupplierId = buyPartsFromSuppliers.SupplierId;
        //                    aBuyPartsFromSupplier.Quantity = buyPartsFromSuppliers.Quantity;
        //                    aBuyPartsFromSupplier.UnitPrice = buyPartsFromSuppliers.UnitPrice;
        //                    aBuyPartsFromSupplier.Other = null;
        //                    aBuyPartsFromSupplier.Price = buyPartsFromSuppliers.Price;
        //                    aBuyPartsFromSupplier.WorkShopId =
        //                        Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
        //                    aBuyPartsFromSupplier.CreatedBy = SessionManger.LoggedInUser(Session);
        //                    aBuyPartsFromSupplier.CreatedDateTime = DateTime.Now;
        //                    aBuyPartsFromSupplier.EditedBy = null;
        //                    aBuyPartsFromSupplier.EditedDateTime = null;


        //                    unitOfWork.BuyPartsFromSupplierRepository.Insert(aBuyPartsFromSupplier);
        //                    unitOfWork.Save();
        //                }

        //                if (other != null || price != null)
        //                {
        //                    aBuyPartsFromSupplier.RegistrationNo = registrationNo;
        //                    aBuyPartsFromSupplier.BuyDate = buyDate;
        //                    aBuyPartsFromSupplier.PartsId = null;
        //                    aBuyPartsFromSupplier.SupplierId = null;
        //                    aBuyPartsFromSupplier.Quantity = null;
        //                    aBuyPartsFromSupplier.UnitPrice = null;
        //                    aBuyPartsFromSupplier.Other = other;
        //                    aBuyPartsFromSupplier.Price = price;
        //                    aBuyPartsFromSupplier.WorkShopId =
        //                        Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
        //                    aBuyPartsFromSupplier.CreatedBy = SessionManger.LoggedInUser(Session);
        //                    aBuyPartsFromSupplier.CreatedDateTime = DateTime.Now;
        //                    aBuyPartsFromSupplier.EditedBy = null;
        //                    aBuyPartsFromSupplier.EditedDateTime = null;

        //                    unitOfWork.BuyPartsFromSupplierRepository.Insert(aBuyPartsFromSupplier);
        //                    unitOfWork.Save();
        //                }
        //            }

        //            return Json(new { success = true, successMessage = " Added Successfully!" }, JsonRequestBehavior.AllowGet);

        //        }
        //        catch (Exception ex)
        //        {
        //            return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    else
        //    {
        //        return Json(new { success = false, errorMessage = "Model is not valid" }, JsonRequestBehavior.AllowGet);
        //    }


        //}

       
        public ActionResult AddPriceForPartsFromSupplier(string registrationNo, DateTime buyDate, IEnumerable<VM_BuyPartsFromSupplier> buyPartsFromSupplier, IEnumerable<VM_BuyPartsFromSupplier> otherExpense)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (buyPartsFromSupplier == null)
                    {

                        tblBuyPartsFromSupplier bBuyPartsFromSupplier = new tblBuyPartsFromSupplier();
                        foreach (VM_BuyPartsFromSupplier otherExpenses in otherExpense)
                        {
                            bBuyPartsFromSupplier.RegistrationNo = registrationNo;
                            bBuyPartsFromSupplier.BuyDate = buyDate;
                            bBuyPartsFromSupplier.PartsId = null;
                            bBuyPartsFromSupplier.SupplierId = null;
                            bBuyPartsFromSupplier.Quantity = null;
                            bBuyPartsFromSupplier.UnitPrice = null;
                            bBuyPartsFromSupplier.Other = otherExpenses.Other;
                            bBuyPartsFromSupplier.Price = otherExpenses.Price;
                            bBuyPartsFromSupplier.StoreId = null;
                            bBuyPartsFromSupplier.WorkShopId =
                                Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                            bBuyPartsFromSupplier.CreatedBy = SessionManger.LoggedInUser(Session);
                            bBuyPartsFromSupplier.CreatedDateTime = DateTime.Now;
                            bBuyPartsFromSupplier.EditedBy = null;
                            bBuyPartsFromSupplier.EditedDateTime = null;

                            unitOfWork.BuyPartsFromSupplierRepository.Insert(bBuyPartsFromSupplier);
                            unitOfWork.Save();

                        }
                    }

                    else
                    {
                        tblBuyPartsFromSupplier aBuyPartsFromSupplier = new tblBuyPartsFromSupplier();
                        foreach (VM_BuyPartsFromSupplier buyPartsFromSuppliers in buyPartsFromSupplier)
                        {



                            aBuyPartsFromSupplier.RegistrationNo = registrationNo;
                            aBuyPartsFromSupplier.BuyDate = buyDate;
                            aBuyPartsFromSupplier.PartsId = buyPartsFromSuppliers.PartsId;
                            aBuyPartsFromSupplier.SupplierId = buyPartsFromSuppliers.SupplierId;
                            aBuyPartsFromSupplier.Quantity = buyPartsFromSuppliers.Quantity;
                            aBuyPartsFromSupplier.UnitPrice = buyPartsFromSuppliers.UnitPrice;
                            aBuyPartsFromSupplier.Other = null;
                            aBuyPartsFromSupplier.StoreId = null;
                            aBuyPartsFromSupplier.Price = buyPartsFromSuppliers.Quantity * buyPartsFromSuppliers.UnitPrice;
                            aBuyPartsFromSupplier.WorkShopId =
                                Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                            aBuyPartsFromSupplier.CreatedBy = SessionManger.LoggedInUser(Session);
                            aBuyPartsFromSupplier.CreatedDateTime = DateTime.Now;
                            aBuyPartsFromSupplier.EditedBy = null;
                            aBuyPartsFromSupplier.EditedDateTime = null;


                            unitOfWork.BuyPartsFromSupplierRepository.Insert(aBuyPartsFromSupplier);
                            unitOfWork.Save();
                        }

                        if (otherExpense != null)
                        {
                            foreach (VM_BuyPartsFromSupplier othersExpenses in otherExpense)
                            {
                                aBuyPartsFromSupplier.RegistrationNo = registrationNo;
                                aBuyPartsFromSupplier.BuyDate = buyDate;
                                aBuyPartsFromSupplier.PartsId = null;
                                aBuyPartsFromSupplier.SupplierId = null;
                                aBuyPartsFromSupplier.Quantity = null;
                                aBuyPartsFromSupplier.UnitPrice = null;
                                aBuyPartsFromSupplier.Other = othersExpenses.Other;
                                aBuyPartsFromSupplier.Price = othersExpenses.Price;
                                aBuyPartsFromSupplier.StoreId = null;
                                aBuyPartsFromSupplier.WorkShopId =
                                    Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                                aBuyPartsFromSupplier.CreatedBy = SessionManger.LoggedInUser(Session);
                                aBuyPartsFromSupplier.CreatedDateTime = DateTime.Now;
                                aBuyPartsFromSupplier.EditedBy = null;
                                aBuyPartsFromSupplier.EditedDateTime = null;

                                unitOfWork.BuyPartsFromSupplierRepository.Insert(aBuyPartsFromSupplier);
                                unitOfWork.Save();
                            }
                        }
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
    }
}