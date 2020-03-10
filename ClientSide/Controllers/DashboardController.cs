using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Data.Context;
using Data.ViewModels;
using Microsoft.AspNetCore.Http;
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
            var role = HttpContext.Session.GetString("role");
            if (!string.IsNullOrWhiteSpace(role))
            {
                if (role.Contains("_MAN") || role.Contains("SUPER_ADMIN") || role.Contains("_RM"))
                {
                    return View();
                }
            }
            return RedirectToAction("unauthorize", "home", null);
        }

        public JsonResult LoadDistribution(int param)
        {
            var get = myContext.DistributionVM.FromSql($"call sp_retrieve_report_distribution({param})").ToList();
            if (!get.Count().Equals(0))
            {
                jsonDistribution = JsonConvert.SerializeObject(get, Formatting.Indented);
                return Json(jsonDistribution);
            }
            else
            {
                return Json("No Data Found");
            }
        }

        public JsonResult LoadTopUniversity(int param)
        {
            var get = myContext.ReportUniversityTopVMs.FromSql($"call sp_retrieve_report_top_university({10},{param})").ToList();
            if (!get.Count().Equals(0))
            {
                jsonUniversity = JsonConvert.SerializeObject(get, Formatting.Indented);
                return Json(jsonUniversity);
            }
            else
            {
                return Json("No Data Found");
            }
        }

        public JsonResult LoadPlanRealization(int param)
        {
            var get = myContext.PlanRealizationVMs.FromSql($"call sp_retrieve_report_plan_realization({param})").ToList();
            if (!get.Count().Equals(0))
            {
                jsonPlanRealization = JsonConvert.SerializeObject(get, Formatting.Indented);
                return Json(jsonPlanRealization);
            }
            else
            {
                return Json("No Data Found");
            }
        }

        public JsonResult LoadUniversityLocation(int param)
        {
            var get = myContext.reportBootcampQuantityVMs.FromSql($"call sp_retrieve_report_university_location({param})").OrderByDescending(x => x.value).ToList();
            if (!get.Count().Equals(0))
            {
                jsonUniversityLocation = JsonConvert.SerializeObject(get, Formatting.Indented);
                return Json(jsonUniversityLocation);
            }
            else
            {
                return Json("No Data Found");
            }
        }

        public ActionResult ExportDistribution(string year)
        {
            var result = myContext.DistributionReportVM.FromSql($"call sp_retrieve_export_distribution({year})").ToList();
            using (var excel = new ExcelPackage())
            {
                var workSheet = excel.Workbook.Worksheets.Add("Distribution Report");
                workSheet.Cells[1, 1].LoadFromCollection(result, PrintHeaders: true, TableStyle: OfficeOpenXml.Table.TableStyles.Medium6);
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
                return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Distribution Report.xlsx");
            }
        }

        public ActionResult ExportTopUniversity(string year)
        {
            var result = myContext.TopTenReportVM.FromSql($"call sp_retrieve_export_top_university({10},{year})").ToList();
            using (var excel = new ExcelPackage())
            {
                var workSheet = excel.Workbook.Worksheets.Add("Top 10 University Report");
                workSheet.Cells[1, 1].LoadFromCollection(result, PrintHeaders: true, TableStyle: OfficeOpenXml.Table.TableStyles.Medium6);
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
                return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Top 10 University Report.xlsx");
            }
        }

        public ActionResult ExportPlanRealization(string year)
        {
            var result = myContext.PlanRealizationReportVM.FromSql($"call sp_retrieve_export_plan_realization({year})").ToList();
            using (var excel = new ExcelPackage())
            {
                var workSheet = excel.Workbook.Worksheets.Add("Plan & Realization Report");
                workSheet.Cells[1, 1].LoadFromCollection(result, PrintHeaders: true, TableStyle: OfficeOpenXml.Table.TableStyles.Medium6);
                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
                return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Plan & Realization Report.xlsx");
            }
        }

        public ActionResult ExportUniversityLocation(string year)
        {
            var result = myContext.UnivLocationReportVM.FromSql($"call sp_retrieve_export_university_location({year})").ToList();
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