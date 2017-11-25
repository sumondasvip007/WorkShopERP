using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Repository;
using DAL.ViewModel;
using HanifWorkShop.Utility;

namespace HanifWorkShop.Controllers
{
    public class PartsInformationController : Controller
    {
        UnitOfWork unitOfWork=new UnitOfWork();
        // GET: PartsInformation
        [Authorize]
        [SessionManger.CheckUserSession]

        public ActionResult AddPartsInfo()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult AddPartsInfo(VM_PartsInfo partsInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tblPartsInfo aPartsInfo = new tblPartsInfo();

                    aPartsInfo.PartsName = partsInfo.PartsName;
                    aPartsInfo.BasePrice = partsInfo.BasePrice;
                    aPartsInfo.WorkShopId = Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                    aPartsInfo.CreatedBy = SessionManger.LoggedInUser(Session);
                    aPartsInfo.CreatedDateTime = DateTime.Now;
                    aPartsInfo.EditedBy = null;
                    aPartsInfo.EditedDateTime = null;

                    unitOfWork.PartsInfoRepository.Insert(aPartsInfo);
                    unitOfWork.Save();

                    return Json(new { success = true, successMessage = "Parts Information Added Successfully!" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, errorMessage = "Fill Up all required filled" }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetAllPartsInfo()
        {
            try
            {
                var partsList = (from a in unitOfWork.PartsInfoRepository.Get()
                                    select new VM_PartsInfo()
                                    {
                                        PartsId = a.PartsId,
                                        PartsName = a.PartsName,
                                        BasePrice = Convert.ToInt32(a.BasePrice)

                                    }).ToList();
                return Json(new { success = true, result = partsList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult ShowAllPartsInfo()
        {
            return View();
        }

        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult Delete(int? id)
        {

            try
            {
                var partsinfo = unitOfWork.PartsInfoRepository.GetByID(id);

                if (partsinfo != null)
                {
                    unitOfWork.PartsInfoRepository.Delete(partsinfo);
                    unitOfWork.Save();

                    return Json(new { success = true, successMessage = "Parts  info successfully Deleted" }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { success = false, errorMessage = "Parts information not Deleted" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult Update(VM_PartsInfo partsInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    tblPartsInfo aPartsInfo = unitOfWork.PartsInfoRepository.GetByID(partsInfo.PartsId);

                    aPartsInfo.PartsName = partsInfo.PartsName;
                    aPartsInfo.BasePrice = partsInfo.BasePrice;
                    aPartsInfo.EditedBy = SessionManger.LoggedInUser(Session);
                    aPartsInfo.EditedDateTime = DateTime.Now;



                    unitOfWork.PartsInfoRepository.Update(aPartsInfo);
                    unitOfWork.Save();

                    return Json(new { success = true, successMessage = "parts Info update successfully." }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {

                    return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, errorMessage = "Enter All required Field" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}