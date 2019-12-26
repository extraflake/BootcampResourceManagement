using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Data.Models;
using Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientSide.Controllers
{
    public class ParticipantsController : Controller
    {
        ClientSide.Base.Port getPort = new Base.Port();

        public IActionResult Index()
        {
            return View(LoadParticipant());
        }

        public JsonResult LoadParticipant()
        {
            IEnumerable<Participant> participants = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Participants");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Participant>>();
                readTask.Wait();
                participants = readTask.Result;
            }
            else
            {
                participants = Enumerable.Empty<Participant>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(participants);
        }

        public JsonResult GetById(string id)
        {
            Participant participant = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Participants/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Participant>();
                readTask.Wait();
                participant = readTask.Result;
            }
            else
            {
                // try to find something
            }
            return Json(participant);
        }

        public JsonResult Detail(string id)
        {
            IEnumerable<ParticipantVM> participants = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Participants/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<ParticipantVM>>();
                readTask.Wait();
                participants = readTask.Result;
            }
            else
            {
                participants = Enumerable.Empty<ParticipantVM>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(participants);
        }

        public JsonResult Insert(InsertParticipantVM insertParticipantVM)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var myContent = JsonConvert.SerializeObject(insertParticipantVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("Participants/InsertParticipant", byteContent).Result;
            return Json(result);
        }

        public JsonResult Delete(string id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(getPort.link);
            var result = client.DeleteAsync("Participants/" + id).Result;
            return Json(result);
        }
    }
}