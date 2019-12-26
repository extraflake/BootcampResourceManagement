using Data.Context;
using Data.Models;
using Data.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ClientSide.Controllers
{
    public class EmployeeRolesController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly MyContext _db = new MyContext();
        ClientSide.Base.Port getPort = new Base.Port();

        public EmployeeRolesController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [Route("EmployeeRoles")]
        public IActionResult Index()
        {
            var a = HttpContext.Session.GetString("id");
            var b = HttpContext.Session.GetString("name");
            var c = HttpContext.Session.GetString("email");
            var d = HttpContext.Session.GetString("role");
            if (a != null || b != null || c != null || d != null)
            {
                if (d == "Admin" || d == "Super Admin")
                {
                    return View(LoadEmployeeRoles());
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                HttpContext.Session.Remove("Id");
                HttpContext.Session.Remove("Name");
                HttpContext.Session.Remove("Email");
                HttpContext.Session.Remove("Role");
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Logins");
            }
            
        }

        public JsonResult LoadEmployeeRoles()
        {
            IEnumerable<EmployeeRoleVM> employeeRole = null;

            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("EmployeeRoles");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IEnumerable<EmployeeRoleVM>>();
                readTask.Wait();
                employeeRole = readTask.Result;
            }
            else
            {
                employeeRole = Enumerable.Empty<EmployeeRoleVM>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(employeeRole);
        }

        public JsonResult Delete(int id)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var result = client.DeleteAsync("EmployeeRoles/" + id).Result;
            return Json(result);
        }

        public JsonResult InsertOrUpdate(int id, EmployeeRoleVM employeeRoleVM)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var myContent = JsonConvert.SerializeObject(employeeRoleVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (id==0)
            {
                var result = client.PostAsync("EmployeeRoles", byteContent).Result;
                return Json(result);
            }
            else
            {
                var result = client.PutAsync("EmployeeRoles/" + employeeRoleVM.id, byteContent).Result;
                return Json(result);
            }
        }

        public JsonResult Insert(InsertEmployeeRoleVM insertEmployeeRoleVM)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var myContent = JsonConvert.SerializeObject(insertEmployeeRoleVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PostAsync("EmployeeRoles", byteContent).Result;
            return Json(result);
        }

        public JsonResult GetById(int id)
        {
            EmployeeRoleVM employeerole = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("EmployeeRoles/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<EmployeeRoleVM>();
                readTask.Wait();

                employeerole = readTask.Result;
            }
            else
            {
                // error
            }
            return Json(employeerole);
        }
    }
}
