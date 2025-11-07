namespace SistemaDeAgendamento.Web.Models.Service
{
    public class EditViewModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public int Duration { get; set; }
    }
}
