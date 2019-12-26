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
    public class RolesController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly MyContext _db = new MyContext();
        ClientSide.Base.Port getPort = new Base.Port();

        public RolesController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [Route("Roles")]
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
                    return View(LoadRoles());
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

        public JsonResult LoadRoles()
        {
            IEnumerable<Role> roles = null;

            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Roles");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Role>>();
                readTask.Wait();
                roles = readTask.Result;
            }
            else
            {
                roles = Enumerable.Empty<Role>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(roles);
        }

        public JsonResult InsertOrUpdate(int id,Role role)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var myContent = JsonConvert.SerializeObject(role);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (id==0)
            {
                var result = client.PostAsync("Roles", byteContent).Result;
                return Json(result);
            }
            else
            {
                var result = client.PutAsync("Roles/" + id, byteContent).Result;
                return Json(result);
            }
        }

        public JsonResult Update(int id, Role role)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var myContent = JsonConvert.SerializeObject(role);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (string.IsNullOrWhiteSpace(role.id.ToString()))
            {
                var result = client.PutAsync("Roles/" + id, byteContent).Result;
                return Json(result);
            }
            else
            {

                var result = client.PostAsync("Roles", byteContent).Result;
                return Json(result);
            }
        }

        public JsonResult GetById(int id)
        {
            Role role = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Roles/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Role>();
                readTask.Wait();
                role = readTask.Result;
            }
            else
            {
                // try to find something
            }
            return Json(role);
        }

        public JsonResult Delete(int id, Role role)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var result = client.DeleteAsync("Roles/" + id).Result;
            return Json(result);
        }
    }
}
