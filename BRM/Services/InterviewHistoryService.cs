using BRM.Services.Interfaces;
using Data.Models;
using Data.Repositories.Interfaces;
using Data.ViewModel;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services
{
    public class InterviewHistoryService : IInterviewHistoryService
    {
        bool status = false;

        private IInterviewHistoryRepository _interviewHistoryRepository;

        public InterviewHistoryService(IInterviewHistoryRepository interviewHistoryRepository)
        {
            _interviewHistoryRepository = interviewHistoryRepository;
        }

        public bool Delete(int id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return status;
            }
            else
            {
                return _interviewHistoryRepository.Delete(id);
            }
        }

        public InterviewHistory Get(int id)
        {
            return _interviewHistoryRepository.Get(id);
        }

        public IEnumerable<InterviewHistoryVM> GetVM()
        {
            return _interviewHistoryRepository.GetVM();
        }

        public IEnumerable<InterviewHistoryVM> GetVMSort(string start, string end)
        {
            return _interviewHistoryRepository.GetVMSort(start, end);
        }

        public InterviewHistoryVM GetVM(int id)
        {
            return _interviewHistoryRepository.GetVM(id);
        }

        public List<SendEmailInterview> Insert(IList<InsertInterviewHistoryVM> insertInterviewHistoryVM)
        {
            return _interviewHistoryRepository.Insert(insertInterviewHistoryVM);
        }

        public bool Update(int id, InsertInterviewHistoryVM interviewHistory)
        {
            if (string.IsNullOrWhiteSpace(interviewHistory.interview_datetime.ToString()) ||
                string.IsNullOrWhiteSpace(interviewHistory.pic.ToString()))
            {
                return status;
            }
            else
            {
                return _interviewHistoryRepository.Update(id, interviewHistory);
            }
        }
    }
}