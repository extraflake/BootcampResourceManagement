using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Data.Context;
using Data.Models;
using Data.ViewModel;
using Data.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientSide.Controllers
{
    public class InterviewHistoriesController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly MyContext _db = new MyContext();
        ClientSide.Base.Port getPort = new Base.Port();

        public InterviewHistoriesController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [Route("Interviews")]
        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("role");
            if (!string.IsNullOrWhiteSpace(role))
            {
                if (role.Contains("_RM") || role.Contains("SUPER_ADMIN"))
                {
                    return View(LoadInterviewHistory());
                }
            }
            return RedirectToAction("unauthorize", "home", null);
        }

        public JsonResult LoadInterviewHistory()
        {
            IEnumerable<InterviewHistory> interviewHistories = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("InterviewHistories");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<InterviewHistory>>();
                readTask.Wait();
                interviewHistories = readTask.Result;
            }
            else
            {
                interviewHistories = Enumerable.Empty<InterviewHistory>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(interviewHistories);
        }

        public JsonResult LoadInterviewHistoryVM()
        {
            IEnumerable<InterviewHistoryVM> interviewHistoriesVM = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("InterviewHistories");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IEnumerable<InterviewHistoryVM>>();
                readTask.Wait();
                interviewHistoriesVM = readTask.Result;
            }
            else
            {
                interviewHistoriesVM = Enumerable.Empty<InterviewHistoryVM>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(interviewHistoriesVM);
        }

        public JsonResult LoadInterviewHistoryVMSort(string start, string end)
        {
            IEnumerable<InterviewHistoryVM> interviewHistoriesVM = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("InterviewHistories/" + start + "/" + end);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IEnumerable<InterviewHistoryVM>>();
                readTask.Wait();
                interviewHistoriesVM = readTask.Result;
            }
            else
            {
                interviewHistoriesVM = Enumerable.Empty<InterviewHistoryVM>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(interviewHistoriesVM);
        }

        public JsonResult GetById(int id)
        {
            InterviewHistoryVM interviewHistoryVM = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("InterviewHistories/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<InterviewHistoryVM>();
                readTask.Wait();
                interviewHistoryVM = readTask.Result;
            }
            else
            {
                // try to find something
            }
            return Json(interviewHistoryVM);
        }

        public JsonResult Delete(string id)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var result = client.DeleteAsync("InterviewHistories/" + id).Result;
            return Json(result);
        }

        public JsonResult Update(int id, InsertInterviewHistoryVM interviewHistory)
        {
            int resultFinal = 404;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var myContent = JsonConvert.SerializeObject(interviewHistory);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("InterviewHistories/" + interviewHistory.id, byteContent).Result;
            var rest = result.StatusCode.ToString();
            if (rest == "OK" || rest == "200")
            {
                resultFinal = 200;
            }
            return Json(resultFinal);
        }

        public JsonResult Insert(BeforeInsertInterviewHistoryVM beforeInsertInterviewHistoryVM)
        {
            //int resultFinal = 404;
            int counter = 0;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };

            var countemp = beforeInsertInterviewHistoryVM.employee.ToArray().Length;
            IList<InsertInterviewHistoryVM> listInsert = new List<InsertInterviewHistoryVM>();
            for (int i = 0; i < countemp; i++)
            {
                MyContext _myContext = new MyContext();
                var emp = beforeInsertInterviewHistoryVM.employee[i].ToString();
                var cus = beforeInsertInterviewHistoryVM.customer;
                var get = _myContext.InterviewHistories.Where(x => x.employee == emp && x.customer == cus && x.department == beforeInsertInterviewHistoryVM.department && x.pic == beforeInsertInterviewHistoryVM.pic).ToList();
                var getcustomer = beforeInsertInterviewHistoryVM.customer.ToString();
                var getemployee = beforeInsertInterviewHistoryVM.employee.ToString();
                var getcount = get.Count;
                if (getcount <= 0)
                {
                    InsertInterviewHistoryVM insert = new InsertInterviewHistoryVM()
                    {
                        employee = beforeInsertInterviewHistoryVM.employee[i].ToString(),
                        interview_datetime = beforeInsertInterviewHistoryVM.interview_datetime,
                        pic = beforeInsertInterviewHistoryVM.pic,
                        note = beforeInsertInterviewHistoryVM.note,
                        customer = beforeInsertInterviewHistoryVM.customer,
                        department = beforeInsertInterviewHistoryVM.department,
                        create_by = HttpContext.Session.GetString("name"),
                        create_datetime = beforeInsertInterviewHistoryVM.create_datetime,
                        update_by = beforeInsertInterviewHistoryVM.update_by,
                        update_datetime = beforeInsertInterviewHistoryVM.update_datetime
                    };
                    listInsert.Add(insert);
                    //var myContent = JsonConvert.SerializeObject(insert);
                    //var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    //var byteContent = new ByteArrayContent(buffer);
                    //byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    //var result = client.PutAsync("InterviewHistories/InsertInterviewHistory", byteContent).Result;
                    //var rest = result.StatusCode.ToString();
                    //if (rest == "OK" || rest == "200")
                    //{
                    //    counter++;
                    //}
                }
                else
                {
                    foreach (var n in get)
                    {
                        if (n.customer == getcustomer && n.employee == getemployee)
                        {
                            //return Json(404);
                        }
                        else if (n.customer != getcustomer)
                        {
                            InsertInterviewHistoryVM insert = new InsertInterviewHistoryVM()
                            {
                                employee = beforeInsertInterviewHistoryVM.employee[i].ToString(),
                                interview_datetime = Convert.ToDateTime(Convert.ToDateTime(beforeInsertInterviewHistoryVM.interview_datetime).ToLongDateString()),
                                pic = beforeInsertInterviewHistoryVM.pic,
                                note = beforeInsertInterviewHistoryVM.note,
                                customer = beforeInsertInterviewHistoryVM.customer,
                                department = beforeInsertInterviewHistoryVM.department,
                                create_by = beforeInsertInterviewHistoryVM.create_by,
                                create_datetime = beforeInsertInterviewHistoryVM.create_datetime,
                                update_by = beforeInsertInterviewHistoryVM.update_by,
                                update_datetime = beforeInsertInterviewHistoryVM.update_datetime
                            };
                            listInsert.Add(insert);
                            //var myContent = JsonConvert.SerializeObject(insert);
                            //var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                            //var byteContent = new ByteArrayContent(buffer);
                            //byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            //var result = client.PutAsync("InterviewHistories/InsertInterviewHistory", byteContent).Result;
                            //var rest = result.StatusCode.ToString();
                            //if (rest == "OK" || rest == "200")
                            //{
                            //    counter++;
                            //}
                        }
                    }
                }
            }
            var serialize = listInsert.ToArray();
            var myContent = JsonConvert.SerializeObject(serialize);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("InterviewHistories/InsertInterviewHistory", byteContent).Result;
            var rest = result.StatusCode.ToString();
            if (rest == "OK" || rest == "200")
            {
                counter++;
            }
            return Json(counter);
        }
    }
}