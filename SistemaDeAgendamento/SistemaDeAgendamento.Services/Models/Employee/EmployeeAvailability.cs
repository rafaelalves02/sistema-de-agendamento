using SistemaDeAgendamento.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeAgendamento.Services.Models.Employee
{
    public class EmployeeAvailability
    {
        public int Id { get; set; }
        public required WeekDay WeekDay { get; set; }

        public required TimeSpan StartTime { get; set; }

        public required TimeSpan EndTime { get; set; }

        public bool IsActive { get; set; }

        public string WeekDayDisplay => WeekDay switch
        {
            WeekDay.Monday => "Segunda-feira",
            WeekDay.Tuesday => "Terça-feira",
            WeekDay.Wednesday => "Quarta-feira",
            WeekDay.Thursday => "Quinta-feira",
            WeekDay.Friday => "Sexta-feira",
            WeekDay.Saturday => "Sábado",
            WeekDay.Sunday => "Domingo",
            _ => WeekDay.ToString()
        };
    }
}
