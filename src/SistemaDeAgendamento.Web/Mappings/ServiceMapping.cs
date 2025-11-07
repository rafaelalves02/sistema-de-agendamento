using SistemaDeAgendamento.Services.Models.Service;
using SistemaDeAgendamento.Web.Models.Appointment;
using SistemaDeAgendamento.Web.Models.Service;

namespace SistemaDeAgendamento.Web.Mappings
{
    public static class ServiceMapping
    {
        public static CreateServiceRequest MapToCreateServiceRequest(this SistemaDeAgendamento.Web.Models.Service.CreateViewModel model)
        {
            return new CreateServiceRequest
            {
                Name = model.Name,
                Price = model.Price,
                Duration = model.DurationInMinutes
            };
        }

        public static SistemaDeAgendamento.Web.Models.Service.ServiceViewModel MapToServiceViewModel(this ServiceResult serviceResult)
        {
            return new SistemaDeAgendamento.Web.Models.Service.ServiceViewModel
            {
                Id = serviceResult.Id,
                Name = serviceResult.Name,
                Price = serviceResult.Price,
                DurationInMinutes = serviceResult.Duration
            };
        }

        public static EditViewModel MapToEditViewModel(this ServiceResult serviceResult)
        {
            return new EditViewModel
            {
                Id = serviceResult.Id,
                Name = serviceResult.Name,
                Price = serviceResult.Price,
                Duration = serviceResult.Duration
            };
        }

        public static EditServiceRequest MapToEditServiceRequest(this EditViewModel model)
        {
            return new EditServiceRequest
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Duration = model.Duration
            };
        }

        public static AppointmentServiceViewModel MapToAppointmentServiceViewModel(this ServiceResult result)
        {
            return new AppointmentServiceViewModel
            {
                Id = result.Id,
                Name = result.Name,
                Price = result.Price,
                Duration = result.Duration
            };
        }
    }
}
