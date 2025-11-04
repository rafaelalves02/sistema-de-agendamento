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
                ServiceId = model.Service!.Id,
                EmployeeId = model.EmployeeId,
                StartTime = model.StartTime!.Value,
                ClientName = model.Client!.Name!,
                ClientPhoneNumber = model.Client.PhoneNumber!
            };
        }

        public static AppointmentViewModel MapToAppointmentViewModel(this AppointmentResult result)
        {
            return new AppointmentViewModel
            {
                Id = result.Id,
                Client = new ClientViewModel
                {
                    Id = result.Client.Id,
                    Name = result.Client.Name,
                    PhoneNumber = result.Client.PhoneNumber
                },
                Service = new ServiceViewModel
                {
                    Id = result.Service.Id,
                    Name = result.Service.Name,
                    Price = result.Service.Price,
                    Duration = result.Service.Duration
                },
                Employee = new AppointmentEmployeeViewModel 
                { 
                    Id = result.Employee.Id,
                    Name = result.Employee.Name,
                    PhoneNumber = result.Employee.PhoneNumber,
                    Email = result.Employee.Email
                },

                Status = (AppointmentStatusViewModel)result.Status,
                StartTime = result.StartTime,
                EndTime = result.EndTime,
                CreatedAt = result.CreatedAt
            };
        }
    }
}
