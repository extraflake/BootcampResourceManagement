    using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientSide.Controllers
{
    public class ProvincesController : Controller
    {
        ClientSide.Base.Port getPort = new Base.Port();

        public IActionResult Index()
        {
            return View(LoadProvince());
        }

        public JsonResult LoadProvince()
        {
            IEnumerable<Province> provinces = null;
            
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Provinces");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Province>>();
                readTask.Wait();
                provinces = readTask.Result;
            }
            else
            {
                provinces = Enumerable.Empty<Province>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(provinces);
        }

        public JsonResult GetById(int id)
        {
            Province province = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Provinces/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Province>();
                readTask.Wait();
                province = readTask.Result;
            }
            else
            {
                // try to find something
            }
            return Json(province);
        }
    }
}