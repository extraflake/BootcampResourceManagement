using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Data.Models;
using Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientSide.Controllers
{
    public class AssetsController : Controller
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
                if (d == "Trainer")
                {
                    return View(LoadAssetDisplay());
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

        public JsonResult LoadAsset()
        {
            IEnumerable<Asset> assets = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Assets");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Asset>>();
                readTask.Wait();
                assets = readTask.Result;
            }
            else
            {
                assets = Enumerable.Empty<Asset>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(assets);
        }

        public JsonResult LoadAssetDisplay()
        {
            IEnumerable<AssetDisplayVM> assets = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Assets/GetAssetDisplay");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<AssetDisplayVM>>();
                readTask.Wait();
                assets = readTask.Result;
            }
            else
            {
                assets = Enumerable.Empty<AssetDisplayVM>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(assets);
        }

        public JsonResult GetById(string id)
        {
            Asset asset = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Assets/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Asset>();
                readTask.Wait();
                asset = readTask.Result;
            }
            else
            {
                // try to find something
            }
            return Json(asset);
        }

        public JsonResult GetCount()
        {
            AssetCountVM asset = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Assets/GetCount/");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<AssetCountVM>();
                readTask.Wait();
                asset = readTask.Result;
            }
            else
            {
                // try to find something
            }
            return Json(asset);
        }

        public JsonResult Insert(Asset asset)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var myContent = JsonConvert.SerializeObject(asset);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("Assets/InsertAsset", byteContent).Result;
            return Json(result);
        }

        public JsonResult Update(Asset asset)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var myContent = JsonConvert.SerializeObject(asset);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("Assets/" + asset.id, byteContent).Result;
            return Json(result);
        }

        public JsonResult Delete(string id)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var result = client.DeleteAsync("Assets/" + id).Result;
            return Json(result.StatusCode);
        }
    }
}