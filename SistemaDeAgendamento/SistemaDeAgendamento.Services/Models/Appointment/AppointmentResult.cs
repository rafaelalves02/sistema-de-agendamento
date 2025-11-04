using SistemaDeAgendamento.Services.Models.Employee;
using SistemaDeAgendamento.Services.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeAgendamento.Services.Models.Appointment
{
    public class AppointmentResult
    {
        public int Id { get; set; }

        public required ClientResult Client { get; set; }

        public required ServiceResult Service { get; set; }

        public required EmployeeResult Employee { get; set; }

        public AppointmentStatus Status { get; set; }

        public required DateTime StartTime { get; set; }

        public required DateTime EndTime { get; set; }

        public required DateTime CreatedAt { get; set; }
    }

    public class ClientResult
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
    }
    public enum AppointmentStatus
    {
        Scheduled,
        Completed,
        Canceled
    }
}
