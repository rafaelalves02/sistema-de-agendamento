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
        public static IList<Availability> MapToAvailability(IList<EmployeeAvailability> request, int employeeId)
        {
            if (request == null)
                return new List<Availability>();

            return request.Select(ea => new Availability
            {
                EmployeeId = employeeId,
                WeekDay = (WeekDay)ea.WeekDay,
                StartTime = ea.StartTime,
                EndTime = ea.EndTime,
                IsActive = ea.IsActive
            }).ToList();
        }
    }
}
