using SistemaDeAgendamento.Repositories.Entities;
using SistemaDeAgendamento.Services.Models.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeAgendamento.Services.Mappings
{
    public static class ClientMapping
    {
        public static Client MapToClient(this CreateAppointmentRequest request)
        {
            return new Client
            {
                Name = request.ClientName,
                PhoneNumber = request.ClientPhoneNumber
            };
        }
    }
}
