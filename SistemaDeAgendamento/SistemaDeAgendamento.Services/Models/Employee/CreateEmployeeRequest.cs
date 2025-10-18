using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeAgendamento.Services.Models.Employee
{
    public class CreateEmployeeRequest
    {
        public required string Name { get; set; }

        public required string PhoneNumber { get; set; }

        public string? Email { get; set; }

        public required string UserName { get; set; }

        public required string Password { get; set; }
    }
}
