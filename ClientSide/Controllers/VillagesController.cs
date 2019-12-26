using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientSide.Controllers
{
    public class VillagesController : Controller
    {

        ClientSide.Base.Port getPort = new Base.Port();

        public IActionResult Index()
        {
            return View(LoadVillage());
        }

        public JsonResult LoadVillage()
        {
            IEnumerable<Village> villages = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Villages");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Village>>();
                readTask.Wait();
                villages = readTask.Result;
            }
            else
            {
                villages = Enumerable.Empty<Village>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(villages);
        }

        public JsonResult GetById(int id)
        {
            Village village = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Villages/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Village>();
                readTask.Wait();
                village = readTask.Result;
            }
            else
            {
                // try to find something
            }
            return Json(village);
        }

        public JsonResult LoadVillagesByParam(int param)
        {
            IEnumerable<Village> village = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Villages/GetVillages/" + param);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Village>>();
                readTask.Wait();
                village = readTask.Result;
            }
            else
            {
                village = Enumerable.Empty<Village>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(village);
        }
    }
}