using System;
using System.Collections.Generic;
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

namespace ClientSide.Controllers
{
    public class UpdateTokenController : Controller
    {
        private readonly MyContext _db = new MyContext();
        ClientSide.Base.Port getPort = new Base.Port();

        [Route("UpdateToken")]
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult LoadUpdateToken()
        {
            IEnumerable<Employee> Employees = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("UpdateToken");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Employee>>();
                readTask.Wait();
                Employees = readTask.Result;
            }
            else
            {
                Employees = Enumerable.Empty<Employee>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(Employees);
        }
    
        public JsonResult SendUpdateToken(Employee employee)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var myContent = JsonConvert.SerializeObject(employee);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PostAsync("UpdateToken/", byteContent).Result;
            return Json(result);
        }

        public JsonResult SendUpdateTokens(UpdateToken updateToken)
        {
            updateToken.Email = HttpContext.Session.GetString("email");
        
            Account account = new Account();
          
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var myContent = JsonConvert.SerializeObject(updateToken);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PostAsync("UpdateToken/", byteContent).Result;
            return Json(result);
        }

        [HttpGet]
        public IActionResult LoadPage()
        {
            return RedirectToAction("Index");
        }
    }
}