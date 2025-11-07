using SistemaDeAgendamento.Web.Enums;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeAgendamento.Web.Models.Employee
{
    public class CreateViewModel
    {
        public required string Name { get; set; }

        public required string PhoneNumber { get; set; }

        public string? Email { get; set; }

        public required string UserName { get; set; }

        public required string Password { get; set; }

        public required IList<EmployeeAvailabilityViewModel> EmployeeAvailability { get; set; }

    }

    public class EmployeeAvailabilityViewModel
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
}
