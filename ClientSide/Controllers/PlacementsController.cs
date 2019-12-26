using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Data.Context;
using Data.Models;
using Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientSide.Controllers
{
    public class PlacementsController : Controller
    {
        ClientSide.Base.Port getPort = new Base.Port();

        public IActionResult Index()
        {
            var a = HttpContext.Session.GetString("id");
            var b = HttpContext.Session.GetString("name");
            var c = HttpContext.Session.GetString("email");
            var d = HttpContext.Session.GetString("role");
            if (a != null && b != null && c != null && d != null)
            {
                if (d == "Trainer" || d == "Relation Manager")
                {
                    return View(LoadPlacement());
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                HttpContext.Session.Remove("Id");
                HttpContext.Session.Remove("Name");
                HttpContext.Session.Remove("Email");
                HttpContext.Session.Remove("Role");
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Home");
            }
        }

        public JsonResult LoadPlacement()
        {
            IEnumerable<PlacementVM> placement = null;

            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };

            var responseTask = client.GetAsync("Placements");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IEnumerable<PlacementVM>>();
                readTask.Wait();
                placement = readTask.Result;
            }
            else
            {
                placement = Enumerable.Empty<PlacementVM>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(placement); // nilai function di javascript
        }

        public JsonResult GetById(int id)
        {
            PlacementVM placementVM = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Placements/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<PlacementVM>();
                readTask.Wait();
                placementVM = readTask.Result;
            }
            else
            {

            }
            return Json(placementVM);
        }

        public JsonResult LoadCustomer(PlacementParamVM placementParamVM)
        {
            IEnumerable<PlacementVM> placement = null;

            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };

            var myContent = JsonConvert.SerializeObject(placementParamVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = client.PostAsync("Placements/GetPlacementRmandCus/", byteContent);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IEnumerable<PlacementVM>>();
                readTask.Wait();
                placement = readTask.Result;
            }
            else
            {
                placement = Enumerable.Empty<PlacementVM>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(placement); 
        }

        public JsonResult Delete(string id)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var result = client.DeleteAsync("Placements/" + id).Result;
            return Json(result);
        }

        public JsonResult Insert(BeforeInsertPlacementVM beforeInsertPlacementVM)
        {
            //string rest = "404";
            int counter = 0;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };

            for (int i = 0; i < beforeInsertPlacementVM.employee.ToArray().Length; i++)
            {
                object getmaxemp = null;
                MyContext _myContext = new MyContext();
                var emp = beforeInsertPlacementVM.employee[i].ToString();
                var getemp = _myContext.Placements.Where(y => y.employee == emp).ToList();
                if (getemp.Count.Equals(0))
                {
                    InsertPlacementVM ins = new InsertPlacementVM()
                    {
                        employee = beforeInsertPlacementVM.employee[i].ToString(),
                        department = beforeInsertPlacementVM.department,
                        start_date = beforeInsertPlacementVM.start_date,
                        customer = beforeInsertPlacementVM.customer,
                        notes = beforeInsertPlacementVM.notes
                    };

                    var myContent = JsonConvert.SerializeObject(ins);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var result = client.PutAsync("Placements/InsertPlacement", byteContent).Result;
                    var rest = result.StatusCode.ToString();
                    if (rest == "OK" || rest == "200")
                    {
                        counter++;
                    }
                }
                else
                {
                    getmaxemp = getemp.Max(x => x.id);
                    var get = _myContext.Placements.Where(x => x.id.ToString() == getmaxemp.ToString()).ToList();

                    DateTime date = new DateTime(0001, 01, 01);

                    foreach(var n in get)
                    {
                        if(n.finish_date == date)
                        {

                        }
                        else if (beforeInsertPlacementVM.start_date < n.finish_date)
                        {

                        }
                        else
                        {
                            InsertPlacementVM ins = new InsertPlacementVM()
                            {
                                employee = beforeInsertPlacementVM.employee[i].ToString(),
                                department = beforeInsertPlacementVM.department,
                                start_date = beforeInsertPlacementVM.start_date,
                                customer = beforeInsertPlacementVM.customer,
                                notes = beforeInsertPlacementVM.notes
                            };

                            var myContent = JsonConvert.SerializeObject(ins);
                            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                            var byteContent = new ByteArrayContent(buffer);
                            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            var result = client.PutAsync("Placements/InsertPlacement", byteContent).Result;
                            var rest = result.StatusCode.ToString();
                            if (rest == "OK" || rest == "200")
                            {
                                counter++;
                            }
                        }
                    }
                }
            }
            return Json(counter);
        }

        public JsonResult Update(Placement placement)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var myContent = JsonConvert.SerializeObject(placement);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("Placements/" + placement.id, byteContent).Result;
            return Json(result);
        }

        public JsonResult FinishDate(Placement placement)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var myContent = JsonConvert.SerializeObject(placement);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("Placements/Finish/" + placement.id, byteContent).Result;
            return Json(result);
        }
    }
}