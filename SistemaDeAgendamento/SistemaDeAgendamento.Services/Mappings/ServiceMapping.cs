using SistemaDeAgendamento.Repositories.Entities;
using SistemaDeAgendamento.Services.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeAgendamento.Services.Mappings
{
    public static class ServiceMapping
    {
        public static Service MapToService(this CreateServiceRequest request)
        {
            return new Service
            {
                Name = request.Name,
                Price = request.Price,
                Duration = request.Duration
            };
        }

        public static ServiceResult MapToServiceResult(this Service service)
        {
            return new ServiceResult
            {
                Id = service.Id,
                Name = service.Name,
                Price = service.Price,
                Duration = service.Duration
            };
        }

        public static Service MapToService(this EditServiceRequest request)
        {
            return new Service
            {
                Id = request.Id,
                Name = request.Name,
                Price = request.Price,
                Duration = request.Duration
            };
        }
    }
}
