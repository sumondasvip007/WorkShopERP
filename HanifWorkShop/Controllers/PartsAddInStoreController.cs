using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Repository;
using DAL.ViewModel;
using HanifWorkShop.Models.ViewModel;
using HanifWorkShop.Utility;

namespace HanifWorkShop.Controllers
{
    public class PartsAddInStoreController : Controller
    {
        UnitOfWork unitOfWork=new UnitOfWork();
        // GET: PartsAddInStore

        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult AddPartsInStore()
        {
            return View();
        }



        [HttpPost]
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult AddPartsInStore(DateTime addDate, IEnumerable<VM_PartsTransfer> addPartInStore)
        {
            if (ModelState.IsValid)
            {
                try
                {

                        tblPartsTransfer aPartsTransfer = new tblPartsTransfer();
                        foreach (VM_PartsTransfer bPartsTransfer in addPartInStore)
                        {
                            aPartsTransfer.StoreId = bPartsTransfer.StoreId;
                            aPartsTransfer.Date = addDate;
                            aPartsTransfer.PartsId = bPartsTransfer.PartsId;
                            aPartsTransfer.Quantity = bPartsTransfer.Quantity;
                            aPartsTransfer.UnitPrice = bPartsTransfer.UnitPrice;
                            aPartsTransfer.Price = bPartsTransfer.Quantity * bPartsTransfer.UnitPrice;
                            aPartsTransfer.isIn = true;
                            aPartsTransfer.isOut = false;
                            aPartsTransfer.WorkShopId =
                                Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                            aPartsTransfer.CreatedBy = SessionManger.LoggedInUser(Session);
                            aPartsTransfer.CreatedDateTime = DateTime.Now;
                            aPartsTransfer.EditedBy = null;
                            aPartsTransfer.EditedDateTime = null;


                            unitOfWork.PartsTransferRepository.Insert(aPartsTransfer);
                            unitOfWork.Save();
                        }
                    return Json(new { success = true, successMessage = "Parts  Added Successfully in Store." }, JsonRequestBehavior.AllowGet);

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
        public JsonResult GetAllStore()
        {
            var storeList = (from a in unitOfWork.StoreRepository.Get()
                                   select new VM_Store()
                                   {
                                       StoreId = a.StoreId,
                                       StoreName = a.StoreName
                                   }).ToList();

            return Json(new { success = true, result = storeList }, JsonRequestBehavior.AllowGet);
        }


    }
}