using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Data.Context;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ClientSide.Controllers
{
    public class ForgotController : Controller
    {
        ClientSide.Base.Port getPort = new Base.Port();

        [Route("ForgotPassword")]
        public IActionResult Forgot()
        {
            return View(LoadForgot());
        }

        public JsonResult LoadForgot()
        {
            IEnumerable<Employee> Employees = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Forgot");
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

        public JsonResult SendForgotPassword(Data.Models.ForgotPassword forgotPassword)
        {
            MyContext myContext = new MyContext();
            var pullEmployee = myContext.Employees.SingleOrDefault(x => x.email.Equals(forgotPassword.email));
            if(pullEmployee != null)
            {
                var pullAccount = myContext.Accounts.SingleOrDefault(x => x.id.Equals(pullEmployee.id));
                if(pullAccount != null)
                {
                    pullAccount.password = Guid.NewGuid().ToString();
                    myContext.Entry(pullAccount).State = EntityState.Modified;
                    myContext.SaveChanges();
                    forgotPassword.password = pullAccount.password;
                }
            }

            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var myContent = JsonConvert.SerializeObject(forgotPassword);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Bootcamp eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJBc3NldCBNYW5hZ2VtZW50IiwiaWF0IjoxNTcxMjEwMzQ0fQ.egQGVL6fHVvPnann4tvJlDR-4N7Pg8J-KC9hhbqa0w90ulWKya2sQUpIVQyqghy4iwBAmQu1fkVopr3eFPk34A");
            var result = client.PostAsync("http://116.254.101.228:8080/usermanagement/brm/forgotpassword", byteContent).Result;
            return Json(result.StatusCode);
        } 
    }
}