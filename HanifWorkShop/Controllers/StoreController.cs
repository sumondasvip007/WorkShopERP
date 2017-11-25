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
    public class StoreController : Controller
    {
        UnitOfWork unitOfWork=new UnitOfWork();
        // GET: Store

        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult AddStore()
        {
            return View();
        }


        [HttpPost]
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult AddStore(VM_Store store)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tblStore aStore = new tblStore();
                    aStore.StoreName = store.StoreName;
                    aStore.WorkShopId = Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                    aStore.CreatedBy = SessionManger.LoggedInUser(Session);
                    aStore.CreatedDateTime = DateTime.Now;
                    aStore.EditedBy = null;
                    aStore.EditedDateTime = null;

                    unitOfWork.StoreRepository.Insert(aStore);
                    unitOfWork.Save();

                    return Json(new { success = true, successMessage = "Store Save Successfully" }, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {

                    return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, errorMessage = "Store not Save" }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}