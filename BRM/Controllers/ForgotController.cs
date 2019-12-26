using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using BRM.Services.Interfaces;
using Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Office.Interop.Outlook;

namespace BRM.Controllers
{
    [Route("api/forgot-password")]
    [ApiController]
    public class ForgotController : Controller
    {
        MyContext myContext = new MyContext();
        private IEmployeeService _employeeService;
        public ForgotController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        [HttpPost]
        public IActionResult ForgotPassword([FromBody] Data.Models.ForgotPassword forgotPassword)
        {
            string email = forgotPassword.email;
            Dictionary<string, object> result = null;

            //check if an employee has specified email
            //if found then continue next flow
            Data.Models.Employee employee = _employeeService.Get(email);
            if (employee == null)
            {
                result = new Dictionary<string, object>();
                result.Add("message", "No Employee found with specified email");
                return NotFound(result);
            }
            //end of check


            //updating user password statically to "mitrainformatika1"
            bool isPasswordUpdated = _employeeService.ForgotPassword(email);
            if (isPasswordUpdated == false)
            {
                result = new Dictionary<string, object>();
                result.Add("message", "Fail while updating Employee password");
                return BadRequest(result);

            }
            _Application application = new Application();
            MailItem mailItem = (MailItem)application.CreateItem(OlItemType.olMailItem);
            mailItem.To = email;
            mailItem.Subject = "Forgot Password System - " + DateTime.Now.ToLocalTime();
            mailItem.HTMLBody = "<html><center><b>Password Reset Request</b></center><br>Hi " +
                email + ", your account password has been updated to: " + "<b>mitrainformatika1 </b>" +
                " <br>Please try relogin with your new password to make sure everything is fine </html>";
            mailItem.Importance = OlImportance.olImportanceHigh;
            ((_MailItem)mailItem).Send();
            result = new Dictionary<string, object>();
            result.Add("message", "Forgot Password email sent successfully");
            return Ok(result);

        }

        /*protected override void Dispose(bool disposing)
        {
            this.smtpClient.Dispose();
            base.Dispose(disposing);
        }*/


        [HttpGet("{email}")]
        public IActionResult GetEmployee(string email)
        {
            var get = _employeeService.Get(email);
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

    }
}