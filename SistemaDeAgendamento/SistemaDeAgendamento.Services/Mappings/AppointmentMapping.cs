using SistemaDeAgendamento.Repositories.Entities;
using SistemaDeAgendamento.Services.Models.Appointment;
using SistemaDeAgendamento.Services.Models.Employee;
using SistemaDeAgendamento.Services.Models.Service;
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

        public static AppointmentResult MapToAppointmentResult(this Appointment appointment)
        {
            return new AppointmentResult
            {
                Id = appointment.Id,
                Client = new ClientResult
                {
                    Id = appointment.Client!.Id,
                    Name = appointment.Client!.Name,
                    PhoneNumber = appointment.Client.PhoneNumber
                },
                Service = new ServiceResult
                {
                    Id = appointment.Service!.Id,
                    Name = appointment.Service!.Name,
                    Price = appointment.Service.Price,
                    Duration = appointment.Service.Duration
                },
                Employee = new EmployeeResult
                {
                    Id = appointment.Employee!.Id,
                    Name = appointment.Employee!.Name,
                    PhoneNumber = appointment.Employee.PhoneNumber,
                    Email = appointment.Employee.Email,           
                },
                Status = (AppointmentStatus)appointment.Status,
                StartTime = appointment.StartTime,
                EndTime = appointment.EndTime,
                CreatedAt = appointment.CreatedAt
            };
        }
    }
}
