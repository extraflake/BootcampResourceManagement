using Data.Models;
using Data.ViewModel;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services
{
    public interface IInterviewHistoryService
    {
        //List<InterviewHistory> Get();
        InterviewHistory Get(int id);
        IEnumerable<InterviewHistoryVM> GetVM();
        IEnumerable<InterviewHistoryVM> GetVMSort(string start, string end);
        InterviewHistoryVM GetVM(int id);
        List<SendEmailInterview> Insert(IList<InsertInterviewHistoryVM> insetInterviewHistoryVM);
        bool Update(int id, InsertInterviewHistoryVM interviewHistory);
        bool Delete(int id);
    }
}