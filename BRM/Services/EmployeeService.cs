using BRM.Services.Interfaces;
using Data.Models;
using Data.Repositories.Interfaces;
using Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRM.Services
{
    public class EmployeeService : IEmployeeService
    {
        bool status = false;

        private IEmployeeRepository _employeeRepository;
        private IAccountRepository _accountRepository;

        public EmployeeService(IEmployeeRepository employeeRepository,IAccountRepository accountRepository)
        {
            _employeeRepository = employeeRepository;
            _accountRepository = accountRepository;
        }

        public bool Delete(string id, EmployeeVM employeeVM)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return status;
            }
            return _employeeRepository.Delete(id, employeeVM);
        }

        public bool ForgotPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return status;
            }
            bool isPasswordSuccessfullyUpdated = _accountRepository.UpdateForgotPassword(email);
            return isPasswordSuccessfullyUpdated;
        }

        public IEnumerable<Employee> Get()
        {
            return _employeeRepository.Get();
        }

        public Employee Get(string email)
        {
            return _employeeRepository.Get(email);
        }

        public EmployeeDisplayVM GetByIdDisplay(string email)
        {
            return _employeeRepository.GetByIdDisplay(email);
        }

        public IEnumerable<EmployeeDisplayVM> GetDisplay()
        {
            return _employeeRepository.GetDisplay();
        }

        public IEnumerable<TrainerVM> GetTrainer(int id)
        {
            return _employeeRepository.GetTrainer(id);
        }

        public bool Insert(EmployeeVM employeeVM)
        {
            if (string.IsNullOrWhiteSpace(employeeVM.LastName) ||
                string.IsNullOrWhiteSpace(employeeVM.FirstName) ||
                string.IsNullOrWhiteSpace(employeeVM.Email) ||
                string.IsNullOrWhiteSpace(employeeVM.Hiring_Location.ToString()) ||
                string.IsNullOrWhiteSpace(employeeVM.Phone))
            {
                return status;
            }
            else
            {
                return _employeeRepository.Insert(employeeVM);
            }
        }

        public bool Update(string email, EmployeeVM employeeVM)
        {
            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(employeeVM.LastName) ||
                string.IsNullOrWhiteSpace(employeeVM.FirstName) ||
                string.IsNullOrWhiteSpace(employeeVM.Email) ||
                string.IsNullOrWhiteSpace(employeeVM.Hiring_Location.ToString()) ||
                string.IsNullOrWhiteSpace(employeeVM.Phone))
            {
                return status;
            }
            else
            {
                return _employeeRepository.Update(email, employeeVM);
            }
        }

        public bool UpdatePrimary(string email, EmployeeVM employeeVM)
        {
            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(employeeVM.LastName) ||
                string.IsNullOrWhiteSpace(employeeVM.FirstName) ||
                string.IsNullOrWhiteSpace(employeeVM.Email) ||
                string.IsNullOrWhiteSpace(employeeVM.Hiring_Location.ToString()) ||
                string.IsNullOrWhiteSpace(employeeVM.Phone))
            {
                return status;
            }
            else
            {
                return _employeeRepository.UpdatePrimary(email, employeeVM);
            }
        }

        public bool UpdateToken(string email, string defaultToken)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return status;
            }
            else if (string.IsNullOrWhiteSpace(defaultToken))
            {
                return status;
            }
            else
            {

            }
            bool isTokenSuccessfullyUpdated = _accountRepository.UpdateToken(email, defaultToken);
            return isTokenSuccessfullyUpdated;
        }
    }
}


