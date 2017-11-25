using System;
using System.Collections;
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
    public class BusInformationController : Controller
    {

        UnitOfWork unitOfWork =new UnitOfWork();
        // GET: BusInformation
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult AddBusInformation()
        {
            return View();
        }


        [HttpPost]
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult AddBusInformation(VM_BusInformation busInformation)
        {

            if (ModelState.IsValid)
            {
                var RegistrationNoExist =
                   unitOfWork.BusInformationRepository.Get()
                       .Where(a => a.RegistrationNo == busInformation.RegistrationNo).ToList();

                if (!RegistrationNoExist.Any())
                {

                    try
                    {
                        tblBusInformation abusBusInformation = new tblBusInformation();
                        abusBusInformation.RegistrationNo = busInformation.RegistrationNo;
                        abusBusInformation.ModelNo = busInformation.ModelNo;
                        abusBusInformation.ChassisNo = busInformation.ChassisNo;
                        abusBusInformation.RouteId = busInformation.RouteId;
                        abusBusInformation.WorkShopId = Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                        abusBusInformation.CreatedBy = SessionManger.LoggedInUser(Session);
                        abusBusInformation.CreatedDateTime = DateTime.Now;
                        abusBusInformation.EditedBy = null;
                        abusBusInformation.EditedDateTime = null;

                        unitOfWork.BusInformationRepository.Insert(abusBusInformation);
                        unitOfWork.Save();

                        return Json(new { success = true, successMessage = "Bus Information Saved Successfully" },
                            JsonRequestBehavior.AllowGet);

                    }
                    catch (Exception ex)
                    {

                        return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
                    }
  
                }
                else
                {
                    return Json(new { success = false, errorMessage = "Registration No already exist" }, JsonRequestBehavior.AllowGet);
                }
            }
           
            else
            {
                return Json(new { success = false, errorMessage = "Bus Information not Saved" }, JsonRequestBehavior.AllowGet);
            }

        }

        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetAllBusInformation()
        {
            var busInformationList = (from a in unitOfWork.BusInformationRepository.Get()
                                   select new VM_BusInformation()
                                   {
                                       BusInformationId = a.BusInformationId,
                                       RegistrationNo = a.RegistrationNo,
                                       ModelNo = a.ModelNo,
                                       ChassisNo = a.ChassisNo,
                                       RouteId = (int) a.RouteId,
                                       RouteName = a.tblRoute.RouteName
                                   }).ToList();

            return Json(new { success = true, result = busInformationList }, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult ShowAllBusInformation()
        {
            return View();
        }

        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult Delete(int? id)
        {

            try
            {
                var busInformation = unitOfWork.BusInformationRepository.GetByID(id);

                if (busInformation != null)
                {
                    unitOfWork.BusInformationRepository.Delete(busInformation);
                    unitOfWork.Save();

                    return Json(new { success = true, successMessage = "Bus Information successfully Deleted" }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { success = false, errorMessage = "Bus information not save" }, JsonRequestBehavior.AllowGet);
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
        public ActionResult Update(VM_BusInformation busInformation)
        {
            if (ModelState.IsValid)
            {
                tblBusInformation abusBusInformation =
                           unitOfWork.BusInformationRepository.GetByID(busInformation.BusInformationId);
                 
                var RegistrationNoExist =
                   unitOfWork.BusInformationRepository.Get()
                       .Where(a => a.RegistrationNo == busInformation.RegistrationNo && a.RegistrationNo!=abusBusInformation.RegistrationNo).ToList();

                if (!RegistrationNoExist.Any())
                {
                    try
                    {


                        abusBusInformation.RegistrationNo = busInformation.RegistrationNo;
                        abusBusInformation.ModelNo = busInformation.ModelNo;
                        abusBusInformation.ChassisNo = busInformation.ChassisNo;
                        abusBusInformation.RouteId = busInformation.RouteId;
                        abusBusInformation.EditedBy = SessionManger.LoggedInUser(Session);
                        abusBusInformation.EditedDateTime = DateTime.Now;

                        unitOfWork.BusInformationRepository.Update(abusBusInformation);
                        unitOfWork.Save();

                        return Json(new { success = true, successMessage = "Bus Info update successfully." }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {

                        return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, errorMessage = "Registration No already exist" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, errorMessage = "Model is not valid" }, JsonRequestBehavior.AllowGet);
            }
        }
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

        [HttpGet]
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult ViewBusInformationForSpecificBusRegistrationNo()
        {
           return View();
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
        public JsonResult GenerateReportForSearchResult(string RegistrationNo)
        {
            try
            {
                List<DAL.ViewModel.VM_BusInformation> busInfoList = unitOfWork.CustomRepository.sp_BusInfoForSpecificBusRegistrationNo((RegistrationNo));
                var newBusInfoList = new List<DAL.ViewModel.VM_BusInformation>();
                foreach (var busInfo in busInfoList)
                {
                    DAL.ViewModel.VM_BusInformation newBusInfo = new DAL.ViewModel.VM_BusInformation();
                    newBusInfo.ModelNo = busInfo.ModelNo;
                    newBusInfo.ChassisNo = busInfo.ChassisNo;
                    newBusInfo.RouteName = busInfo.RouteName;
                    newBusInfoList.Add(newBusInfo);

                }
                int workShopId = Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                string workShopName = unitOfWork.WorkShopInformationRepository.GetByID(workShopId).Name;
                string workShopAddress = unitOfWork.WorkShopInformationRepository.GetByID(workShopId).Address;


                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reports/BusInfoForSpecificBusRegistrationNoReport.rdlc");
                localReport.SetParameters(new ReportParameter("RegistrationNo", RegistrationNo));
                localReport.SetParameters(new ReportParameter("WorkShopName", workShopName));
                localReport.SetParameters(new ReportParameter("WorkShopAddress", workShopAddress));
                ReportDataSource reportDataSource = new ReportDataSource("BusInfoForSpecificBusRegistrationNoDataSet", newBusInfoList);

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
                "  <MarginBottom>0.1in</MarginBottom>" +
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
                return Json(new { success = true, successMessage = "Report generated successfully.", result = busInfoList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult ViewPdfReportForSearchResult(string RegistrationNo)
        {
            try
            {
                List<DAL.ViewModel.VM_BusInformation> busInfoList = unitOfWork.CustomRepository.sp_BusInfoForSpecificBusRegistrationNo((RegistrationNo));
                var newBusInfoList = new List<DAL.ViewModel.VM_BusInformation>();
                foreach (var busInfo in busInfoList)
                {
                    DAL.ViewModel.VM_BusInformation newBusInfo = new DAL.ViewModel.VM_BusInformation();
                    newBusInfo.ModelNo = busInfo.ModelNo;
                    newBusInfo.ChassisNo = busInfo.ChassisNo;
                    newBusInfo.RouteName = busInfo.RouteName;
                    newBusInfoList.Add(newBusInfo);

                }
                int workShopId = Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                string workShopName = unitOfWork.WorkShopInformationRepository.GetByID(workShopId).Name;
                string workShopAddress = unitOfWork.WorkShopInformationRepository.GetByID(workShopId).Address;


                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reports/BusInfoForSpecificBusRegistrationNoReport.rdlc");
                localReport.SetParameters(new ReportParameter("RegistrationNo", RegistrationNo));
                localReport.SetParameters(new ReportParameter("WorkShopName", workShopName));
                localReport.SetParameters(new ReportParameter("WorkShopAddress", workShopAddress));
                ReportDataSource reportDataSource = new ReportDataSource("BusInfoForSpecificBusRegistrationNoDataSet", newBusInfoList);

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
                "  <MarginBottom>0.1in</MarginBottom>" +
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