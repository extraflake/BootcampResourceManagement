using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Data.Context;
using Data.Models;
using Data.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClientSide.Controllers
{
    public class LoginsController : Controller
    {
        ClientSide.Base.Port getPort = new Base.Port();

        // GET: /<controller>/
        [Route("Login")]
        public IActionResult Index()
        {
            var a = HttpContext.Session.GetString("id");
            var b = HttpContext.Session.GetString("name");
            var c = HttpContext.Session.GetString("email");
            var d = HttpContext.Session.GetString("role");
            if (a != null && b != null && c != null && d != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        
        public ActionResult Dashboard(string id, string name, string mail)
        {
            //var a = HttpContext.Session.GetString("id");
            //var b = HttpContext.Session.GetString("name");
            //var c = HttpContext.Session.GetString("email");
            //var d = HttpContext.Session.GetString("role");
            //if (a != null && b != null && c != null && d != null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
                return View("Dashboard");
            //}
        }

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Id");
            HttpContext.Session.Remove("Name");
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("Role");
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
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
                HttpContext.Session.SetString("id", userCredential.Id);
                HttpContext.Session.SetString("name", userCredential.Name);
                HttpContext.Session.SetString("email", userCredential.Email);
                HttpContext.Session.SetString("role", userCredential.Role);
            }
            else
            {
                // try to find something
            }
            return Json(result);
        }
    }
}
