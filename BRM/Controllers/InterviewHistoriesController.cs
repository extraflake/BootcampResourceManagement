using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BRM.Services;
using Data.Models;
using Data.ViewModel;
using Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewHistoriesController : ControllerBase
    {
        private IInterviewHistoryService _interviewHistoryService;

        public InterviewHistoriesController(IInterviewHistoryService interviewHistoryService)
        {
            _interviewHistoryService = interviewHistoryService;
        }

        [HttpGet]
        public IActionResult GetInterviewHistories()
        {
            var get = _interviewHistoryService.GetVM();
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpGet("{Id}")]
        public IActionResult GetInterviewHistoriesById(int id)
        {
            var get = _interviewHistoryService.GetVM(id);
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }

        [HttpPut("InsertInterviewHistory")]
        public IActionResult InsertInterviewHistory(InsertInterviewHistoryVM interviewHistory)
        {
            var push = _interviewHistoryService.Insert(interviewHistory);
            if (push)
            {
                return Ok("Insert Success");
            }
            return StatusCode(500);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateInterviewHistory(int id, InsertInterviewHistoryVM interviewHistory)
        {
            var push = _interviewHistoryService.Update(id, interviewHistory);
            if (push)
            {
                return Ok("Insert Success");
            }
            return StatusCode(500);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteInterviewHistory(int id)
        {
            var push = _interviewHistoryService.Delete(id);
            if (push)
            {
                return Ok("Delete Success");
            }
            return StatusCode(500);
        }

        [HttpGet("{start}/{end}")]
        public IActionResult GetInterviewHistoriesSort(string start, string end)
        {
            var get = _interviewHistoryService.GetVMSort(start, end);
            if (get != null)
            {
                return Ok(get);
            }
            return NotFound("No Data Found");
        }
    }
}