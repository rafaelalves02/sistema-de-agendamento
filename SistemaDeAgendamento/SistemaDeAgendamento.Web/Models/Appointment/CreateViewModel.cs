using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeAgendamento.Web.Models.Appointment
{
    public class CreateViewModel
    {
        public int EmployeeId { get; set; }

        public CreateClientViewModel? Client { get; set; }

        public AppointmentServiceViewModel? Service { get; set; }

        public List<SelectListItem>? Employees { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Por favor selecione um dia")]
        public DateOnly? SelectedDate { get; set; }

        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Por favor selecione um horario")]
        public TimeSpan? SelectedTime { get; set; }

        public DateTime? StartTime
        {
            get
            {
                if (SelectedDate.HasValue && SelectedTime.HasValue)
                {
                    return SelectedDate.Value.ToDateTime(TimeOnly.FromTimeSpan(SelectedTime.Value));
                }

                return null;
            }
        }

        public IList<DayAvailablityViewModel>? AvailableDays { get; set; }
    }

    public class CreateClientViewModel
    {
        public string? Name { get; set; }

        public string? PhoneNumber { get; set; }
    }

    public class AppointmentServiceViewModel
    {
        public int Id { get; set; }

        public decimal? Price { get; set; }

        public string? Name { get; set; }

        public int? Duration { get; set; }
    }

    public class DayAvailablityViewModel
    {
        public DateTime Date { get; set; }    
        
        public List<TimeSpan> AvailableTimes { get; set; } = new();
    }
}
