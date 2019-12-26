using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientSide.Controllers
{
    public class DistrictsController : Controller
    {
        ClientSide.Base.Port getPort = new Base.Port();

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
                    return View(LoadDistrict());
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

        public JsonResult LoadDistrict()
        {
            IEnumerable<District> districts = null;

            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Districts");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<District>>();
                readTask.Wait();
                districts = readTask.Result;
            }
            else
            {
                districts = Enumerable.Empty<District>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(districts);
        }

        public JsonResult GetById(int id)
        {
            District district = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Districts/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<District>();
                readTask.Wait();
                district = readTask.Result;
            }
            else
            {
                // try to find something
            }
            return Json(district);
        }

        public JsonResult LoadDistrictsByParam(string param)
        {
            IEnumerable<District> districts = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Districts/GetDistricts/" + param);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<District>>();
                readTask.Wait();
                districts = readTask.Result;
            }
            else
            {
                districts = Enumerable.Empty<District>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(districts);
        }
    }
}