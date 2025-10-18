using MySqlX.XDevAPI.Common;
using SistemaDeAgendamento.Repositories;
using SistemaDeAgendamento.Services.Mappings;
using SistemaDeAgendamento.Services.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeAgendamento.Services
{
    public interface IEmployeeService
    {
        CreateEmployeeResult Create(CreateEmployeeRequest request);

        IList<EmployeeResult> Read();

        EditEmployeeResult Edit(EditEmployeeRequest request);

        EmployeeResult? GetById(int id); 

        DeleteEmployeeResult Delete(int id);    
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAvailabilityRepository _availabilityRepository;
        public EmployeeService(IEmployeeRepository employeeRepository, IUserRepository userRepository, IAvailabilityRepository availabilityRepository)
        {
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
            _availabilityRepository = availabilityRepository;
        }

        public CreateEmployeeResult Create(CreateEmployeeRequest request)
        {
            var result = new CreateEmployeeResult();

            if (request == null)
            {
                result.ErrorMessage = "requisição inválida.";
                return result;
            }

            var existingUser = _userRepository.GetByUserName(request.UserName);

            if (existingUser != null)
            {
                result.ErrorMessage = "Usuário já existente.";
                return result;
            }

            var userId = _userRepository.Insert(request.MapToUser());

            if (!userId.HasValue)
            {
                result.ErrorMessage = "Erro ao inserir o usuário";

                return result;
            }

            var employeeId = _employeeRepository.Insert(request.MapToEmployee(userId.Value));

            if (!employeeId.HasValue)
            {
                result.ErrorMessage = "Erro ao inserir o funcionário";

                return result;
            }

            var availabilityId = _availabilityRepository.Insert(employeeId.Value);//fazer rota da lista de availability até aq, se fudeukkkk

            result.Success = true;

            return result;
        }


        public EditEmployeeResult Edit(EditEmployeeRequest request)
        {
            var result = new EditEmployeeResult();

            var existingUser = _userRepository.GetByUserName(request.UserName);

            if (existingUser != null && existingUser.Id != request.UserId)
            {
                result.ErrorMessage = "Já existe um usuário com esse Username";

                return result;
            }

            var affectedRows = _employeeRepository.Update(request.MapToEmployee());

            if (affectedRows == 0)
            {
                result.ErrorMessage = "Não foi possível atualizar o funcionário";

                return result;
            }

            affectedRows = _userRepository.Update(request.MapToUser());

            if (affectedRows == 0)
            {
                result.ErrorMessage = "Não foi possível atualizar o usuário";

                return result;
            }

            result.Success = true;

            return result;
        }

        public DeleteEmployeeResult Delete(int id)
        {
            var result = new DeleteEmployeeResult();

            var employee = _employeeRepository.GetById(id);

            if (employee == null)
            {
                result.ErrorMessage = "Não foi possível encontrar o funcionário";

                return result;
            }

            var affectedRows = _employeeRepository.Delete(id);

            if (affectedRows == 0)
            {
                result.ErrorMessage = "Não foi possível apagar o srviço";

                return result;
            }

            result.Success = true;

            return result;
        }

        public EmployeeResult? GetById(int id)
        {
            var employee = _employeeRepository.GetById(id);

            if (employee == null)
            {
                return null;
            }

            return employee.MapToEmployeeResult();
        }

        public IList<EmployeeResult> Read()
        {
            var employees = _employeeRepository.Read();

            var result = employees.Select(e => e.MapToEmployeeResult()).ToList();

            return result;
        }
    }
}
