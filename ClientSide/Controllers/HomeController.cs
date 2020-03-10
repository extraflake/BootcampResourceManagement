using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClientSide.Models;
using Microsoft.AspNetCore.Http;
using Data.ViewModels;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ClientSide.Controllers
{
    public class HomeController : Controller
    {
        ClientSide.Base.Port getPort = new Base.Port();

        public IActionResult Index()
        {
            var name = HttpContext.Session.GetString("name");
            var email = HttpContext.Session.GetString("email");
            var role = HttpContext.Session.GetString("role");
            var Email = HttpContext.Session.GetString("Email");
            var Role = HttpContext.Session.GetString("Role");
            if (name != null && email != null && role != null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }

        public JsonResult CheckingSession()
        {
            var name = HttpContext.Session.GetString("name");
            var email = HttpContext.Session.GetString("email");
            var role = HttpContext.Session.GetString("role");
            if (name != null && email != null && role != null)
            {
                return Json(new { available = "active" });
            }
            else
            {
                return Json(null);
            }
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public JsonResult GetCredential(UserCredentialVM user)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var myContent = JsonConvert.SerializeObject(user);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PostAsync("Account", byteContent).Result;
            return Json(result);
        }

        public JsonResult Validate(string email, string password)
        {
            UserCredentialVM userCredential = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Accounts/" + email + "/" + password);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<UserCredentialVM>();
                readTask.Wait();
                userCredential = readTask.Result;
                // set session
                HttpContext.Session.SetString("name", userCredential.Name);
                HttpContext.Session.SetString("email", userCredential.Email);
                HttpContext.Session.SetString("role", userCredential.Role);
            }
            return Json(result);
        }

        public IActionResult Unauthorize()
        {
            return View();
        }
    }
}
