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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Renci.SshNet;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClientSide.Controllers
{
    public class CustomersController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly MyContext _db = new MyContext();
        ClientSide.Base.Port getPort = new Base.Port();
        List<CustomerFilterVM> resultList = null;

        public CustomersController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View(LoadCustomer());
        }

        public JsonResult LoadCustomer()
        {
            IEnumerable<CustomerVM> customers = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Customers");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IEnumerable<CustomerVM>>();
                readTask.Wait();
                customers = readTask.Result;
            }
            else
            {
                customers = Enumerable.Empty<CustomerVM>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(customers);
        }

        public JsonResult GetById(string id)
        {
            CustomerVM customerVM = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Customers/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<CustomerVM>();
                readTask.Wait();
                customerVM = readTask.Result;
            }
            else
            {
                // try to find something
            }
            return Json(customerVM);
        }

        public JsonResult Delete(string id)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var result = client.DeleteAsync("Customers/" + id).Result;
            return Json(result);
        }

        public JsonResult Update(int id, Customer customer)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var myContent = JsonConvert.SerializeObject(customer);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("Customers/" + customer.id, byteContent).Result;
            return Json(result);
        }

        public JsonResult Insert(Customer customer)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var myContent = JsonConvert.SerializeObject(customer);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PostAsync("Customers/", byteContent).Result;
            return Json(result);
        }

        //add filter controller by putra
        public List<CustomerFilterVM> LoadRelationManager()
        {
            MyContext _myContext = new MyContext();
            resultList = _myContext.CustomerFilterVMs.FromSql($"call sp_retrieve_rm").ToList();
            return resultList;
        }
    }
}

