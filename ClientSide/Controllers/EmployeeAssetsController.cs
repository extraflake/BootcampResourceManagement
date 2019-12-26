using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Data.Models;
using Data.ViewModel;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientSide.Controllers
{
    public class EmployeeAssetsController : Controller
    {
        ClientSide.Base.Port getPort = new Base.Port();

        [Route("HistoryAssets")]
        public IActionResult Index()
        {
            return View(LoadEmployeeAsset());
        }

        public JsonResult LoadEmployeeAsset()
        {
            IEnumerable<EmployeeAssetVM> employeeAssets = null;
            
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)

            };
            var responseTask = client.GetAsync("EmployeeAssets");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<EmployeeAssetVM>>();
                readTask.Wait();
                employeeAssets = readTask.Result;
            }
            else
            {
                employeeAssets = Enumerable.Empty<EmployeeAssetVM>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(employeeAssets);
        }

        public JsonResult GetById(string id)
        {
            EmployeeAsset employeeAsset = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("EmployeeAssets/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<EmployeeAsset>();
                readTask.Wait();
                employeeAsset = readTask.Result;
            }
            else
            {
                // try to find something
            }
            return Json(employeeAsset);
        }

        public JsonResult Insert(InsertEmployeeAssetVM insertEmployeeAssetVM)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var myContent = JsonConvert.SerializeObject(insertEmployeeAssetVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PostAsync("EmployeeAssets", byteContent).Result;
            return Json(result);
        }

        public JsonResult Update(EmployeeAsset employeeAsset)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var myContent = JsonConvert.SerializeObject(employeeAsset);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("EmployeeAssets/" + employeeAsset.id, byteContent).Result;
            return Json(result);
        }

        public JsonResult Delete(string id)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var result = client.DeleteAsync("EmployeeAssets/" + id).Result;
            return Json(result);
        }
    }
}