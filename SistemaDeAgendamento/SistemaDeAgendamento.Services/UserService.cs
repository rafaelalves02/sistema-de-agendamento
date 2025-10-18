using SistemaDeAgendamento.Repositories;
using SistemaDeAgendamento.Services.Mappings;
using SistemaDeAgendamento.Services.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeAgendamento.Services
{
    public interface IUserService 
    {
        ValidateLoginResult ValidateLogin(string username, string password);   
    }


    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ValidateLoginResult ValidateLogin(string username, string password)
        {
            var result = new ValidateLoginResult();

            if (string.IsNullOrWhiteSpace(username))
            {
                result.ErrorMessage = "Usuário é obrigatório";
                return result;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                result.ErrorMessage = "Senha é obrigatória";
                return result;
            }

            var user = _userRepository.GetByUserName(username);

            if (user == null)
            {
                result.ErrorMessage = "Ususário ou Senha incorreto";
                return result;
            }

            if (user.Password != password)
            {
                result.ErrorMessage = "Ususário ou Senha incorreto";
                return result;  
            }

            result.User = user.MapToUserResult();

            result.Success = true;

            return result;
        }
    }
}
