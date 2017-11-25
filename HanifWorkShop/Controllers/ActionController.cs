using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Repository;
using HanifWorkShop.Models.ViewModel;
using HanifWorkShop.Utility;

namespace HanifWorkShop.Controllers
{
    public class ActionController : Controller
    {
        UnitOfWork unitOfWork=new UnitOfWork();
        // GET: Action
         [Authorize]
        [SessionManger.CheckUserSession]
       
        public ActionResult AddAction()
        {
            return View();
        }

        [HttpPost]
         [Authorize]
         [SessionManger.CheckUserSession]
        public JsonResult AddAction(VM_Action ViewAction)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tblAction aAction = new tblAction();

                    aAction.ActionName = ViewAction.ActionName;
                    aAction.ActionDisplayName = ViewAction.ActionDisplayName;
                    aAction.ActionUrl = ViewAction.ActionUrl;
                    aAction.ModuleId = ViewAction.ModuleId;
                    aAction.IsInMenu = ViewAction.IsInMenu;
                    aAction.IsView = ViewAction.IsView;
                    aAction.WorkShopId = Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                    aAction.CreatedBy = SessionManger.LoggedInUser(Session);
                    aAction.CreatedDateTime = DateTime.Now;
                    aAction.EditedBy = null;
                    aAction.EditedDateTime = null;

                    unitOfWork.ActionRepository.Insert(aAction);
                    unitOfWork.Save();

                    return Json(new { success = true, successMessage = "Action Added Successfully!" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, errorMessage = ex }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, errorMessage = "Fill Up all required filled" }, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult ShowAllAction()
        {
            return View();
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetAllAction()
        {
            var allActionList = (from a in unitOfWork.ActionRepository.Get()
                                 select new VM_Action()
                                 {
                                     ActionId = a.ActionId,
                                     ActionName = a.ActionName,
                                     ActionDisplayName = a.ActionDisplayName,
                                     ActionUrl = a.ActionUrl,
                                     ModuleId = (int) a.ModuleId,
                                     ModuleName = a.tblModule.ModuleName,
                                     IsInMenu = (bool) a.IsInMenu,
                                     IsView = (bool) a.IsView
                                 }).ToList();
            return Json(new { result = allActionList }, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetAllActionForSpecificModule(int ModuleId)
        //{
        //    var allActionList = (from a in unitOfWork.ActionRepository.Get().Where(a => a.ModuleId == ModuleId)
        //                         select new VM_Action()
        //                         {
        //                             ActionId = a.ActionId,
        //                             ActionDisplayName = a.ActionDisplayName,
        //                             ActionUrl = a.ActionUrl,
                                  
                                 
        //                         }).ToList();
        //    return Json(new { result = allActionList }, JsonRequestBehavior.AllowGet);
        //}


        [HttpPost]
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult Update(VM_Action viewAction)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    tblAction aAction = unitOfWork.ActionRepository.GetByID(viewAction.ActionId);

                    aAction.ActionName = viewAction.ActionName;
                    aAction.ActionDisplayName = viewAction.ActionDisplayName;
                    aAction.ActionUrl = viewAction.ActionUrl;
                    aAction.ModuleId = viewAction.ModuleId;
                    aAction.IsInMenu = viewAction.IsInMenu;
                    aAction.IsView = viewAction.IsView;
                    aAction.EditedBy = SessionManger.LoggedInUser(Session);
                    aAction.EditedDateTime = DateTime.Now;

                    unitOfWork.ActionRepository.Update(aAction);
                    unitOfWork.Save();

                    return Json(new { success = true, successMessage = "Action Info update successfully." }, JsonRequestBehavior.AllowGet);
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
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult Delete(int? id)
        {

            try
            {
                var action = unitOfWork.ActionRepository.GetByID(id);

                if (action != null)
                {
                    unitOfWork.ActionRepository.Delete(action);
                    unitOfWork.Save();

                    return Json(new { success = true, successMessage = "Action info successfully Deleted" }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { success = false, errorMessage = "Action information not save" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                return Json(new { success = false, errorMessage = ex }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}