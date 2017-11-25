using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Repository;
using HanifWorkShop.Models.ViewModel;
using HanifWorkShop.Utility;
using Microsoft.Reporting.WebForms;

namespace HanifWorkShop.Controllers
{
    public class DesignationController : Controller
    {
        UnitOfWork unitOfWork=new UnitOfWork();
        // GET: Designation

        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult AddDesignation()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult AddDesignation(VM_Designation designation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tblDesignation aDesignation=new tblDesignation();
                    aDesignation.DesignationName = designation.DesignationName;
                    aDesignation.WorkShopId = Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                    aDesignation.CreatedBy = SessionManger.LoggedInUser(Session);
                    aDesignation.CreatedDateTime = DateTime.Now;
                    aDesignation.EditedBy = null;
                    aDesignation.EditedDateTime = null;

                    unitOfWork.DesignationRepository.Insert(aDesignation);
                    unitOfWork.Save();

                    return Json(new { success = true, successMessage = "Designation Save Successfully" }, JsonRequestBehavior.AllowGet);

                }
                catch (Exception ex)
                {

                    return Json(new { success = false, errorMessage = ex }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, errorMessage = "Designation not Save" }, JsonRequestBehavior.AllowGet);
            }
         
        }

        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetAllDesignation()
        {
            var designationList = (from a in unitOfWork.DesignationRepository.Get()
                select new VM_Designation()
                {
                    DesignationId = a.DesignationId,
                    DesignationName = a.DesignationName
                }).ToList();

            return Json(new { success = true, result = designationList }, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult ShowAllDesignation()
        {
            return View();
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult Delete(int? id)
        {

            try
            {
                var dersignation = unitOfWork.DesignationRepository.GetByID(id);

                if (dersignation != null)
                {
                    unitOfWork.DesignationRepository.Delete(dersignation);
                    unitOfWork.Save();

                    return Json(new {success = true, successMessage = "Designation  info successfully Deleted" }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { success = false,errorMessage = "Designation information not Deleted" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                return Json(new { success = false, errorMessage = ex }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult Update(VM_Designation designation)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    tblDesignation aDesignation = unitOfWork.DesignationRepository.GetByID(designation.DesignationId);

                    aDesignation.DesignationName = designation.DesignationName;
                    aDesignation.EditedBy = SessionManger.LoggedInUser(Session);
                    aDesignation.EditedDateTime = DateTime.Now;



                    unitOfWork.DesignationRepository.Update(aDesignation);
                    unitOfWork.Save();

                    return Json(new { success = true, successMessage = "Designation Info update successfully." }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {

                    return Json(new { success = false, errorMessage = ex }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, errorMessage = "Model is not valid" }, JsonRequestBehavior.AllowGet);
            }
        }




    }



}