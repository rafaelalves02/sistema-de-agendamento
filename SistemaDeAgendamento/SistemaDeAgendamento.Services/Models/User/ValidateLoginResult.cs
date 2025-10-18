using SistemaDeAgendamento.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeAgendamento.Services.Models.User
{
    public class ValidateLoginResult : BaseResult
    {
        public UserResult? User { get; set; }
    }

    public class UserResult
    { 
        public int Id { get; set; }

        public required string UserName { get; set; }   
        
        public Role Role { get; set; }
    }


}
