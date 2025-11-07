using SistemaDeAgendamento.Repositories.Entities;
using SistemaDeAgendamento.Services.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeAgendamento.Services.Mappings
{
    public static class AvailabilityMapping
    {
        public static Availability MapToAvailability(this EmployeeAvailability availability, int employeeId)
        {
            return new Availability
            {
                Id = availability.Id,
                EmployeeId = employeeId,
                WeekDay = (Repositories.Entities.WeekDay)availability.WeekDay,
                StartTime = availability.StartTime,
                EndTime = availability.EndTime,
                IsActive = availability.IsActive
            };
        }
    }
}
