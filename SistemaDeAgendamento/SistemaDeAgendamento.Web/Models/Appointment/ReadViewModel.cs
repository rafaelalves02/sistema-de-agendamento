
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaDeAgendamento.Web.Models.Employee;
using System.ComponentModel.DataAnnotations;

namespace SistemaDeAgendamento.Web.Models.Appointment
{
    public class ReadViewModel
    {
        public IList<AppointmentViewModel>? Appointments { get; set; }

        public string? ClientPhoneNumber { get; set; }

        public DateTime? Date { get; set; }
        public int? ServiceId { get; set; }
        public int? EmployeeId { get; set; }
        public AppointmentStatusViewModel? Status { get; set; }

        public IList<SelectListItem>? Services { get; set; }
        public IList<SelectListItem>? Employees { get; set; }

    }

    public class AppointmentViewModel
    {
        public int Id { get; set; }

        public required ClientViewModel Client { get; set; }

        public required ServiceViewModel Service { get; set; }

        public required AppointmentEmployeeViewModel Employee { get; set; }

        public AppointmentStatusViewModel Status { get; set; }

        public required DateTime StartTime { get; set; }

        public required DateTime EndTime { get; set; }

        public required DateTime CreatedAt { get; set; }
    }

    public class ClientViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrWhiteSpace(PhoneNumber))
            {
                var digits = new string(PhoneNumber.Where(char.IsDigit).ToArray());
                if (digits.Length < 11)
                {
                    yield return new ValidationResult(
                        "Por favor informe um telefone com pelo menos 11 dígitos.",
                        new[] { nameof(PhoneNumber) }
                    );
                }
            }
        }
    }

    public class ServiceViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required int Duration { get; set; }
    }

    public class AppointmentEmployeeViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public string? Email { get; set; }
    }

    public enum AppointmentStatusViewModel
    {
        Scheduled,
        Completed,
        Canceled
    }
}
