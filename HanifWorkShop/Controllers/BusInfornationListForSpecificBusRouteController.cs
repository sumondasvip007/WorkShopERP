using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Repository;
using HanifWorkShop.Utility;
using Microsoft.Reporting.WebForms;

namespace HanifWorkShop.Controllers
{
    public class BusInfornationListForSpecificBusRouteController : Controller
    {
        UnitOfWork unitOfWork=new UnitOfWork();
        // GET: BusInfornationListForSpecificBusRoute
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult ViewBusInfornationListForSpecificBusRoute()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [SessionManger.CheckUserSession]

        public ActionResult ViewBusInfornationListForSpecificBusRoute(int routeId)
        {
            try
            {
                List<DAL.ViewModel.VM_BusInformation> busInfoList = unitOfWork.CustomRepository.sp_BusInformationListForABusRoute(routeId);

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
        public ActionResult ViewPdfReportForSearchResult(int routeId)
        {
            try
            {
                List<DAL.ViewModel.VM_BusInformation> busInfoList = unitOfWork.CustomRepository.sp_BusInformationListForABusRoute((routeId));
                var newBusInfoList = new List<DAL.ViewModel.VM_BusInformation>();
                foreach (var busInfo in busInfoList)
                {
                    DAL.ViewModel.VM_BusInformation newBusInfo = new DAL.ViewModel.VM_BusInformation();
                    newBusInfo.RegistrationNo = busInfo.RegistrationNo;
                    newBusInfo.ModelNo = busInfo.ModelNo;
                    newBusInfo.ChassisNo = busInfo.ChassisNo;
                    newBusInfoList.Add(newBusInfo);

                }
                int workShopId = Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                string workShopName = unitOfWork.WorkShopInformationRepository.GetByID(workShopId).Name;
                string workShopAddress = unitOfWork.WorkShopInformationRepository.GetByID(workShopId).Address;
                string routerName = unitOfWork.RouteRepository.GetByID(routeId).RouteName;

                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reports/BusInfornationListForSpecificBusRouteReport.rdlc");
                localReport.SetParameters(new ReportParameter("RouterName", routerName.ToString()));
                localReport.SetParameters(new ReportParameter("WorkShopName", workShopName));
                localReport.SetParameters(new ReportParameter("WorkShopAddress", workShopAddress));
                ReportDataSource reportDataSource = new ReportDataSource("BusInfornationListForSpecificBusRouteDataSet", newBusInfoList);

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

        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GenerateReportForSearchResult(int routeId)
        {
            try
            {
                List<DAL.ViewModel.VM_BusInformation> busInfoList = unitOfWork.CustomRepository.sp_BusInformationListForABusRoute((routeId));
                var newBusInfoList = new List<DAL.ViewModel.VM_BusInformation>();
                foreach (var busInfo in busInfoList)
                {
                    DAL.ViewModel.VM_BusInformation newBusInfo = new DAL.ViewModel.VM_BusInformation();
                    newBusInfo.RegistrationNo = busInfo.RegistrationNo;
                    newBusInfo.ModelNo = busInfo.ModelNo;
                    newBusInfo.ChassisNo = busInfo.ChassisNo;
                    newBusInfoList.Add(newBusInfo);

                }
                int workShopId = Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                string workShopName = unitOfWork.WorkShopInformationRepository.GetByID(workShopId).Name;
                string workShopAddress = unitOfWork.WorkShopInformationRepository.GetByID(workShopId).Address;
                string routerName = unitOfWork.RouteRepository.GetByID(routeId).RouteName;

                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reports/BusInfornationListForSpecificBusRouteReport.rdlc");
                localReport.SetParameters(new ReportParameter("RouterName", routerName.ToString()));
                localReport.SetParameters(new ReportParameter("WorkShopName", workShopName));
                localReport.SetParameters(new ReportParameter("WorkShopAddress", workShopAddress));
                ReportDataSource reportDataSource = new ReportDataSource("BusInfornationListForSpecificBusRouteDataSet", newBusInfoList);

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

    }
}