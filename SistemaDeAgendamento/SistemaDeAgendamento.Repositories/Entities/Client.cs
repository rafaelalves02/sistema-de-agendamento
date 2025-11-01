using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeAgendamento.Repositories.Entities
{
    public class Client
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string PhoneNumber { get; set; }
    }
}
