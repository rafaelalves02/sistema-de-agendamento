namespace SistemaDeAgendamento.Web.Models.Service
{
    public class CreateViewModel
    {
        public required string Name { get; set; }

        public required decimal Price { get; set; }

        public int DurationInMinutes { get; set; }
    }
}
