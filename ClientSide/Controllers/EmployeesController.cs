using ClientSide.ViewModels;
using Data.Context;
using Data.Models;
using Data.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientSide.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly MyContext _db = new MyContext();
        ClientSide.Base.Port getPort = new Base.Port();
        int counter = 0;

        public EmployeesController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [Route("Participants")]
        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("role");
            if (!string.IsNullOrWhiteSpace(role))
            {
                if (role.Contains("_MAN") || role.Contains("SUPER_ADMIN"))
                {
                    return View(LoadEmployeeDisplay());
                }
            }
            return RedirectToAction("unauthorize", "home", null);
        }

        public JsonResult LoadEmployee()
        {
            IEnumerable<Employee> employee = null;

            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Employees");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Employee>>();
                readTask.Wait();
                employee = readTask.Result;
            }
            else
            {
                employee = Enumerable.Empty<Employee>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(employee);
        }

        public JsonResult LoadEmployeeDisplay()
        {
            IEnumerable<EmployeeDisplayVM> employee = null;

            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Employees/GetDisplay");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<EmployeeDisplayVM>>();
                readTask.Wait();
                employee = readTask.Result;
            }
            else
            {
                employee = Enumerable.Empty<EmployeeDisplayVM>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(employee);
        }

        public JsonResult LoadEmployeeParticipant()
        {
            IEnumerable<ParticipantDisplayVM> employee = null;

            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Employees/Participant");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<ParticipantDisplayVM>>();
                readTask.Wait();
                employee = readTask.Result;
            }
            else
            {
                employee = Enumerable.Empty<ParticipantDisplayVM>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(employee);
        }

        public JsonResult LoadTrainer(string id)
        {
            IEnumerable<Employee> employee = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Employees/trainer/" + Convert.ToInt16(id));
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<Employee>>();
                readTask.Wait();
                employee = readTask.Result;
            }
            else
            {
                employee = Enumerable.Empty<Employee>();
                ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return Json(employee);
        }

        public JsonResult GetById(string email)
        {
            Employee employee = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Employees/" + email);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<Employee>();
                readTask.Wait();
                employee = readTask.Result;
            }
            else
            {
                // try to find something
            }
            return Json(employee);
        }

        public JsonResult GetByIdDisplay(string email)
        {
            EmployeeDisplayVM employee = null;
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var responseTask = client.GetAsync("Employees/GetByIdDisplay/" + email);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<EmployeeDisplayVM>();
                readTask.Wait();
                employee = readTask.Result;
            }
            else
            {
                // try to find something
            }
            return Json(employee);
        }

        public JsonResult Insert(EmployeeVM employeeVM)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var myContent = JsonConvert.SerializeObject(employeeVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("Employees/InsertEmployee", byteContent).Result;
            if (result.IsSuccessStatusCode)
            {
                counter++;
            }
            return Json(counter);
        }

        public JsonResult Update(EmployeeVM employeeVM)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var myContent = JsonConvert.SerializeObject(employeeVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage result = null;
            if (string.IsNullOrWhiteSpace(employeeVM.Email))
            {
                result = client.PutAsync("Employees/InsertEmployee", byteContent).Result;
                Console.WriteLine(result);
            }
            else
            {
                result = client.PutAsync("Employees/" + employeeVM.Email, byteContent).Result;
                Console.WriteLine(result);
            }

            return Json(result);
        }

        public JsonResult UpdateNIK(EmployeeVM employeeVM)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var myContent = JsonConvert.SerializeObject(employeeVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("Employees/UpdateEmployeeNIK/" + employeeVM.Email, byteContent).Result;
            return Json(result);
        }

        public JsonResult Delete(string id, EmployeeVM employeeVM)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(getPort.link)
            };
            var myContent = JsonConvert.SerializeObject(employeeVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PostAsync("Employees/" + id, byteContent).Result;
            return Json(result);
        }

        public JsonResult Upload(IList<IFormFile> formFile)
        {
            object counterVM = null;
            EmployeeVM customerList = new EmployeeVM();
            foreach(IFormFile source in formFile)
            {
                using (var stream = new MemoryStream())
                {
                    source.CopyToAsync(stream);

                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet excelWorksheet = package.Workbook.Worksheets["Kandidat Bootcamp " + DateTime.Now.Year];
                        var rowCount = excelWorksheet.Dimension.Rows;

                        for (int i = 2; i <= rowCount; i++)
                        {
                            string checklast = excelWorksheet.Cells[i, 3].Value.ToString();
                            string checkfirst = excelWorksheet.Cells[i, 2].Value.ToString();
                            string id = null;
                            if (checklast.Length < 3 && checkfirst.Length < 2)
                            {
                                id = excelWorksheet.Cells[i, 2].Value.ToString() + excelWorksheet.Cells[i, 3].Value.ToString();
                            }
                            else if (checklast.Length < 3)
                            {
                                id = excelWorksheet.Cells[i, 2].Value.ToString().Substring(0, 2) + excelWorksheet.Cells[i, 3].Value.ToString();
                            }
                            else if (checkfirst.Length < 2)
                            {
                                id = excelWorksheet.Cells[i, 2].Value.ToString() + excelWorksheet.Cells[i, 3].Value.ToString().Substring(0, 3);
                            }
                            else
                            {
                                id = excelWorksheet.Cells[i, 2].Value.ToString().Substring(0, 2) + excelWorksheet.Cells[i, 3].Value.ToString().Substring(0, 3);
                            }
                            var firstName = excelWorksheet.Cells[i, 2].Value.ToString();
                            var lastName = excelWorksheet.Cells[i, 3].Value.ToString();
                            var phone = excelWorksheet.Cells[i, 10].Value.ToString();
                            var email = excelWorksheet.Cells[i, 6].Value.ToString();
                            var batch = excelWorksheet.Cells[i, 4].Value.ToString().Substring(9, 2);
                            var onboard = excelWorksheet.Cells[i, 5].Value.ToString();
                            var isExist = _db.Employees.FirstOrDefault(x => x.email == email && x.id == id);
                            var isBatchExist = _db.Batches.FirstOrDefault(x => x.Id == batch);
                            if (isExist == null)
                            {
                                if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(phone)
                                    || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(batch) ||
                                    string.IsNullOrWhiteSpace(onboard))
                                {
                                    break;
                                }
                                else
                                {
                                    string validatePhone = "0";
                                    string phoneValidate = phone;
                                    if (phoneValidate.ToString().Substring(0, 1) == "0")
                                    {
                                        validatePhone = phone.Remove(0, 1);
                                    }
                                    else if (phoneValidate.ToString().Substring(0, 1) == "1" ||
                                      phoneValidate.ToString().Substring(0, 1) == "2" ||
                                      phoneValidate.ToString().Substring(0, 1) == "3" ||
                                      phoneValidate.ToString().Substring(0, 1) == "4" ||
                                      phoneValidate.ToString().Substring(0, 1) == "5" ||
                                      phoneValidate.ToString().Substring(0, 1) == "6" ||
                                      phoneValidate.ToString().Substring(0, 1) == "7" ||
                                      phoneValidate.ToString().Substring(0, 1) == "9")
                                    {
                                        validatePhone = String.Concat("8", phoneValidate.Remove(0, 1));
                                    }
                                    customerList.Id = id;
                                    customerList.FirstName = firstName;
                                    customerList.LastName = lastName;
                                    customerList.Phone = validatePhone;
                                    customerList.Email = email;
                                    customerList.Hiring_Location = 1;
                                    customerList.Religion = 1;
                                    customerList.Village = 1;
                                    var result = Insert(customerList);
                                    counterVM = result.Value;
                                }
                                if (isBatchExist == null)
                                {
                                    var map = new BatchVM
                                    {
                                        StartDate = Convert.ToDateTime(onboard),
                                        Id = batch
                                    };
                                    BatchesController batchesController = new BatchesController();
                                    batchesController.Insert(map);
                                }
                            }
                        }
                    }
                }
            }
            return Json(counterVM);
        }
    }
}
