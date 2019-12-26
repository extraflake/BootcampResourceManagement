using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Data.Context;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Renci.SshNet;

namespace ClientSide.Controllers
{
    public class DashboardController : Controller
    {
        MyContext myContext = new MyContext();
        string jsonDistribution = "";
        string jsonUniversity = "";
        string jsonPlanRealization = "";
        string jsonUniversityLocation = "";

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult LoadDistribution()
        {
            var get = myContext.DistributionVM.FromSql($"call sp_retrieve_report_distribution({DateTime.Now.Year})").ToList();
            jsonDistribution = JsonConvert.SerializeObject(get, Formatting.Indented);
            return Json(jsonDistribution);
        }

        public JsonResult LoadTopUniversity(int param)
        {
            var get = myContext.ReportUniversityTopVMs.FromSql($"call sp_retrieve_report_top_university({10},{2019})").ToList();
            jsonUniversity = JsonConvert.SerializeObject(get, Formatting.Indented);
            return Json(jsonUniversity);
        }

        public JsonResult LoadPlanRealization(int param)
        {
            var get = myContext.PlanRealizationVMs.FromSql($"call sp_retrieve_report_plan_realization({DateTime.Now.Year})").ToList();
            jsonPlanRealization = JsonConvert.SerializeObject(get, Formatting.Indented);
            return Json(jsonPlanRealization);
        }

        public JsonResult LoadUniversityLocation(int param)
        {
            var get = myContext.reportBootcampQuantityVMs.FromSql($"call sp_retrieve_report_university_location({DateTime.Now.Year})").OrderByDescending(x => x.value).ToList();
            jsonUniversityLocation = JsonConvert.SerializeObject(get, Formatting.Indented);
            return Json(jsonUniversityLocation);
        }

        public ActionResult ExportDistribution()
        {
            var result = myContext.DistributionReportVM.FromSql($"call sp_retrieve_export_distribution({DateTime.Now.Year})").ToList();
            using (var excel = new ExcelPackage())
            {
                var workSheet = excel.Workbook.Worksheets.Add("Distribution Report");
                workSheet.Cells[1, 1].LoadFromCollection(result, PrintHeaders: true, TableStyle: OfficeOpenXml.Table.TableStyles.Medium6);
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
                return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Distribution Report.xlsx");
            }
        }

        public ActionResult ExportTopUniversity()
        {
            var result = myContext.TopTenReportVM.FromSql($"call sp_retrieve_export_top_university({10},{2019})").ToList();
            using (var excel = new ExcelPackage())
            {
                var workSheet = excel.Workbook.Worksheets.Add("Top 10 University Report");
                workSheet.Cells[1, 1].LoadFromCollection(result, PrintHeaders: true, TableStyle: OfficeOpenXml.Table.TableStyles.Medium6);
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
                return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Top 10 University Report.xlsx");
            }
        }

        public ActionResult ExportPlanRealization()
        {
            var result = myContext.PlanRealizationReportVM.FromSql($"call sp_retrieve_export_plan_realization({2019})").ToList();
            using (var excel = new ExcelPackage())
            {
                var workSheet = excel.Workbook.Worksheets.Add("Plan & Realization Report");
                workSheet.Cells[1, 1].LoadFromCollection(result, PrintHeaders: true, TableStyle: OfficeOpenXml.Table.TableStyles.Medium6);
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
                return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Plan & Realization Report.xlsx");
            }
        }

        public ActionResult ExportUniversityLocation()
        {
            var result = myContext.UnivLocationReportVM.FromSql($"call sp_retrieve_export_university_location({DateTime.Now.Year})").ToList();
            using (var excel = new ExcelPackage())
            {
                var workSheet = excel.Workbook.Worksheets.Add("University Location Report");
                workSheet.Cells[1, 1].LoadFromCollection(result, PrintHeaders: true, TableStyle: OfficeOpenXml.Table.TableStyles.Medium6);
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
                return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "University Location Report.xlsx");
            }
        }
    }
}