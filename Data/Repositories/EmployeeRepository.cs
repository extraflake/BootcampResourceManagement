using Data.Context;
using Data.Models;
using Data.Repositories.Interfaces;
using Data.ViewModels;
using System;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Renci.SshNet;

namespace Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private MyContext _myContext;
        bool status = false;
        IEnumerable<Employee> resultList = null;
        IEnumerable<EmployeeDisplayVM> resultListVM = null;
        IEnumerable<TrainerVM> resultListTrainerVM = null;
        Employee result = null;
        EmployeeDisplayVM resultVM = null;

        public EmployeeRepository(MyContext myContext)
        {
            _myContext = myContext;
        }

        public bool Delete(string email, EmployeeVM employeeVM)
        {
            SshClient client = new SshClient("116.254.101.228", 1282, "admin0", "M1i_5erV3r2-:D204");
            client.Connect();
            if (client.IsConnected)
            {
                var portForwarded = new ForwardedPortLocal("127.0.0.1", 3306, "127.0.0.1", 3306);
                client.AddForwardedPort(portForwarded);
                portForwarded.Start();
                var get = Get(email);
                get.is_deleted = true;
                _myContext.Entry(get).State = EntityState.Modified;
                var result = _myContext.SaveChanges();
                if (result > 0)
                {
                    status = true;
                }
            }
            client.Disconnect();
            return status;

        }

        public IEnumerable<Employee> Get()
        {
            resultList = _myContext.Employees.ToList().OrderByDescending(x => x.create_date);
            return resultList;
        }

        public Employee Get(string email)
        {
            result = _myContext.Employees.FirstOrDefault(x => x.email == email);
            return result;
        }

        public EmployeeDisplayVM GetByIdDisplay(string email)
        {
            resultVM = _myContext.EmployeeDisplayVMs.FromSql($"call sp_retrieve_employee_by_email({email})").FirstOrDefault();
            return resultVM;
        }

        public IEnumerable<EmployeeDisplayVM> GetDisplay()
        {
            resultListVM = _myContext.EmployeeDisplayVMs.FromSql($"call sp_retrieve_employee").ToList().OrderByDescending(x => x.create_date);
            return resultListVM;
        }

        public IEnumerable<TrainerVM> GetTrainer(int id)
        {
            resultListTrainerVM = _myContext.TrainerVMs.FromSql($"call sp_retrieve_trainer_available({id})").ToList();
            return resultListTrainerVM;
        }

        public bool isUserExistById(String id)
        {
            var get = _myContext.Employees.FirstOrDefault(x => x.id == id);
            if (get != null)
            {
                status = true;
            }
            return status;
        }

        public bool Insert(EmployeeVM employeeVM)
        {
            employeeVM.Id = getId(5);
            var getsecondlayer = _myContext.Employees.Where(x => x.email == employeeVM.Email || x.phone == employeeVM.Phone).FirstOrDefault();
            if (employeeVM.Id != null)
            {
                if (getsecondlayer != null)
                {

                }
                else
                {
                    var push = new Employee(employeeVM, "1");
                    _myContext.Employees.Add(push);
                    var result = _myContext.SaveChanges();
                    if (result > 0)
                        status = true;
                }
            }
            return status;
        }

        private string getId(int length)
        {
            string id = string.Empty;
            string random = generateRandomID(length);
            while (isUserExistById(random) == true)
            {
                random = generateRandomID(length);
            }
            id = random;
            return id;
        }

        private string generateRandomID(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

        public bool Update(string email, EmployeeVM employeeVM)
        {
            string connStr = "server=mejakerja.mysql.database.azure.com;user id=mejaadmin@mejakerja;password=M3tr0dat@P@s5;port=3306;persistsecurityinfo=True;database=db_brm;allowuservariables=True;Convert Zero Datetime='True'";
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("sp_update_employee", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@Id", employeeVM.Id);
            cmd.Parameters.AddWithValue("@FirstName", employeeVM.FirstName);
            cmd.Parameters.AddWithValue("@LastName", employeeVM.LastName);
            cmd.Parameters.AddWithValue("@HiringLocation", employeeVM.Hiring_Location);
            cmd.Parameters.AddWithValue("@Phone", employeeVM.Phone);
            cmd.Parameters.AddWithValue("@BirthDate", DateTime.Now.ToLocalTime());
            cmd.Parameters.AddWithValue("@Email", email);
            var result = cmd.ExecuteNonQuery();
            conn.Close();
            if (result > 0)
            {
                status = true;
            }
            return status;

        }

        public bool UpdatePrimary(string email, EmployeeVM employeeVM)
        {
            string connStr = "server=mejakerja.mysql.database.azure.com;user id=mejaadmin@mejakerja;password=M3tr0dat@P@s5;port=3306;persistsecurityinfo=True;database=db_brm;allowuservariables=True;Convert Zero Datetime='True'";
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("sp_update_employee", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@Id", employeeVM.Id);
            cmd.Parameters.AddWithValue("@FirstName", employeeVM.FirstName);
            cmd.Parameters.AddWithValue("@LastName", employeeVM.LastName);
            cmd.Parameters.AddWithValue("@HiringLocation", employeeVM.Hiring_Location);
            cmd.Parameters.AddWithValue("@Phone", employeeVM.Phone);
            cmd.Parameters.AddWithValue("@BirthDate", DateTime.Now.ToLocalTime());
            cmd.Parameters.AddWithValue("@Email", email);
            var result = cmd.ExecuteNonQuery();
            conn.Close();
            if (result > 0)
            {
                status = true;
            }
            return status;
        }
    }
}
