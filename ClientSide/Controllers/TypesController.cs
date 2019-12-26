using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientSide.Controllers
{
    public class TypesController : Controller
    {
        ClientSide.Base.Port getPort = new Base.Port();

        public IActionResult Index()
        {
            return View(LoadType());
        }

        public JsonResult LoadType()
        {
            IEnumerable<Data.Models.Type> batch = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Types");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Data.Models.Type>>();
                readTask.Wait();
                batch = readTask.Result;
            }
            else
            {
                batch = Enumerable.Empty<Data.Models.Type>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(batch);
        }

        public JsonResult GetById(string id)
        {
            Data.Models.Type type = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Types/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Data.Models.Type>();
                readTask.Wait();
                type = readTask.Result;
            }
            else
            {
                // try to find something
            }
            return Json(type);
        }

        public JsonResult GetCount(string id)
        {
            Data.Models.Type type = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Types/GetCount/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Data.Models.Type>();
                readTask.Wait();
                type = readTask.Result;
            }
            else
            {
                // try to find something
            }
            return Json(type);
        }

        public async Task<JsonResult> InsertOrUpdate(TypeVM typeVM)
        {
            int counter = 0;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var myContent = JsonConvert.SerializeObject(typeVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = await client.PutAsync("Types/", byteContent);
            var rest = result.StatusCode.ToString();
            if (rest == "OK" || rest == "200")
            {
                counter++;
            }
            return Json(counter);
            //using (var httpClient = new HttpClient())
            //{
            //    StringContent content = new StringContent(JsonConvert.SerializeObject(typeVM), Encoding.UTF8, "application/json");

            //    using (var response = await httpClient.PostAsync(getPort.link + "Types/", content))
            //    {
            //        string apiResponse = await response.Content.ReadAsStringAsync();
            //        typeVM = JsonConvert.DeserializeObject<TypeVM>(apiResponse);
            //    }
            //}
            //return Json(typeVM);
        }
    }
}