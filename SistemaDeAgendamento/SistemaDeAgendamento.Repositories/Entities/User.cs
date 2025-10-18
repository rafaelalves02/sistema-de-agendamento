using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeAgendamento.Repositories.Entities
{
    public class User
    {
        public int Id { get; set; }

        public required string UserName { get; set; }

        public required string Password { get; set; }

        public int RoleId { get; set; }
    }
}
