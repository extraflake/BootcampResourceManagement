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


    public class ChangePasswordController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly MyContext _db = new MyContext();
        ClientSide.Base.Port getPort = new Base.Port();

        public ChangePasswordController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [Route("ChangePassword/")]
        public IActionResult Index()
        {
            return View(LoadChangePassword());
        }
        public JsonResult LoadChangePassword()
        {
            IEnumerable<Account> accountClasses = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("ChangePassword");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Account>>();
                readTask.Wait();
                accountClasses = readTask.Result;
            }
            else
            {
                accountClasses = Enumerable.Empty<Account>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(accountClasses);
        }

        public JsonResult Validate(string token)
        {
            Account account = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Account/" + token);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Account>();
                readTask.Wait();
                account = readTask.Result;
                // set session
                HttpContext.Session.SetString("token", account.token);
            }
            else
            {
                // try to find something
            }
            return Json(result);
        }

        public JsonResult GetByToken(string token)
        {
            Account account = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Account/" + token);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Account>();
                readTask.Wait();
                account = readTask.Result;
            }
            else
            {
                // try to find something
            }
            return Json(account);
        }
        
        [HttpPut("/UpdatePassword")]
        public JsonResult UpdatePassword(ChangePasswordVM changePasswordVM)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var myContent = JsonConvert.SerializeObject(changePasswordVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("Accounts/UpdateChangePassword/" + changePasswordVM.token, byteContent).Result;
            return Json(result.StatusCode);
        }

        [HttpGet("/GetTokenByEmail")]
        public JsonResult GetTokenByEmail()
        {
            string email = HttpContext.Session.GetString("email");
            GetTokenByEmailVM getTokenByEmail = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("ChangePasswords/GetToken/" + email);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<GetTokenByEmailVM>();
                readTask.Wait();
                getTokenByEmail = readTask.Result;
            }
            else
            {
                // try to find something
            }
            return Json(getTokenByEmail);
        }
    }
}