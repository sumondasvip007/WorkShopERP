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
    public class TotalCostHistoryFromDateToDateForABusRegistrationNoController : Controller
    {
        UnitOfWork unitOfWork=new UnitOfWork();
        // GET: TotalCostHistoryFromDateToDateForABusRegistrationNo

        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult ViewTotalCostHistoryFromDateToDateForABusRegistrationNo()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [SessionManger.CheckUserSession]

        public ActionResult SearchTotalCostHistoryFromDateToDateForABusRegistrationNo(string registrationNo, DateTime fromDate, DateTime toDate)
        {
            try
            {
                List<DAL.ViewModel.VM_CostForBusRegistrationNo> totalCostInfoList = unitOfWork.CustomRepository.sp_totalCostHistoryFromDateToDateForBusRegistrationNoFullFinal(registrationNo, fromDate, toDate);

                double totalAmount = 0;

                if (totalCostInfoList.Any())
                {
                    totalAmount = totalCostInfoList.Select(s => s.Price).Sum();
                    return Json(new { success = true, result = totalCostInfoList, TotalAmount = totalAmount }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, errorMessage = "No Cost found.", TotalAmount = totalAmount }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GenerateReportForSearchResult(string registrationNo, string fromDate, string toDate)
        {
            try
            {
                List<DAL.ViewModel.VM_CostForBusRegistrationNo> totalCostInfoList = unitOfWork.CustomRepository.sp_totalCostHistoryFromDateToDateForBusRegistrationNoFullFinal(registrationNo, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                double totalAmount = 0;
                var newTotalCostInfoList = new List<DAL.ViewModel.VM_CostForBusRegistrationNo>();
                foreach (var totalCostInfo in totalCostInfoList)
                {
                    DAL.ViewModel.VM_CostForBusRegistrationNo newtotalCostInfo = new DAL.ViewModel.VM_CostForBusRegistrationNo();
                    newtotalCostInfo.CompanyName = totalCostInfo.CompanyName;
                    newtotalCostInfo.PartsName = totalCostInfo.PartsName;
                    newtotalCostInfo.Other = totalCostInfo.Other;
                    newtotalCostInfo.Quantity = totalCostInfo.Quantity;
                    newtotalCostInfo.UnitPrice = totalCostInfo.UnitPrice;
                    newtotalCostInfo.Price = totalCostInfo.Price;
                    newtotalCostInfo.StoreName = totalCostInfo.StoreName;
                    newTotalCostInfoList.Add(totalCostInfo);

                }

                totalAmount = totalCostInfoList.Select(s => s.Price).Sum();
                int workShopId = Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                string workShopName = unitOfWork.WorkShopInformationRepository.GetByID(workShopId).Name;
                string workShopAddress = unitOfWork.WorkShopInformationRepository.GetByID(workShopId).Address;



                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reports/TotalCostHistoryFromDateToDateForABusRegistrationNoReport.rdlc");
                localReport.SetParameters(new ReportParameter("RegistrationNo", registrationNo));
                localReport.SetParameters(new ReportParameter("FromDate", fromDate.ToString()));
                localReport.SetParameters(new ReportParameter("ToDate", toDate.ToString()));
                localReport.SetParameters(new ReportParameter("WorkShopName", workShopName));
                localReport.SetParameters(new ReportParameter("WorkShopAddress", workShopAddress));
                ReportDataSource reportDataSource = new ReportDataSource("TotalCostHistoryFromDateToDateForABusRegistrationNoDataSet", newTotalCostInfoList);

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
                return Json(new { success = true, successMessage = "Report generated successfully.", result = totalCostInfoList, TotalAmount = totalAmount }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult ViewPdfReportForSearchResult(string registrationNo, string fromDate, string toDate)
        {
            try
            {
                List<DAL.ViewModel.VM_CostForBusRegistrationNo> totalCostInfoList = unitOfWork.CustomRepository.sp_totalCostHistoryFromDateToDateForBusRegistrationNoFullFinal(registrationNo, Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate));
                var newTotalCostInfoList = new List<DAL.ViewModel.VM_CostForBusRegistrationNo>();
                foreach (var totalCostInfo in totalCostInfoList)
                {
                    DAL.ViewModel.VM_CostForBusRegistrationNo newtotalCostInfo = new DAL.ViewModel.VM_CostForBusRegistrationNo();
                    newtotalCostInfo.CompanyName = totalCostInfo.CompanyName;
                    newtotalCostInfo.PartsName = totalCostInfo.PartsName;
                    newtotalCostInfo.Other = totalCostInfo.Other;
                    newtotalCostInfo.Quantity = totalCostInfo.Quantity;
                    newtotalCostInfo.UnitPrice = totalCostInfo.UnitPrice;
                    newtotalCostInfo.Price = totalCostInfo.Price;
                    newtotalCostInfo.StoreName = totalCostInfo.StoreName;
                    newTotalCostInfoList.Add(totalCostInfo);

                }
                int workShopId = Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                string workShopName = unitOfWork.WorkShopInformationRepository.GetByID(workShopId).Name;
                string workShopAddress = unitOfWork.WorkShopInformationRepository.GetByID(workShopId).Address;



                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reports/TotalCostHistoryFromDateToDateForABusRegistrationNoReport.rdlc");
                localReport.SetParameters(new ReportParameter("RegistrationNo", registrationNo));
                localReport.SetParameters(new ReportParameter("FromDate", fromDate.ToString()));
                localReport.SetParameters(new ReportParameter("ToDate", toDate.ToString()));
                localReport.SetParameters(new ReportParameter("WorkShopName", workShopName));
                localReport.SetParameters(new ReportParameter("WorkShopAddress", workShopAddress));
                ReportDataSource reportDataSource = new ReportDataSource("TotalCostHistoryFromDateToDateForABusRegistrationNoDataSet", newTotalCostInfoList);

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