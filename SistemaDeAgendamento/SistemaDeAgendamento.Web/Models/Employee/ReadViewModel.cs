namespace SistemaDeAgendamento.Web.Models.Employee
{
    public class ReadViewModel
    {
        public IList<EmployeeViewModel>? Employees { get; set; }
    }

    public class EmployeeViewModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public required string Name { get; set; }

        public required string PhoneNumber { get; set; }

        public string? Email { get; set; }

        public required string UserName { get; set; }
    }
}
