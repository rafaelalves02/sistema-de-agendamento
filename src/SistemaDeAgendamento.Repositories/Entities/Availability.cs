using System;
using System.Collections.Generic;
using System.Linq;


namespace SistemaDeAgendamento.Repositories.Entities
{
    public class Availability
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public WeekDay WeekDay { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public bool IsActive { get; set; }
    }

    public enum WeekDay
    {
        monday,
        tuesday,
        wednesday,
        thursday,
        friday,
        saturday,
        sunday
    }
}
