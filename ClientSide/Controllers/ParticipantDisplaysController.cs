using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClientSide.Controllers
{
    public class ParticipantDisplaysController : Controller
    {
        ClientSide.Base.Port getPort = new Base.Port();
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult LoadParticipantDisplay()
        {
            IEnumerable<ParticipantDisplayVM> participantdisplays = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("ParticipantDisplays");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<ParticipantDisplayVM>>();
                readTask.Wait();
                participantdisplays = readTask.Result;
            }
            else
            {
                participantdisplays = Enumerable.Empty<ParticipantDisplayVM>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(participantdisplays);
        }
    }
}