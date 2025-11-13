using SistemaDeAgendamento.Repositories;
using SistemaDeAgendamento.Services.Mappings;
using SistemaDeAgendamento.Services.Models.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeAgendamento.Services
{
    public interface IAppointmentService
    {
        IList<TimeSpan> GetAvailableSlots(int employeeId, DateTime date, int serviceDuration);

        CreateAppointmentResult Create(CreateAppointmentRequest request);

        IList<AppointmentResult> Read();

        IList<AppointmentResult> GetByEmployeeId(int employeeId);

        IList<AppointmentResult> GetByClientPhoneNumber(string phoneNumber);
            
    }


    public class AppointmentService(IAppointmentRepository appointmentRepository, IAvailabilityRepository availabilityRepository, IServiceRepository serviceRepository, IEmployeeRepository employeeRepository, IClientRepository clientRepository) : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository = appointmentRepository;
        private readonly IAvailabilityRepository _availabilityRepository = availabilityRepository;
        private readonly IServiceRepository _serviceRepository = serviceRepository;
        private readonly IEmployeeRepository _employeeRepository = employeeRepository;
        private readonly IClientRepository _clientRepository = clientRepository;

        public IList<TimeSpan> GetAvailableSlots(int employeeId, DateTime date, int serviceDuration)
        {
            var weekday = date.DayOfWeek;

            var availabilities = _availabilityRepository.GetAvailabilitiesByEmployeeAndWeekday(employeeId, weekday);

            var appointments = _appointmentRepository.GetAppointmentsByEmployeeAndDate(employeeId, date);

            var availableSlots = new List<TimeSpan>();

            foreach (var availability in availabilities)
            {
                DateTime start = date.Date.Add(availability.StartTime);
                DateTime end = date.Date.Add(availability.EndTime);

                DateTime current = start;

                while (current.AddMinutes(10) <= end)
                {
                    DateTime next = current.AddMinutes(serviceDuration);

                    bool hasConflict = appointments.Any(ap =>
                        (current < ap.EndTime && next > ap.StartTime)
                    );

                    if (!hasConflict && next <= end)
                        availableSlots.Add(current.TimeOfDay);

                    current = current.AddMinutes(10);
                }
            }

            return availableSlots;
        }


        public CreateAppointmentResult Create(CreateAppointmentRequest request)
        {
            var result = new CreateAppointmentResult();

            if (request == null)
            {
                result.ErrorMessage = "Request inválido.";
            }

            if (request!.StartTime < DateTime.Now)
            {
                result.ErrorMessage = "A data e hora do agendamento devem ser futuras.";
                return result;
            }

            if (string.IsNullOrWhiteSpace(request.ClientName))
            {
                result.ErrorMessage = "O nome do cliente é obrigatório.";
                return result;
            }

            if (string.IsNullOrWhiteSpace(request.ClientPhoneNumber))
            {
                result.ErrorMessage = "O telefone do cliente é obrigatório.";
                return result;
            }

            var service = _serviceRepository.GetById(request!.ServiceId);

            if (service == null)
            {
                result.ErrorMessage = "Serviço não encontrado.";
                return result;
            }

            var endTime = request.StartTime.AddMinutes(service.Duration);

            var employee = _employeeRepository.GetById(request.EmployeeId);

            if (employee == null)
            {
                result.ErrorMessage = "Funcionário não encontrado.";
                return result;
            }

            var availableSlots = GetAvailableSlots(request.EmployeeId, request.StartTime.Date, service.Duration);

            if (!availableSlots.Contains(request.StartTime.TimeOfDay))
            {
                result.ErrorMessage = "O horário selecionado não está disponível.";
                return result;
            }

            var client = _clientRepository.GetByPhoneNumber(request.ClientPhoneNumber);

            if (client == null)
            {
                 var newClientId = _clientRepository.Insert(request.MapToClient());

                 if (!newClientId.HasValue)
                 {
                    result.ErrorMessage = "Não foi possível criar o cliente.";
                    return result;
                 }
                 
                client = _clientRepository.GetById(newClientId.Value);
            }

            var appointmentId = _appointmentRepository.Insert(request.MapToAppointment(client!.Id, endTime));

            if (!appointmentId.HasValue)
            {
                result.ErrorMessage = "Não foi possível criar o agendamento.";
                return result;
            }

            result.Success = true;

            return result;
        }
        public IList<AppointmentResult> Read()
        {
            var result = new List<AppointmentResult>();

            var appointments = _appointmentRepository.Read();

            result = [.. appointments.Select(a => a.MapToAppointmentResult())];

            return result;
        }

        public IList<AppointmentResult> GetByEmployeeId(int employeeId)
        {
            var result = new List<AppointmentResult>();

            var appointments = _appointmentRepository.GetByEmployeeId(employeeId);

            result = [.. appointments.Select(a => a.MapToAppointmentResult())];

            return result;
        }

        public IList<AppointmentResult> GetByClientPhoneNumber(string phoneNumber)
        {
            var result = new List<AppointmentResult>();

            var appointments = _appointmentRepository.GetByClientPhoneNumber(phoneNumber);

            result = [.. appointments.Select(a => a.MapToAppointmentResult())];

            return result;
        }
    }
}

