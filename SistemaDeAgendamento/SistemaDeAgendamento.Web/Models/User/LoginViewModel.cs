namespace SistemaDeAgendamento.Web.Models.User
{
    public class LoginViewModel
    {
        public required string UserName { get; set; }

        public required string Password { get; set; }

        public bool RemerberMe { get; set; }
    }
}
