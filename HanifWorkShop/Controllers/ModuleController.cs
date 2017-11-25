using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Repository;
using HanifWorkShop.Models.ViewModel;
using HanifWorkShop.Utility;
using Microsoft.AspNet.Identity;

namespace HanifWorkShop.Controllers
{
    public class ModuleController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Module   
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult AddModule()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult AddModule(VM_Module module)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tblModule aModule = new tblModule();

                    aModule.ModuleName = module.ModuleName;
                    aModule.ModuleOrder = module.ModuleOrder;
                    aModule.ModuleIcon = module.ModuleIcon;
                    aModule.WorkShopId = Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                    aModule.CreatedBy = SessionManger.LoggedInUser(Session);
                    aModule.CreatedDateTime = DateTime.Now;
                    aModule.EditedBy = null;
                    aModule.EditedDateTime = null;

                    unitOfWork.ModuleRepository.Insert(aModule);
                    unitOfWork.Save();

                    return Json(new { success = true, successMessage = "Module Added Successfully!" }, JsonRequestBehavior.AllowGet);
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
        public JsonResult GetAllModule()
        {
      

            try
            {
                decimal id = SessionManger.WorkShopOfLoggedInUser(Session);
                string user_id = User.Identity.GetUserId();
                var result = unitOfWork.ModuleRepository.Get()
                .Select(a => new
                {
                    a.ModuleId,
                    a.ModuleName,
                    a.ModuleIcon,
                    action = a.tblActions.Where(b => b.IsInMenu == true && b.tblUserActionMappings.Where(am => am.user_id == user_id && am.is_permitted == 1).Any())
                    .Select(ac
                        => new
                        {
                            ac.ActionId,
                            ac.ActionName,
                            ac.ActionDisplayName,
                            ac.ActionUrl
                        }
                        ).ToList()
                }).Where(a => a.action.Count() > 0).ToList();
                return Json(new { result = result, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetModules()
        {
            try
            {
                var moduleList = (from a in unitOfWork.ModuleRepository.Get()
                                select new VM_Module()
                                {
                                  ModuleId=a.ModuleId,
                                  ModuleName = a.ModuleName,
                                  ModuleIcon=a.ModuleIcon,
                                  ModuleOrder = (int) a.ModuleOrder
                                  
                                }).ToList();
                return Json(new { success = true, result = moduleList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult ShowAllModule()
        {
            return View();
        }


        [HttpPost]
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult Update(VM_Module module)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    tblModule aModule = unitOfWork.ModuleRepository.GetByID(module.ModuleId);

                    aModule.ModuleName = module.ModuleName;
                    aModule.ModuleOrder = module.ModuleOrder;
                    aModule.ModuleIcon = module.ModuleIcon;
                    aModule.EditedBy = SessionManger.LoggedInUser(Session);
                    aModule.EditedDateTime = DateTime.Now;
                    

                    unitOfWork.ModuleRepository.Update(aModule);
                    unitOfWork.Save();

                    return Json(new { success = true, successMessage = "Module Info update successfully." }, JsonRequestBehavior.AllowGet);
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
                var module = unitOfWork.ModuleRepository.GetByID(id);

                if (module != null)
                {
                    unitOfWork.ModuleRepository.Delete(module);
                    unitOfWork.Save();

                    return Json(new { success = true, successMessage = "Module info successfully Deleted" }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { success = false, errorMessage = "Module information not save" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                return Json(new { success = false, errorMessage = ex }, JsonRequestBehavior.AllowGet);
            }

        }

    }

}