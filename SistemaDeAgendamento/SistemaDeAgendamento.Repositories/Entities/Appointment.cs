using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeAgendamento.Repositories.Entities
{
    public class Appointment
    {
        public required int Id { get; set; }

        public required int ClientId { get; set; }

        public required int ServiceId { get; set; }

        public required int EmployeeId { get; set; }

        public Status Status { get; set; }

        public required DateTime StartTime { get; set; }

        public required DateTime EndTime { get; set; }

        public required DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public enum Status
    {
        Scheduled,
        Completed,
        Canceled
    }
}
