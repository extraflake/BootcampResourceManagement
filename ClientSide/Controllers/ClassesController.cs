using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientSide.Controllers
{
    public class ClassesController : Controller
    {
        ClientSide.Base.Port getPort = new Base.Port();

        public IActionResult Index()
        {
            return View(LoadClass());
        }

        public JsonResult LoadClass()
        {
            IEnumerable<Class> classes = null;

            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Classes");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Class>>();
                readTask.Wait();
                classes = readTask.Result;
            }
            else
            {
                classes = Enumerable.Empty<Class>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(classes);
        }

        public JsonResult GetById(int id)
        {
            Class clas = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Classes/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Class>();
                readTask.Wait();
                clas = readTask.Result;
            }
            else
            {
                // try to find something
            }
            return Json(clas);
        }
    }
}