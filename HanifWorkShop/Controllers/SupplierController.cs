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
    public class SupplierController : Controller
    {
        UnitOfWork unitOfWork=new UnitOfWork();
        // GET: Supplier

        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult AddSupplier()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult AddSupplier(VM_Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tblSupplier aSupplier = new tblSupplier();

                    aSupplier.CompanyName = supplier.CompanyName;
                    aSupplier.Address = supplier.Address;
                    aSupplier.ContactNo = supplier.ContactNo;
                    aSupplier.WorkShopId = Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                    aSupplier.CreatedBy = SessionManger.LoggedInUser(Session);
                    aSupplier.CreatedDateTime = DateTime.Now;
                    aSupplier.EditedBy = null;
                    aSupplier.EditedDateTime = null;

                    unitOfWork.SupplierRepository.Insert(aSupplier);
                    unitOfWork.Save();

                    return Json(new { success = true, successMessage = "Supplier Added Successfully!" }, JsonRequestBehavior.AllowGet);
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

        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult ShowAllSupplier()
        {
            return View();
        }

        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult Delete(int? id)
        {

            try
            {
                var supplier = unitOfWork.SupplierRepository.GetByID(id);

                if (supplier != null)
                {
                    unitOfWork.SupplierRepository.Delete(supplier);
                    unitOfWork.Save();

                    return Json(new { success = true, successMessage = "Supplier  info successfully Deleted" }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { success = false, errorMessage = "Supplier information not Deleted" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult Update(VM_Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    tblSupplier aSupplier = unitOfWork.SupplierRepository.GetByID(supplier.SupplierId);

                    aSupplier.CompanyName = supplier.CompanyName;
                    aSupplier.Address = supplier.Address;
                    aSupplier.ContactNo = supplier.ContactNo;
                    aSupplier.EditedBy = SessionManger.LoggedInUser(Session);
                    aSupplier.EditedDateTime = DateTime.Now;



                    unitOfWork.SupplierRepository.Update(aSupplier);
                    unitOfWork.Save();

                    return Json(new { success = true, successMessage = "Supplier Info update successfully." }, JsonRequestBehavior.AllowGet);
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