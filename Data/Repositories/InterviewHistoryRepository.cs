using Data.Context;
using Data.Models;
using Data.Repositories.Interfaces;
using Data.ViewModel;
using Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class InterviewHistoryRepository : IInterviewHistoryRepository
    {
        MyContext myContext = new MyContext();
        List<InterviewHistory> resultList = null;
        IEnumerable<InterviewHistoryVM> resultListVM = null;
        InterviewHistory result = null;
        InterviewHistoryVM resultVM = null;
        bool status = false;

        public List<InterviewHistory> Get()
        {
            resultList = myContext.InterviewHistories.ToList();
            return resultList;
        }

        public InterviewHistory Get(int id)
        {
            result = myContext.InterviewHistories.FirstOrDefault(x => x.id == id);
            return result;
        }

        public List<SendEmailInterview> Insert(IList<InsertInterviewHistoryVM> insertInterviewHistoryVM)
        {
            List<SendEmailInterview> SendEmailInterview = new List<SendEmailInterview>();
            foreach(var item in insertInterviewHistoryVM)
            {
                var checkavailable = myContext.InterviewHistories.Where(x => x.employee == item.employee && x.customer == item.customer && x.department == item.department && x.pic == item.pic).ToList();
                if (checkavailable.Count().Equals(0))
                {
                    var interviewHistory = new InterviewHistory()
                    {
                        employee = item.employee,
                        interview_datetime = item.interview_datetime,
                        pic = item.pic,
                        note = item.note,
                        customer = item.customer,
                        department = item.department,
                        created_by = item.create_by,
                        create_datetime = item.create_datetime,
                        updated_by = item.update_by,
                        update_datetime = item.update_datetime
                    };
                    myContext.InterviewHistories.Add(interviewHistory);
                    var result = myContext.SaveChanges();
                    if (result > 0)
                    {
                        var getUser = myContext.Employees.Where(x => x.id.Equals(item.nik) && x.email.Equals(item.create_by)).SingleOrDefault();
                        SendEmailInterview push = new SendEmailInterview(interviewHistory);
                        push.nik = item.nik;
                        push.email = getUser.email;
                        SendEmailInterview.Add(push);
                    }
                }
            }
            return SendEmailInterview;
        }

        public bool Update(int id, InsertInterviewHistoryVM interviewHistory)
        {
            var get = Get(id);
            if (get.employee == interviewHistory.employee && get.customer == interviewHistory.customer)
            {
                get.pic = interviewHistory.pic;
                get.department = interviewHistory.department;
                get.note = interviewHistory.note;
                get.interview_datetime = interviewHistory.interview_datetime;
                myContext.Entry(get).State = EntityState.Modified;
            }
            else
            {
                get.Update(interviewHistory.id, interviewHistory.interview_datetime, interviewHistory.pic, interviewHistory.note, interviewHistory.department, interviewHistory.create_by, interviewHistory.create_datetime, interviewHistory.update_by, interviewHistory.update_datetime, interviewHistory.customer, interviewHistory.employee);
                myContext.Entry(get).State = EntityState.Modified;
            }
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                status = true;
            }
            return status;
        }

        public bool Delete(int id)
        {
            var get = Get(id);
            myContext.InterviewHistories.Remove(get);
            var result = myContext.SaveChanges();
            if (result > 0)
            {
                status = true;
            }
            return status;
        }

        public IEnumerable<InterviewHistoryVM> GetVM()
        {
            resultListVM = myContext.InterviewHistoryVMs.FromSql($"call sp_retrieve_interview_employee_customer").ToList().OrderByDescending(x => x.interview_datetime);
            return resultListVM;
        }

        public IEnumerable<InterviewHistoryVM> GetVMSort(string start, string end)
        {
            resultListVM = myContext.InterviewHistoryVMs.FromSql($"call sp_retrieve_interview_employee_customer_sort({start},{end})").ToList();
            return resultListVM;
        }

        public InterviewHistoryVM GetVM(int id)
        {
            resultVM = myContext.InterviewHistoryVMs.FromSql($"call sp_retrieve_interview_employee_customer").SingleOrDefault(x => x.id == id);
            return resultVM;
        }
    }
}
