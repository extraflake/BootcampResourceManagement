using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientSide.Controllers
{
    public class SubDistrictsController : Controller
    {
        ClientSide.Base.Port getPort = new Base.Port();

        public IActionResult Index()
        {
            return View(LoadSubDistrict());
        }

        public JsonResult LoadSubDistrict()
        {
            IEnumerable<Subdistrict> subdistricts = null;
            
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("SubDistricts");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Subdistrict>>();
                readTask.Wait();
                subdistricts = readTask.Result;
            }
            else
            {
                subdistricts = Enumerable.Empty<Subdistrict>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(subdistricts);
        }

        public JsonResult GetById(int id)
        {
            Subdistrict subdistrict = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("SubDistricts/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Subdistrict>();
                readTask.Wait();
                subdistrict = readTask.Result;
            }
            else
            {
                // try to find something
            }
            return Json(subdistrict);
        }

        public JsonResult LoadSubDistrictsByParam(int param)
        {
            IEnumerable<Subdistrict> subdistricts = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("SubDistricts/GetSubDistricts/" + param);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Subdistrict>>();
                readTask.Wait();
                subdistricts = readTask.Result;
            }
            else
            {
                subdistricts = Enumerable.Empty<Subdistrict>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(subdistricts);
        }
    }
}