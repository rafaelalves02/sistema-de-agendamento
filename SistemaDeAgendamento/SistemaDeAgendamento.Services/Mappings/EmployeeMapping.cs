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
