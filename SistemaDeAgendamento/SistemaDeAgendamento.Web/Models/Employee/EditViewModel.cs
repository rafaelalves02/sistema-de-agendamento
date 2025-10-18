using SistemaDeAgendamento.Web.Enums;

namespace SistemaDeAgendamento.Web.Models.Employee
{
    public class EditViewModel
    {
        public required int Id { get; set; }
        public required int UserId { get; set; }
        public required string Name { get; set; }

        public required string PhoneNumber { get; set; }

        public string? Email { get; set; }

        public required string UserName { get; set; }

        public required string Password { get; set; }

        public IList<EditEmployeeAvailabilityViewModel>? EmployeeAvailability { get; set; }
    }

    public class EditEmployeeAvailabilityViewModel 
    {
        public required WeekDay WeekDay { get; set; }

        public required TimeSpan StartTime { get; set; }

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
