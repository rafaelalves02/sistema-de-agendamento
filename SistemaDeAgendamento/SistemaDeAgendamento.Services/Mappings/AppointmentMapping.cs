using SistemaDeAgendamento.Repositories.Entities;
using SistemaDeAgendamento.Services.Models.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeAgendamento.Services.Mappings
{
    public static class AppointmentMapping
    {
        public static Appointment MapToAppointment(this CreateAppointmentRequest request,int clientId , DateTime endTime)
        {
            return new Appointment
            {
                ServiceId = request.ServiceId,
                EmployeeId = request.EmployeeId,
                StartTime = request.StartTime,
                EndTime = endTime,
                Status = Status.Scheduled,
                CreatedAt = DateTime.Now,
                ClientId = clientId,
            };
        }
    }
}
