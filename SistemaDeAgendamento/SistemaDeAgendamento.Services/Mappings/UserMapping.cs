using SistemaDeAgendamento.Repositories.Entities;
using SistemaDeAgendamento.Services.Enums;
using SistemaDeAgendamento.Services.Models.Employee;
using SistemaDeAgendamento.Services.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeAgendamento.Services.Mappings
{
    public static class UserMapping
    {
        public static UserResult MapToUserResult(this User user)
        {
            return new UserResult
            {
                Id = user.Id,
                UserName = user.UserName,
                Role = (Role)user.RoleId
            };
        }

        //employee

        public static User MapToUser(this CreateEmployeeRequest request)
        {
            return new User
            {
                UserName = request.UserName,
                Password = request.Password,
                RoleId = (int)Role.Employee
            };
        }

        public static User MapToUser(this EditEmployeeRequest request)
        {
            return new User
            {
                Id = request.UserId,
                UserName = request.UserName,
                Password = request.Password!
            };
        }
    }
}
