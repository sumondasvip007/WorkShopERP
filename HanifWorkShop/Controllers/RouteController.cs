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
    public class RouteController : Controller
    {
        UnitOfWork unitOfWork=new UnitOfWork();
        // GET: Route
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult AddRoute()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult AddRoute(VM_Route route)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tblRoute aRoute = new tblRoute();
                    aRoute.RouteName = route.RouteName;
                    aRoute.WorkShopId = Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                    aRoute.CreatedBy = SessionManger.LoggedInUser(Session);
                    aRoute.CreatedDateTime = DateTime.Now;
                    aRoute.EditedBy = null;
                    aRoute.EditedDateTime = null;

                    unitOfWork.RouteRepository.Insert(aRoute);
                    unitOfWork.Save();

                    return Json(new { success = true, successMessage = "Bus Route Added Successfully." }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {

                    return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, errorMessage = "Model is not Valid" }, JsonRequestBehavior.AllowGet);
            }
            
           
        }


        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetAllRoute()
        {
            var routeList = (from a in unitOfWork.RouteRepository.Get()
                                   select new VM_Route()
                                   {
                                       RouteId = a.RouteId,
                                       RouteName = a.RouteName
                                   }).ToList();

            return Json(new { success = true, result = routeList }, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult ShowAllRoute()
        {
            return View();
        }

        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult Delete(int? id)
        {

            try
            {
                var route = unitOfWork.RouteRepository.GetByID(id);

                if (route != null)
                {
                    unitOfWork.RouteRepository.Delete(route);
                    unitOfWork.Save();

                    return Json(new { success = true, successMessage = "Route  info successfully Deleted" }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { success = false, errorMessage = "Route information not save" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult Update(VM_Route route)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    tblRoute aRoute = unitOfWork.RouteRepository.GetByID(route.RouteId);

                    aRoute.RouteName = route.RouteName;
                    aRoute.EditedBy = SessionManger.LoggedInUser(Session);
                    aRoute.EditedDateTime = DateTime.Now;


                    unitOfWork.RouteRepository.Update(aRoute);
                    unitOfWork.Save();

                    return Json(new { success = true, successMessage = "Route Info update successfully." }, JsonRequestBehavior.AllowGet);
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