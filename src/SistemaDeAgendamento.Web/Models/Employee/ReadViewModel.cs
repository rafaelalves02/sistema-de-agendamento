using SistemaDeAgendamento.Web.Enums;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeAgendamento.Web.Models.Employee
{
    public class ReadViewModel
    {
        public IList<EmployeeViewModel>? Employees { get; set; }

        public required IList<ReadEmployeeAvailabilityViewModel>? EmployeeAvailability { get; set; }

    }

    public class ReadEmployeeAvailabilityViewModel
    {
        public required WeekDay WeekDay { get; set; }

        [Required(ErrorMessage = "Informe o horário de início.")]
        public required TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "Informe o horário de saída.")]
        public required TimeSpan EndTime { get; set; }

        public bool IsActive { get; set; }

        public string WeekDayDisplay => WeekDay switch
        {
            WeekDay.Sunday => "Domingo",
            WeekDay.Monday => "Segunda-feira",
            WeekDay.Tuesday => "Terça-feira",
            WeekDay.Wednesday => "Quarta-feira",
            WeekDay.Thursday => "Quinta-feira",
            WeekDay.Friday => "Sexta-feira",
            WeekDay.Saturday => "Sábado",
            _ => WeekDay.ToString()
        };
    }

    public class EmployeeViewModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public required string Name { get; set; }

        public required string PhoneNumber { get; set; }

        public string? Email { get; set; }

        public required string UserName { get; set; }
    }
}
