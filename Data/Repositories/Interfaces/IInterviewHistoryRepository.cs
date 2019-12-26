using Data.Models;
using Data.ViewModel;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Interfaces
{
    public interface IInterviewHistoryRepository
    {
        List<InterviewHistory> Get();
        InterviewHistory Get(int id);
        IEnumerable<InterviewHistoryVM> GetVM();
        IEnumerable<InterviewHistoryVM> GetVMSort(string start, string end);
        InterviewHistoryVM GetVM(int id);
        bool Insert(InsertInterviewHistoryVM insertInterviewHistoryVM);
        bool Update(int id, InsertInterviewHistoryVM interviewHistory);
        bool Delete(int id);
    }
}
