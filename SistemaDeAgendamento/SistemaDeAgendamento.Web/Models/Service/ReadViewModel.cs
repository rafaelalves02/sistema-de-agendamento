namespace SistemaDeAgendamento.Web.Models.Service
{
    public class ReadViewModel
    {
        public IList<ServiceViewModel>? Services { get; set; }
    }

    public class ServiceViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public int DurationInMinutes { get; set; }
    }
}
