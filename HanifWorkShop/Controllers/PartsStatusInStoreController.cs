using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.Repository;
using DAL.ViewModel;
using HanifWorkShop.Utility;
using Microsoft.Ajax.Utilities;
using Microsoft.Reporting.WebForms;

namespace HanifWorkShop.Controllers
{
    public class PartsStatusInStoreController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: PartsStatusInStore
        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult ViewPartsStatusInStore()
        {
            return View();
        }

        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetAllStore()
        {
            try
            {
                var storeList = (from a in unitOfWork.StoreRepository.Get()
                                 select new VM_Store()
                                 {
                                     StoreId = a.StoreId,
                                     StoreName = a.StoreName
                                 }).ToList();

                return Json(new { success = true, result = storeList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GetAllPartsByStore(int? id)
        {
            try
            {
                double totalAmount = 0;
                var storeInfo = unitOfWork.StoreRepository.Get().Where(s => s.StoreId == id).FirstOrDefault();

                var partsId = unitOfWork.PartsTransferRepository.Get().Where(a => a.StoreId == storeInfo.StoreId).DistinctBy(a => a.tblPartsInfo.PartsId).ToList();

                var partsList = new List<Vm_PartsTransfetToBusRegistrationNoFromStore>();
                foreach (var aParts in partsId)
                {
                    if (aParts != null)
                    {
                        Vm_PartsTransfetToBusRegistrationNoFromStore newParts = new Vm_PartsTransfetToBusRegistrationNoFromStore();
                        var partsInfo = unitOfWork.PartsInfoRepository.GetByID(aParts.PartsId);

                        if (partsInfo != null)
                        {

                            int partsIn = (int)unitOfWork.PartsTransferRepository.Get().Where(x => x.isIn == true && x.PartsId == partsInfo.PartsId).Select(x => x.Quantity).Sum();
                            int partsOut = (int)unitOfWork.PartsTransferRepository.Get().Where(x => x.isOut == true && x.PartsId == partsInfo.PartsId).Select(x => x.Quantity).Sum();

                            int getProductAvilableQty = partsIn - partsOut;

                           

                            if (getProductAvilableQty > 0)
                            {
                                
                                newParts.PartsId = partsInfo.PartsId;
                                newParts.PartsName = partsInfo.PartsName;
                                newParts.UnitPrice = Convert.ToInt32(partsInfo.BasePrice);
                                newParts.AvailableQuatity = getProductAvilableQty;
                                newParts.Price = newParts.AvailableQuatity*newParts.UnitPrice;
                                totalAmount += newParts.Price;
                                partsList.Add(newParts);
                            }

                          
                        }
                    }
                }
                return Json(new { result = partsList, TotalAmount = totalAmount, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(new { errorMessage = exception.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorize]
        [SessionManger.CheckUserSession]
        public JsonResult GenerateReportForSearchResult(int? id)
        {
            try
            {

                double totalAmount = 0;
                var storeInfo = unitOfWork.StoreRepository.Get().Where(s => s.StoreId == id).FirstOrDefault();

                var partsId = unitOfWork.PartsTransferRepository.Get().Where(a => a.StoreId == storeInfo.StoreId).DistinctBy(a => a.tblPartsInfo.PartsId).ToList();

                var partsList = new List<Vm_PartsTransfetToBusRegistrationNoFromStore>();
                foreach (var aParts in partsId)
                {
                    if (aParts != null)
                    {
                        Vm_PartsTransfetToBusRegistrationNoFromStore newParts = new Vm_PartsTransfetToBusRegistrationNoFromStore();
                        var partsInfo = unitOfWork.PartsInfoRepository.GetByID(aParts.PartsId);

                        if (partsInfo != null)
                        {

                            int partsIn = (int)unitOfWork.PartsTransferRepository.Get().Where(x => x.isIn == true && x.PartsId == partsInfo.PartsId).Select(x => x.Quantity).Sum();
                            int partsOut = (int)unitOfWork.PartsTransferRepository.Get().Where(x => x.isOut == true && x.PartsId == partsInfo.PartsId).Select(x => x.Quantity).Sum();

                            int getProductAvilableQty = partsIn - partsOut;



                            if (getProductAvilableQty > 0)
                            {

                                newParts.PartsId = partsInfo.PartsId;
                                newParts.PartsName = partsInfo.PartsName;
                                newParts.UnitPrice = Convert.ToInt32(partsInfo.BasePrice);
                                newParts.AvailableQuatity = getProductAvilableQty;
                                newParts.Price = newParts.AvailableQuatity * newParts.UnitPrice;
                                totalAmount += newParts.Price;
                                partsList.Add(newParts);
                            }


                        }
                    }
                }

                int workShopId = Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                string workShopName = unitOfWork.WorkShopInformationRepository.GetByID(workShopId).Name;
                string workShopAddress = unitOfWork.WorkShopInformationRepository.GetByID(workShopId).Address;
                string storeName = unitOfWork.StoreRepository.GetByID(id).StoreName;



                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reports/PartsStatusInStoreReport.rdlc");
                localReport.SetParameters(new ReportParameter("StoreName", storeName));
                localReport.SetParameters(new ReportParameter("WorkShopName", workShopName));
                localReport.SetParameters(new ReportParameter("WorkShopAddress", workShopAddress));
                ReportDataSource reportDataSource = new ReportDataSource("PartsStatusInStoreDataSet", partsList);

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
                return Json(new { success = true, successMessage = "Report generated successfully.", result = partsList, TotalAmount = totalAmount }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        [Authorize]
        [SessionManger.CheckUserSession]
        public ActionResult ViewPdfReportForSearchResult(int? id)
        {
            try
            {
               
                var storeInfo = unitOfWork.StoreRepository.Get().Where(s => s.StoreId == id).FirstOrDefault();

                var partsId = unitOfWork.PartsTransferRepository.Get().Where(a => a.StoreId == storeInfo.StoreId).DistinctBy(a => a.tblPartsInfo.PartsId).ToList();

                var partsList = new List<Vm_PartsTransfetToBusRegistrationNoFromStore>();
                foreach (var aParts in partsId)
                {
                    if (aParts != null)
                    {
                        Vm_PartsTransfetToBusRegistrationNoFromStore newParts = new Vm_PartsTransfetToBusRegistrationNoFromStore();
                        var partsInfo = unitOfWork.PartsInfoRepository.GetByID(aParts.PartsId);

                        if (partsInfo != null)
                        {

                            int partsIn = (int)unitOfWork.PartsTransferRepository.Get().Where(x => x.isIn == true && x.PartsId == partsInfo.PartsId).Select(x => x.Quantity).Sum();
                            int partsOut = (int)unitOfWork.PartsTransferRepository.Get().Where(x => x.isOut == true && x.PartsId == partsInfo.PartsId).Select(x => x.Quantity).Sum();

                            int getProductAvilableQty = partsIn - partsOut;



                            if (getProductAvilableQty > 0)
                            {

                                newParts.PartsId = partsInfo.PartsId;
                                newParts.PartsName = partsInfo.PartsName;
                                newParts.UnitPrice = Convert.ToInt32(partsInfo.BasePrice);
                                newParts.AvailableQuatity = getProductAvilableQty;
                                newParts.Price = newParts.AvailableQuatity * newParts.UnitPrice;
                                
                                partsList.Add(newParts);
                            }


                        }
                    }
                }
                int workShopId = Int32.Parse(SessionManger.WorkShopOfLoggedInUser(Session).ToString());
                string workShopName = unitOfWork.WorkShopInformationRepository.GetByID(workShopId).Name;
                string workShopAddress = unitOfWork.WorkShopInformationRepository.GetByID(workShopId).Address;
                string storeName = unitOfWork.StoreRepository.GetByID(id).StoreName;



                LocalReport localReport = new LocalReport();
                localReport.ReportPath = Server.MapPath("~/Reports/PartsStatusInStoreReport.rdlc");
                localReport.SetParameters(new ReportParameter("StoreName", storeName));                
                localReport.SetParameters(new ReportParameter("WorkShopName", workShopName));
                localReport.SetParameters(new ReportParameter("WorkShopAddress", workShopAddress));
                ReportDataSource reportDataSource = new ReportDataSource("PartsStatusInStoreDataSet", partsList);

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