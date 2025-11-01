using SistemaDeAgendamento.Services.Models.Appointment;
using SistemaDeAgendamento.Web.Models.Appointment;

namespace SistemaDeAgendamento.Web.Mappings
{
    public static class AppointmentMapping
    {
        public static CreateAppointmentRequest MapToCreateAppointmentRequest(this CreateViewModel model)
        {           
            return new CreateAppointmentRequest
            {
                ServiceId = model.Service.Id,
                EmployeeId = model.EmployeeId,
                StartTime = model.StartTime!.Value,
                ClientName = model.Client.Name!,
                ClientPhoneNumber = model.Client.PhoneNumber!
            };
        }
    }
}
