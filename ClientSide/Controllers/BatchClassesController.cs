using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Data.Models;
using Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientSide.Controllers
{
    public class BatchClassesController : Controller
    {
        ClientSide.Base.Port getPort = new Base.Port();

        [Route("Class")]
        public IActionResult Index()
        {
            var a = HttpContext.Session.GetString("id");
            var b = HttpContext.Session.GetString("name");
            var c = HttpContext.Session.GetString("email");
            var d = HttpContext.Session.GetString("role");
            if (a != null || b != null || c != null || d != null)
            {
                if (d == "Trainer")
                {
                    return View(LoadBatchClasses());
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
                return RedirectToAction("Index", "Home");
            }
        }

        public JsonResult LoadBatchClasses()
        {
            IEnumerable<BatchClassDisplayVM> batchClasses = null;
            ClientSide.Base.Port getPort = new Base.Port();
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
                //BaseAddress = new Uri("https://brm20191017012751.azurewebsites.net/api/")
            };
            var responseTask = client.GetAsync("BatchClasses");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<BatchClassDisplayVM>>();
                readTask.Wait();
                batchClasses = readTask.Result;
            }
            else
            {
                batchClasses = Enumerable.Empty<BatchClassDisplayVM>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(batchClasses);
        }

        public JsonResult Insert(BatchClassVM batchClassVM)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(getPort.link);
            var myContent = JsonConvert.SerializeObject(batchClassVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("BatchClasses/InsertBatchClass", byteContent).Result;
            return Json(result);
        }

        public JsonResult Update(BatchClassVM batchClassVM)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(getPort.link);
            var myContent = JsonConvert.SerializeObject(batchClassVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("BatchClasses/" + batchClassVM.id, byteContent).Result;
            return Json(result);
        }

        public JsonResult GetById(string id)
        {
            BatchClass batchClass = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("BatchClasses/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<BatchClass>();
                readTask.Wait();
                batchClass = readTask.Result;
            }
            else
            {
                // try to find something
            }
            return Json(batchClass);
        }

        public JsonResult Delete(string id)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var result = client.DeleteAsync("BatchClasses/" + id).Result;
            return Json(result);
        }
    }
}