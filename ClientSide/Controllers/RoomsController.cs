using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClientSide.Controllers
{
    public class RoomsController : Controller
    {
        ClientSide.Base.Port getPort = new Base.Port();

        public IActionResult Index()
        {
            return View(LoadRoom());
        }

        public JsonResult LoadRoom()
        {
            IEnumerable<Room> rooms = null;
            
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Rooms");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Room>>();
                readTask.Wait();
                rooms = readTask.Result;
            }
            else
            {
                rooms = Enumerable.Empty<Room>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(rooms);
        }
    }
}