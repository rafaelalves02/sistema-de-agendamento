using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeAgendamento.Services.Models.Appointment
{
    public class CreateAppointmentRequest
    {
        public required int ServiceId { get; set; }

        public required int EmployeeId { get; set; }

        public required DateTime StartTime { get; set; }

        public required string ClientName { get; set; }

        public required string ClientPhoneNumber { get; set; }
    }

}

