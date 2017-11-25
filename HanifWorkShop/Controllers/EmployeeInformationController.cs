using System;
using System.Collections.Generic;
using System.IO;
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
    public class EmployeeInformationController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: EmployeeInformation
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult SaveEmployeeInformation()
        {
            return View();

        }

        [HttpPost]
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult SaveEmployeeInformation(HttpPostedFileBase file, HttpPostedFileBase nidFile, HttpPostedFileBase drivingLicenseFile, VM_EmployeeInformation aEmployee)
        {

            //string emptyEmail = "undefined";
            var EmailExist = unitOfWork.EmployeeInformationRepository.Get()
              .Where(a => a.EmployeeEmail == aEmployee.EmployeeEmail && a.EmployeeEmail != "undefined" && a.EmployeeEmail != "").ToList();


            if (ModelState.IsValid)
            {
                try
                {
                    if (!EmailExist.Any())
                    {
                        string path = "";
                        if (file != null)
                        {
                            string pic = System.IO.Path.GetFileName(file.FileName);
                            path = System.IO.Path.Combine(
                                Server.MapPath("~/Image"), pic);
                            // file is uploaded
                            file.SaveAs(path);

                            if (nidFile != null)
                            {

                                string picNid = System.IO.Path.GetFileName(nidFile.FileName);
                                path = System.IO.Path.Combine(
                                    Server.MapPath("~/Image"), picNid);
                                // file is uploaded
                                nidFile.SaveAs(path);

                                if (drivingLicenseFile != null)
                                {
                                    string picDrivingLicense = System.IO.Path.GetFileName(drivingLicenseFile.FileName);
                                    path = System.IO.Path.Combine(
                                        Server.MapPath("~/Image"), picDrivingLicense);
                                    // file is uploaded
                                    drivingLicenseFile.SaveAs(path);

                                    tblEmployeeInformation employee = new tblEmployeeInformation();
                                    employee.EmployeeName = aEmployee.EmployeeName;
                                    employee.EmployeeAddress = aEmployee.EmployeeAddress;
                                    employee.ContactNumber = aEmployee.ContactNumber;
                                    employee.EmployeeNid = aEmployee.EmployeeNid;
                                    employee.EmployeeNidImage = "/Image/" + picNid;
                                    employee.EmployeeEmail = aEmployee.EmployeeEmail;
                                    employee.EmployeeImage = "/Image/" + pic;
                                    employee.DesignationId = aEmployee.DesignationId;
                                    employee.DrivingLicenseImage = "/Image/" + picDrivingLicense;
                                    employee.Salary = aEmployee.Salary;
                                    employee.RouteId = aEmployee.RouteId;
                                    employee.WorkShopId =
                                        Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                                    employee.CreatedBy = SessionManger.LoggedInUser(Session);
                                    employee.CreatedDateTime = DateTime.Now;
                                    employee.EditedBy = null;
                                    employee.EditedDateTime = null;

                                    unitOfWork.EmployeeInformationRepository.Insert(employee);
                                    unitOfWork.Save();
                                }

                                else
                                {
                                    tblEmployeeInformation employee = new tblEmployeeInformation();
                                    employee.EmployeeName = aEmployee.EmployeeName;
                                    employee.EmployeeAddress = aEmployee.EmployeeAddress;
                                    employee.ContactNumber = aEmployee.ContactNumber;
                                    employee.EmployeeNid = aEmployee.EmployeeNid;
                                    employee.EmployeeNidImage = "/Image/" + picNid;
                                    employee.EmployeeEmail = aEmployee.EmployeeEmail;
                                    employee.EmployeeImage = "/Image/" + pic;
                                    employee.DesignationId = aEmployee.DesignationId;
                                    employee.DrivingLicenseImage = aEmployee.DrivingLicenseImage;
                                    employee.Salary = aEmployee.Salary;
                                    employee.RouteId = aEmployee.RouteId;
                                    employee.WorkShopId =
                                        Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                                    employee.CreatedBy = SessionManger.LoggedInUser(Session);
                                    employee.CreatedDateTime = DateTime.Now;
                                    employee.EditedBy = null;
                                    employee.EditedDateTime = null;

                                    unitOfWork.EmployeeInformationRepository.Insert(employee);
                                    unitOfWork.Save();
                                }



                            }
                            else
                            {
                                if (drivingLicenseFile != null)
                                {
                                    string picDrivingLicense = System.IO.Path.GetFileName(drivingLicenseFile.FileName);
                                    path = System.IO.Path.Combine(
                                        Server.MapPath("~/Image"), picDrivingLicense);
                                    // file is uploaded
                                    drivingLicenseFile.SaveAs(path);


                                    tblEmployeeInformation employee = new tblEmployeeInformation();
                                    employee.EmployeeName = aEmployee.EmployeeName;
                                    employee.EmployeeAddress = aEmployee.EmployeeAddress;
                                    employee.ContactNumber = aEmployee.ContactNumber;
                                    employee.EmployeeNid = aEmployee.EmployeeNid;
                                    employee.EmployeeNidImage = aEmployee.EmployeeNidImage;
                                    employee.EmployeeEmail = aEmployee.EmployeeEmail;
                                    employee.EmployeeImage = "/Image/" + pic;
                                    employee.DesignationId = aEmployee.DesignationId;
                                    employee.DrivingLicenseImage = "/Image/" + picDrivingLicense;
                                    employee.Salary = aEmployee.Salary;
                                    employee.RouteId = aEmployee.RouteId;
                                    employee.WorkShopId =
                                        Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                                    employee.CreatedBy = SessionManger.LoggedInUser(Session);
                                    employee.CreatedDateTime = DateTime.Now;
                                    employee.EditedBy = null;
                                    employee.EditedDateTime = null;

                                    unitOfWork.EmployeeInformationRepository.Insert(employee);
                                    unitOfWork.Save();
                                }
                                else
                                {
                                    tblEmployeeInformation employee = new tblEmployeeInformation();
                                    employee.EmployeeName = aEmployee.EmployeeName;
                                    employee.EmployeeAddress = aEmployee.EmployeeAddress;
                                    employee.ContactNumber = aEmployee.ContactNumber;
                                    employee.EmployeeNid = aEmployee.EmployeeNid;
                                    employee.EmployeeNidImage = aEmployee.EmployeeNidImage;
                                    employee.EmployeeEmail = aEmployee.EmployeeEmail;
                                    employee.EmployeeImage = "/Image/" + pic;
                                    employee.DesignationId = aEmployee.DesignationId;
                                    employee.DrivingLicenseImage = aEmployee.DrivingLicenseImage;
                                    employee.Salary = aEmployee.Salary;
                                    employee.RouteId = aEmployee.RouteId;
                                    employee.WorkShopId =
                                        Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                                    employee.CreatedBy = SessionManger.LoggedInUser(Session);
                                    employee.CreatedDateTime = DateTime.Now;
                                    employee.EditedBy = null;
                                    employee.EditedDateTime = null;

                                    unitOfWork.EmployeeInformationRepository.Insert(employee);
                                    unitOfWork.Save();
                                }
                            }
                        }
                        else
                        {

                            if (nidFile != null)
                            {
                                string picNid = System.IO.Path.GetFileName(nidFile.FileName);
                                path = System.IO.Path.Combine(
                                    Server.MapPath("~/Image"), picNid);
                                // file is uploaded
                                nidFile.SaveAs(path);

                                if (drivingLicenseFile != null)
                                {
                                    string picDrivingLicense = System.IO.Path.GetFileName(drivingLicenseFile.FileName);
                                    path = System.IO.Path.Combine(
                                        Server.MapPath("~/Image"), picDrivingLicense);
                                    // file is uploaded
                                    drivingLicenseFile.SaveAs(path);


                                    tblEmployeeInformation employee = new tblEmployeeInformation();
                                    employee.EmployeeName = aEmployee.EmployeeName;
                                    employee.EmployeeAddress = aEmployee.EmployeeAddress;
                                    employee.ContactNumber = aEmployee.ContactNumber;
                                    employee.EmployeeNid = aEmployee.EmployeeNid;
                                    employee.EmployeeNidImage = "/Image/" + picNid;
                                    employee.EmployeeEmail = aEmployee.EmployeeEmail;
                                    employee.EmployeeImage = aEmployee.EmployeeImage;
                                    employee.DesignationId = aEmployee.DesignationId;
                                    employee.DrivingLicenseImage = "/Image/" + picDrivingLicense;
                                    employee.Salary = aEmployee.Salary;
                                    employee.RouteId = aEmployee.RouteId;
                                    employee.WorkShopId =
                                        Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                                    employee.CreatedBy = SessionManger.LoggedInUser(Session);
                                    employee.CreatedDateTime = DateTime.Now;
                                    employee.EditedBy = null;
                                    employee.EditedDateTime = null;
                                    unitOfWork.EmployeeInformationRepository.Insert(employee);
                                    unitOfWork.Save();
                                }
                                else
                                {
                                    tblEmployeeInformation employee = new tblEmployeeInformation();
                                    employee.EmployeeName = aEmployee.EmployeeName;
                                    employee.EmployeeAddress = aEmployee.EmployeeAddress;
                                    employee.ContactNumber = aEmployee.ContactNumber;
                                    employee.EmployeeNid = aEmployee.EmployeeNid;
                                    employee.EmployeeNidImage = "/Image/" + picNid;
                                    employee.EmployeeEmail = aEmployee.EmployeeEmail;
                                    employee.EmployeeImage = aEmployee.EmployeeImage;
                                    employee.DesignationId = aEmployee.DesignationId;
                                    employee.DrivingLicenseImage = aEmployee.DrivingLicenseImage;
                                    employee.Salary = aEmployee.Salary;
                                    employee.RouteId = aEmployee.RouteId;
                                    employee.WorkShopId =
                                        Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                                    employee.CreatedBy = SessionManger.LoggedInUser(Session);
                                    employee.CreatedDateTime = DateTime.Now;
                                    employee.EditedBy = null;
                                    employee.EditedDateTime = null;
                                    unitOfWork.EmployeeInformationRepository.Insert(employee);
                                    unitOfWork.Save();
                                }
                            }
                            else
                            {
                                if (drivingLicenseFile != null)
                                {
                                    string picDrivingLicense = System.IO.Path.GetFileName(drivingLicenseFile.FileName);
                                    path = System.IO.Path.Combine(
                                        Server.MapPath("~/Image"), picDrivingLicense);
                                    // file is uploaded
                                    drivingLicenseFile.SaveAs(path);


                                    tblEmployeeInformation employee = new tblEmployeeInformation();
                                    employee.EmployeeName = aEmployee.EmployeeName;
                                    employee.EmployeeAddress = aEmployee.EmployeeAddress;
                                    employee.ContactNumber = aEmployee.ContactNumber;
                                    employee.EmployeeNid = aEmployee.EmployeeNid;
                                    employee.EmployeeNidImage = aEmployee.EmployeeNidImage;
                                    employee.EmployeeEmail = aEmployee.EmployeeEmail;
                                    employee.EmployeeImage = aEmployee.EmployeeImage;
                                    employee.DesignationId = aEmployee.DesignationId;
                                    employee.DrivingLicenseImage = "/Image/" + picDrivingLicense;
                                    employee.Salary = aEmployee.Salary;
                                    employee.RouteId = aEmployee.RouteId;
                                    employee.WorkShopId =
                                        Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                                    employee.CreatedBy = SessionManger.LoggedInUser(Session);
                                    employee.CreatedDateTime = DateTime.Now;
                                    employee.EditedBy = null;
                                    employee.EditedDateTime = null;
                                    unitOfWork.EmployeeInformationRepository.Insert(employee);
                                    unitOfWork.Save();
                                }
                                else
                                {
                                    tblEmployeeInformation employee = new tblEmployeeInformation();
                                    employee.EmployeeName = aEmployee.EmployeeName;
                                    employee.EmployeeAddress = aEmployee.EmployeeAddress;
                                    employee.ContactNumber = aEmployee.ContactNumber;
                                    employee.EmployeeNid = aEmployee.EmployeeNid;
                                    employee.EmployeeNidImage = aEmployee.EmployeeNidImage;
                                    employee.EmployeeEmail = aEmployee.EmployeeEmail;
                                    employee.EmployeeImage = aEmployee.EmployeeImage;
                                    employee.DesignationId = aEmployee.DesignationId;
                                    employee.DrivingLicenseImage = aEmployee.DrivingLicenseImage;
                                    employee.Salary = aEmployee.Salary;
                                    employee.RouteId = aEmployee.RouteId;
                                    employee.WorkShopId =
                                        Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                                    employee.CreatedBy = SessionManger.LoggedInUser(Session);
                                    employee.CreatedDateTime = DateTime.Now;
                                    employee.EditedBy = null;
                                    employee.EditedDateTime = null;
                                    unitOfWork.EmployeeInformationRepository.Insert(employee);
                                    unitOfWork.Save();
                                }
                            }
                        }
                        return Json(new { success = true, successMessage = "Employee Information Added Successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, errorMessage = "The email already exist" }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, errorMessage = "Please Fill Up all required field." }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionManger.CheckUserSession]

        public ActionResult EmployeeInformationList()
        {
            return View();
        }
        [SessionManger.CheckUserSession]
        [Authorize]
        public JsonResult GetAllEmployeeInformation()
        {
            try
            {

                var employeeInformationList = (from a in unitOfWork.EmployeeInformationRepository.Get()
                                               select new VM_EmployeeInformation()
                                               {
                                                   EmployeeId = a.EmployeeId,
                                                   EmployeeName = a.EmployeeName,
                                                   EmployeeAddress = a.EmployeeAddress,
                                                   ContactNumber = a.ContactNumber,
                                                   EmployeeNid = a.EmployeeNid,
                                                   EmployeeNidImage = a.EmployeeNidImage,
                                                   EmployeeEmail = a.EmployeeEmail,
                                                   EmployeeImage = a.EmployeeImage,
                                                   DesignationId = Convert.ToInt32(a.DesignationId),
                                                   DesignationName = a.tblDesignation.DesignationName,
                                                   DrivingLicenseImage = a.DrivingLicenseImage,
                                                   Salary = (int) a.Salary,
                                                   RouteId = (int)a.RouteId,
                                                   RouteName = a.tblRoute.RouteName,


                                               }).ToList();
                return Json(new { success = true, result = employeeInformationList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult Delete(int? id)
        {

            try
            {
                var employee = unitOfWork.EmployeeInformationRepository.GetByID(id);

                if (employee != null)
                {
                    unitOfWork.EmployeeInformationRepository.Delete(employee);
                    unitOfWork.Save();

                    return Json(new { success = true, successMessage = "Employee  info successfully Deleted" }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { success = false, errorMessage = "Employee information not save" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult Update(HttpPostedFileBase file, HttpPostedFileBase nidFile, HttpPostedFileBase drivingLicenseFile, VM_EmployeeInformation aEmployee)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    tblEmployeeInformation aEmployeeInformation = unitOfWork.EmployeeInformationRepository.GetByID(aEmployee.EmployeeId);
                    var EmailExist = unitOfWork.EmployeeInformationRepository.Get()
              .Where(a => a.EmployeeEmail == aEmployee.EmployeeEmail && a.EmployeeEmail != aEmployeeInformation.EmployeeEmail && a.EmployeeEmail != "undefined" && a.EmployeeEmail != null && a.EmployeeEmail != " " && a.EmployeeEmail != "NULL" && a.EmployeeEmail != "null" && !string.IsNullOrEmpty(a.EmployeeEmail)).ToList();


                    if (!EmailExist.Any())
                    {
                        string path = "";
                        if (file != null)
                        {
                            string pic = System.IO.Path.GetFileName(file.FileName);
                            path = System.IO.Path.Combine(
                                Server.MapPath("~/Image"), pic);
                            // file is uploaded
                            file.SaveAs(path);

                            if (nidFile != null)
                            {

                                string picNid = System.IO.Path.GetFileName(nidFile.FileName);
                                path = System.IO.Path.Combine(
                                    Server.MapPath("~/Image"), picNid);
                                // file is uploaded
                                nidFile.SaveAs(path);

                                if (drivingLicenseFile != null)
                                {
                                    string picDrivingLicense = System.IO.Path.GetFileName(drivingLicenseFile.FileName);
                                    path = System.IO.Path.Combine(
                                        Server.MapPath("~/Image"), picDrivingLicense);
                                    // file is uploaded
                                    drivingLicenseFile.SaveAs(path);


                                    aEmployeeInformation.EmployeeName = aEmployee.EmployeeName;
                                    aEmployeeInformation.EmployeeAddress = aEmployee.EmployeeAddress;
                                    aEmployeeInformation.ContactNumber = aEmployee.ContactNumber;
                                    aEmployeeInformation.EmployeeNid = aEmployee.EmployeeNid;
                                    aEmployeeInformation.EmployeeNidImage = "/Image/" + picNid;
                                    aEmployeeInformation.EmployeeEmail = aEmployee.EmployeeEmail;
                                    aEmployeeInformation.EmployeeImage = "/Image/" + pic;
                                    aEmployeeInformation.DesignationId = aEmployee.DesignationId;
                                    aEmployeeInformation.DrivingLicenseImage = "/Image/" + picDrivingLicense;
                                    aEmployeeInformation.Salary = aEmployee.Salary;
                                    aEmployeeInformation.RouteId = aEmployee.RouteId;
                                    aEmployeeInformation.EditedBy = SessionManger.LoggedInUser(Session);
                                    aEmployeeInformation.EditedDateTime = DateTime.Now;

                                    unitOfWork.EmployeeInformationRepository.Update(aEmployeeInformation);
                                    unitOfWork.Save();
                                }
                                else
                                {

                                    aEmployeeInformation.EmployeeName = aEmployee.EmployeeName;
                                    aEmployeeInformation.EmployeeAddress = aEmployee.EmployeeAddress;
                                    aEmployeeInformation.ContactNumber = aEmployee.ContactNumber;
                                    aEmployeeInformation.EmployeeNid = aEmployee.EmployeeNid;
                                    aEmployeeInformation.EmployeeNidImage = "/Image/" + picNid;
                                    aEmployeeInformation.EmployeeEmail = aEmployee.EmployeeEmail;
                                    aEmployeeInformation.EmployeeImage = "/Image/" + pic;
                                    aEmployeeInformation.DesignationId = aEmployee.DesignationId;
                                    aEmployeeInformation.DrivingLicenseImage = aEmployee.DrivingLicenseImage;
                                    aEmployeeInformation.Salary = aEmployee.Salary;
                                    aEmployeeInformation.RouteId = aEmployee.RouteId;
                                    aEmployeeInformation.EditedBy = SessionManger.LoggedInUser(Session);
                                    aEmployeeInformation.EditedDateTime = DateTime.Now;

                                    unitOfWork.EmployeeInformationRepository.Update(aEmployeeInformation);
                                    unitOfWork.Save();
                                }
                            }
                            else
                            {
                                if (drivingLicenseFile != null)
                                {
                                    string picDrivingLicense = System.IO.Path.GetFileName(drivingLicenseFile.FileName);
                                    path = System.IO.Path.Combine(
                                        Server.MapPath("~/Image"), picDrivingLicense);
                                    // file is uploaded
                                    drivingLicenseFile.SaveAs(path);

                                    aEmployeeInformation.EmployeeName = aEmployee.EmployeeName;
                                    aEmployeeInformation.EmployeeAddress = aEmployee.EmployeeAddress;
                                    aEmployeeInformation.ContactNumber = aEmployee.ContactNumber;
                                    aEmployeeInformation.EmployeeNid = aEmployee.EmployeeNid;
                                    aEmployeeInformation.EmployeeNidImage = aEmployee.EmployeeNidImage;
                                    aEmployeeInformation.EmployeeEmail = aEmployee.EmployeeEmail;
                                    aEmployeeInformation.EmployeeImage = "/Image/" + pic;
                                    aEmployeeInformation.DesignationId = aEmployee.DesignationId;
                                    aEmployeeInformation.DrivingLicenseImage = "/Image/" + picDrivingLicense;
                                    aEmployeeInformation.Salary = aEmployee.Salary;
                                    aEmployeeInformation.RouteId = aEmployee.RouteId;
                                    aEmployeeInformation.EditedBy = SessionManger.LoggedInUser(Session);
                                    aEmployeeInformation.EditedDateTime = DateTime.Now;

                                    unitOfWork.EmployeeInformationRepository.Update(aEmployeeInformation);
                                    unitOfWork.Save();
                                }

                                else
                                {
                                    aEmployeeInformation.EmployeeName = aEmployee.EmployeeName;
                                    aEmployeeInformation.EmployeeAddress = aEmployee.EmployeeAddress;
                                    aEmployeeInformation.ContactNumber = aEmployee.ContactNumber;
                                    aEmployeeInformation.EmployeeNid = aEmployee.EmployeeNid;
                                    aEmployeeInformation.EmployeeNidImage = aEmployee.EmployeeNidImage;
                                    aEmployeeInformation.EmployeeEmail = aEmployee.EmployeeEmail;
                                    aEmployeeInformation.EmployeeImage = "/Image/" + pic;
                                    aEmployeeInformation.DesignationId = aEmployee.DesignationId;
                                    aEmployeeInformation.DrivingLicenseImage = aEmployee.DrivingLicenseImage;
                                    aEmployeeInformation.Salary = aEmployee.Salary;
                                    aEmployeeInformation.RouteId = aEmployee.RouteId;
                                    aEmployeeInformation.EditedBy = SessionManger.LoggedInUser(Session);
                                    aEmployeeInformation.EditedDateTime = DateTime.Now;

                                    unitOfWork.EmployeeInformationRepository.Update(aEmployeeInformation);
                                    unitOfWork.Save();
                                }

                            }
                        }
                        else
                        {
                            if (nidFile != null)
                            {
                                string picNid = System.IO.Path.GetFileName(nidFile.FileName);
                                path = System.IO.Path.Combine(
                                    Server.MapPath("~/Image"), picNid);
                                // file is uploaded
                                nidFile.SaveAs(path);

                                if (drivingLicenseFile != null)
                                {
                                    string picDrivingLicense = System.IO.Path.GetFileName(drivingLicenseFile.FileName);
                                    path = System.IO.Path.Combine(
                                        Server.MapPath("~/Image"), picDrivingLicense);
                                    // file is uploaded
                                    drivingLicenseFile.SaveAs(path);

                                    aEmployeeInformation.EmployeeName = aEmployee.EmployeeName;
                                    aEmployeeInformation.EmployeeAddress = aEmployee.EmployeeAddress;
                                    aEmployeeInformation.ContactNumber = aEmployee.ContactNumber;
                                    aEmployeeInformation.EmployeeNid = aEmployee.EmployeeNid;
                                    aEmployeeInformation.EmployeeNidImage = "/Image/" + picNid;
                                    aEmployeeInformation.EmployeeEmail = aEmployee.EmployeeEmail;
                                    aEmployeeInformation.EmployeeImage = aEmployee.EmployeeImage;
                                    aEmployeeInformation.DesignationId = aEmployee.DesignationId;
                                    aEmployeeInformation.DrivingLicenseImage = "/Image/" + picDrivingLicense;
                                    aEmployeeInformation.Salary = aEmployee.Salary;
                                    aEmployeeInformation.RouteId = aEmployee.RouteId;
                                    aEmployeeInformation.EditedBy = SessionManger.LoggedInUser(Session);
                                    aEmployeeInformation.EditedDateTime = DateTime.Now;
                                    unitOfWork.EmployeeInformationRepository.Update(aEmployeeInformation);
                                    unitOfWork.Save();
                                }
                                else
                                {
                                    aEmployeeInformation.EmployeeName = aEmployee.EmployeeName;
                                    aEmployeeInformation.EmployeeAddress = aEmployee.EmployeeAddress;
                                    aEmployeeInformation.ContactNumber = aEmployee.ContactNumber;
                                    aEmployeeInformation.EmployeeNid = aEmployee.EmployeeNid;
                                    aEmployeeInformation.EmployeeNidImage = "/Image/" + picNid;
                                    aEmployeeInformation.EmployeeEmail = aEmployee.EmployeeEmail;
                                    aEmployeeInformation.EmployeeImage = aEmployee.EmployeeImage;
                                    aEmployeeInformation.DesignationId = aEmployee.DesignationId;
                                    aEmployeeInformation.DrivingLicenseImage = aEmployee.DrivingLicenseImage;
                                    aEmployeeInformation.Salary = aEmployee.Salary;
                                    aEmployeeInformation.RouteId = aEmployee.RouteId;
                                    aEmployeeInformation.EditedBy = SessionManger.LoggedInUser(Session);
                                    aEmployeeInformation.EditedDateTime = DateTime.Now;
                                    unitOfWork.EmployeeInformationRepository.Update(aEmployeeInformation);
                                    unitOfWork.Save();
                                }
                            }
                            else
                            {
                                if (drivingLicenseFile != null)
                                {
                                    string picDrivingLicense = System.IO.Path.GetFileName(drivingLicenseFile.FileName);
                                    path = System.IO.Path.Combine(
                                        Server.MapPath("~/Image"), picDrivingLicense);
                                    // file is uploaded
                                    drivingLicenseFile.SaveAs(path);

                                    aEmployeeInformation.EmployeeName = aEmployee.EmployeeName;
                                    aEmployeeInformation.EmployeeAddress = aEmployee.EmployeeAddress;
                                    aEmployeeInformation.ContactNumber = aEmployee.ContactNumber;
                                    aEmployeeInformation.EmployeeNid = aEmployee.EmployeeNid;
                                    aEmployeeInformation.EmployeeNidImage = aEmployee.EmployeeNidImage;
                                    aEmployeeInformation.EmployeeEmail = aEmployee.EmployeeEmail;
                                    aEmployeeInformation.EmployeeImage = aEmployee.EmployeeImage;
                                    aEmployeeInformation.DesignationId = aEmployee.DesignationId;
                                    aEmployeeInformation.DrivingLicenseImage = "/Image/" + picDrivingLicense;
                                    aEmployeeInformation.Salary = aEmployee.Salary;
                                    aEmployeeInformation.RouteId = aEmployee.RouteId;
                                    aEmployeeInformation.EditedBy = SessionManger.LoggedInUser(Session);
                                    aEmployeeInformation.EditedDateTime = DateTime.Now;
                                    unitOfWork.EmployeeInformationRepository.Update(aEmployeeInformation);
                                    unitOfWork.Save();
                                }
                                else
                                {
                                    aEmployeeInformation.EmployeeName = aEmployee.EmployeeName;
                                    aEmployeeInformation.EmployeeAddress = aEmployee.EmployeeAddress;
                                    aEmployeeInformation.ContactNumber = aEmployee.ContactNumber;
                                    aEmployeeInformation.EmployeeNid = aEmployee.EmployeeNid;
                                    aEmployeeInformation.EmployeeNidImage = aEmployee.EmployeeNidImage;
                                    aEmployeeInformation.EmployeeEmail = aEmployee.EmployeeEmail;
                                    aEmployeeInformation.EmployeeImage = aEmployee.EmployeeImage;
                                    aEmployeeInformation.DesignationId = aEmployee.DesignationId;
                                    aEmployeeInformation.DrivingLicenseImage = aEmployee.DrivingLicenseImage;
                                    aEmployeeInformation.Salary = aEmployee.Salary;
                                    aEmployeeInformation.RouteId = aEmployee.RouteId;
                                    aEmployeeInformation.EditedBy = SessionManger.LoggedInUser(Session);
                                    aEmployeeInformation.EditedDateTime = DateTime.Now;
                                    unitOfWork.EmployeeInformationRepository.Update(aEmployeeInformation);
                                    unitOfWork.Save();
                                }
                            }
                        }
                        return Json(new { success = true, successMessage = "Employee Information Update Successfully" });
                    }
                    else
                    {
                        return Json(new { success = false, errorMessage = "The email already exist" }, JsonRequestBehavior.AllowGet);
                    }

                }
                catch (Exception ex)
                {

                    return Json(new { success = false, errorMessage = ex }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, errorMessage = "Model Is not valid" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult ViewAllEmployeeInformationForSpecificBusRoute()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [SessionManger.CheckUserSession]

        public ActionResult ViewAllEmployeeInformationForSpecificBusRoute(int routeId)
        {
            try
            {
                List<DAL.ViewModel.VM_Employee> employeeList = unitOfWork.CustomRepository.sp_EmployeeInformationForSpecificBusRoute(routeId);

                if (employeeList.Any())
                {
                    return Json(new { success = true, result = employeeList }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, errorMessage = "No Employee found." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            

        }

        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GenerateReportForSearchResult(int routeId)
        {
            try
            {
                List<DAL.ViewModel.VM_Employee> employeeList = unitOfWork.CustomRepository.sp_EmployeeInformationForSpecificBusRoute((routeId));
                var newEmployeeList = new List<DAL.ViewModel.VM_Employee>();
                foreach (var employee in employeeList)
                {
                    DAL.ViewModel.VM_Employee newEmployee = new DAL.ViewModel.VM_Employee();
                    newEmployee.EmployeeName = employee.EmployeeName;
                    newEmployee.EmployeeEmail = employee.EmployeeEmail;
                    newEmployee.EmployeeAddress = employee.EmployeeAddress;
                    newEmployee.ContactNumber = employee.ContactNumber;
                    newEmployee.EmployeeNid = employee.EmployeeNid;
                    newEmployee.DesignationName = employee.DesignationName;
                    newEmployee.Salary = employee.Salary;
                    newEmployeeList.Add(newEmployee);

                }
                int workShopId = Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                string workShopName = unitOfWork.WorkShopInformationRepository.GetByID(workShopId).Name;
                string workShopAddress = unitOfWork.WorkShopInformationRepository.GetByID(workShopId).Address;
                string routerName = unitOfWork.RouteRepository.GetByID(routeId).RouteName;

                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reports/EmployeeInformationForSpecificBusRouteReport.rdlc");
                localReport.SetParameters(new ReportParameter("RouterName", routerName.ToString()));
                localReport.SetParameters(new ReportParameter("WorkShopName", workShopName));
                localReport.SetParameters(new ReportParameter("WorkShopAddress", workShopAddress));
                ReportDataSource reportDataSource = new ReportDataSource("EmployeeInformationForSpecificBusRouteDataSet", newEmployeeList);

                localReport.DataSources.Add(reportDataSource);
                string reportType = "pdf";
                string mimeType;
                string encoding;
                string fileNameExtension;
                //The DeviceInfo settings should be changed based on the reportType
                //http://msdn.microsoft.com/en-us/library/ms155397.aspx
                string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>0in</MarginLeft>" +
                "  <MarginRight>0in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";
                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;
                //Render the report
                renderedBytes = localReport.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);
                var path = System.IO.Path.Combine(Server.MapPath("~/pdfReport"));
                var saveAs = string.Format("{0}.pdf", Path.Combine(path, "myfilename"));

                var idx = 0;
                while (System.IO.File.Exists(saveAs))
                {
                    idx++;
                    saveAs = string.Format("{0}.{1}.pdf", Path.Combine(path, "myfilename"), idx);
                }
                Session["report"] = saveAs;
                using (var stream = new FileStream(saveAs, FileMode.Create, FileAccess.Write))
                {
                    stream.Write(renderedBytes, 0, renderedBytes.Length);
                    stream.Close();
                }
                localReport.Dispose();
                return Json(new { success = true, successMessage = "Report generated successfully.", result = employeeList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult ViewPdfReportForSearchResult(int routeId)
        {
            try
            {
                List<DAL.ViewModel.VM_Employee> employeeList = unitOfWork.CustomRepository.sp_EmployeeInformationForSpecificBusRoute((routeId));
                var newEmployeeList = new List<DAL.ViewModel.VM_Employee>();
                foreach (var employee in employeeList)
                {
                    DAL.ViewModel.VM_Employee newEmployee = new DAL.ViewModel.VM_Employee();
                    newEmployee.EmployeeName = employee.EmployeeName;
                    newEmployee.EmployeeEmail = employee.EmployeeEmail;
                    newEmployee.EmployeeAddress = employee.EmployeeAddress;
                    newEmployee.ContactNumber = employee.ContactNumber;
                    newEmployee.EmployeeNid = employee.EmployeeNid;
                    newEmployee.DesignationName = employee.DesignationName;
                    newEmployee.Salary = employee.Salary;
                    newEmployeeList.Add(newEmployee);

                }
                int workShopId = Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString()); 
                string workShopName = unitOfWork.WorkShopInformationRepository.GetByID(workShopId).Name;
                string workShopAddress = unitOfWork.WorkShopInformationRepository.GetByID(workShopId).Address;
                string routerName = unitOfWork.RouteRepository.GetByID(routeId).RouteName;

                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reports/EmployeeInformationForSpecificBusRouteReport.rdlc");
                localReport.SetParameters(new ReportParameter("RouterName", routerName.ToString()));
                localReport.SetParameters(new ReportParameter("WorkShopName", workShopName));
                localReport.SetParameters(new ReportParameter("WorkShopAddress", workShopAddress));
                ReportDataSource reportDataSource = new ReportDataSource("EmployeeInformationForSpecificBusRouteDataSet", newEmployeeList);

                localReport.DataSources.Add(reportDataSource);
                string reportType = "pdf";
                string mimeType;
                string encoding;
                string fileNameExtension;
                //The DeviceInfo settings should be changed based on the reportType
                //http://msdn.microsoft.com/en-us/library/ms155397.aspx
                string deviceInfo =
                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>8.5in</PageWidth>" +
                "  <PageHeight>11in</PageHeight>" +
                "  <MarginTop>0.5in</MarginTop>" +
                "  <MarginLeft>0in</MarginLeft>" +
                "  <MarginRight>0in</MarginRight>" +
                "  <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] renderedBytes;

                //Render the report
                renderedBytes = localReport.Render(
                    reportType,
                    deviceInfo,
                    out mimeType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);
                return File(renderedBytes, mimeType);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
