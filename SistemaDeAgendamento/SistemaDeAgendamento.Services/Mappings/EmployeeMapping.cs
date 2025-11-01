using SistemaDeAgendamento.Repositories.Entities;
using SistemaDeAgendamento.Services.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeAgendamento.Services.Mappings
{
    public static class EmployeeMapping
    {
        public static Employee MapToEmployee(this CreateEmployeeRequest request, int userId)
        {
            return new Employee
            {
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                User = new User
                {
                    Id = userId,
                    UserName = request.UserName,
                    Password = request.Password
                }
            };
        }

        public static EmployeeResult MapToEmployeeResult(this Employee employee, IList<Availability>? availabilities)
        {
            return new EmployeeResult
            {
                Id = employee.Id,
                UserId = employee.User!.Id,
                Name = employee.Name,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email,
                UserName = employee.User.UserName,
                Password = employee.User.Password,
                EmployeeAvailability = availabilities?
                    .Select(a => new EmployeeAvailability
                    {
                        WeekDay = (Services.Enums.WeekDay)a.WeekDay,
                        StartTime = a.StartTime,
                        EndTime = a.EndTime,
                        IsActive = a.IsActive
                    })
                    .ToList()
            };
        }

        public static EmployeeResult MapToEmployeeResult(this Employee entity)
        {
            return new EmployeeResult
            {
                Id = entity.Id,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                Name = entity.Name,
                UserName = entity.User!.UserName,
                UserId = entity.User.Id,
                Password = entity.User.Password
            };
        }

        public static Employee MapToEmployee(this EditEmployeeRequest request)
        {
            return new Employee
            {
                Id = request.Id,
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                User = new User
                {
                    Id = request.UserId,
                    UserName = request.UserName,
                    Password = request.Password!
                }
            };
        }
    }
}
