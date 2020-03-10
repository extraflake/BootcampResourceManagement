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
using Newtonsoft.Json.Linq;

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
            var c = HttpContext.Session.GetString("email");
            var d = HttpContext.Session.GetString("role");
            if (c != null && d != null)
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
                BaseAddress = new Uri("https://sakura.mii.co.id:8080/APISAKURAJWT/getuserbyemail?email=" + email)
            };
            client.DefaultRequestHeaders.Add("Authorization", "sakura eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJzYWt1cmEiLCJpYXQiOjE1ODAxOTYxMzJ9.zYvu8-qr48lmTx7_3tZMmVmIBuGPVXmgHbFOwSdTAdYZO9FFQWa7rUeKodtfOMkzfnnjVQSl6f_t54_qvlo7cA");
            var responseTask = client.GetAsync("");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                // var readTask = result.Content.ReadAsAsync<UserCredentialVM>();
                // readTask.Wait();
                // userCredential = readTask.Result;
                JToken stuff1 = JObject.Parse(JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString());
                var get = stuff1.SelectToken("Password").ToString();
                var resultCheck = BCrypt.Net.BCrypt.Verify(password, get);
                if (resultCheck)
                {
                    // set session
                    HttpContext.Session.SetString("email", stuff1.SelectToken("Email").ToString());
                    HttpContext.Session.SetString("name", stuff1.SelectToken("Name").ToString());
                    HttpContext.Session.SetString("role", stuff1.SelectToken("Role").ToString());
                }
            }
            else
            {
                // try to find something
            }
            return Json(result);
        }
    }
}
