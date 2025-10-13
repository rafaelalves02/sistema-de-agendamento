using SistemaDeAgendamento.Repositories;
using SistemaDeAgendamento.Services.Mappings;
using SistemaDeAgendamento.Services.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeAgendamento.Services
{
    public interface IServiceService
    {
        CreateServiceResult Create(CreateServiceRequest request);

        IList<ServiceResult> Read();

        ServiceResult? GetById(int id);

        EditServiceResult Edit(EditServiceRequest request);

        DeleteServiceResult Delete(int id);
    }
    public class ServiceService : IServiceService
    {

        private readonly IServiceRepository _serviceRepository;

        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }   

        public CreateServiceResult Create(CreateServiceRequest request)
        {
            var result = new CreateServiceResult();

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                result.ErrorMessage = "O nome do serviço é obrigatório.";
                return result;
            }

            if (request.Price < 0)
            {
                result.ErrorMessage = "O preço do serviço deve ser maior que zero.";
                return result;
            }

            if (request.Duration <= 0)
            {
                result.ErrorMessage = "A duração do serviço deve ser maior que zero.";
                return result;
            }

            var serviceId = _serviceRepository.Insert(request.MapToService());

            if (!serviceId.HasValue)
            {
                result.ErrorMessage = "Não foi possível criar o serviço.";
                return result;
            }

            result.Success = true;

            return result;

        }

        public IList<ServiceResult> Read()
        {
            var services = _serviceRepository.Read();

            var result = services.Select(s => s.MapToServiceResult()).ToList();

            return result;
        }

        public ServiceResult? GetById(int id)
        {
            var service = _serviceRepository.GetById(id);

            if (service == null)
            {
                return null;
            }

            return service.MapToServiceResult();
        }

        public EditServiceResult Edit(EditServiceRequest request)
        {
            var result = new EditServiceResult();

            var affectedRows = _serviceRepository.Update(request.MapToService());

            if (affectedRows == 0)
            {
                result.ErrorMessage = "Não foi possível editar o serviço";

                return result;
            }

            result.Success = true;

            return result;
        }

        public DeleteServiceResult Delete(int id)
        {
            var result = new DeleteServiceResult();

            var aluno = _serviceRepository.GetById(id);

            if (aluno == null)
            {
                result.ErrorMessage = "Não foi possível encontrar o aluno";

                return result;
            }

            var affectedRows = _serviceRepository.Delete(id);

            if (affectedRows == 0)
            {
                result.ErrorMessage = "Não foi possível apagar o aluno";

                return result;
            }

            result.Success = true;

            return result;
        
    }
    }
}
