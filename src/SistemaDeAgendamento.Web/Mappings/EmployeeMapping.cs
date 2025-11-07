using SistemaDeAgendamento.Services.Models.Employee;
using SistemaDeAgendamento.Web.Models.Employee;

namespace SistemaDeAgendamento.Web.Mappings
{
    public static class EmployeeMapping
    {
        public static CreateEmployeeRequest MapToCreateEmployeeRequest(this CreateViewModel model)
        {
            return new CreateEmployeeRequest
            {
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                EmployeeAvailability = model.EmployeeAvailability
                    .Select(vm => new EmployeeAvailability
                    {
                        WeekDay = (Services.Enums.WeekDay)vm.WeekDay,
                        StartTime = vm.StartTime,   
                        EndTime = vm.EndTime,
                        IsActive = vm.IsActive,
                    })
                    .ToList()
            };
        }

        public static EmployeeViewModel MapToEmployeeViewModel(this EmployeeResult result)
        {
            return new EmployeeViewModel
            {
                Id = result.Id,
                UserId = result.UserId,
                Name = result.Name,
                UserName = result.UserName!,
                PhoneNumber= result.PhoneNumber,
                Email = result.Email
            };
        }

        public static EditViewModel MapToEditViewModel(this EmployeeResult result)
        {
            return new EditViewModel
            {
                Id = result.Id,
                UserId = result.UserId,
                Name = result.Name,
                Email = result.Email,
                Password = result.Password!,
                PhoneNumber = result.PhoneNumber,
                UserName = result.UserName!,
                EmployeeAvailability = (result.EmployeeAvailability ?? new List<EmployeeAvailability>())
                    .Select(a => new EditEmployeeAvailabilityViewModel
                    {
                        Id = a.Id,
                        WeekDay = (Web.Enums.WeekDay)a.WeekDay,
                        StartTime = a.StartTime,
                        EndTime = a.EndTime,
                        IsActive = a.IsActive
                    })
                    .ToList()
            };
        }

        public static EditEmployeeRequest MapToEditEmployeeRequest(this EditViewModel model)
        {
            return new EditEmployeeRequest
            {
                Id = model.Id,
                UserId = model.UserId,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                UserName = model.UserName,
                Password = model.Password,
                EmployeeAvailability = model.EmployeeAvailability
                    .Select(vm => new EmployeeAvailability
                    {
                        Id = vm.Id,
                        WeekDay = (Services.Enums.WeekDay)vm.WeekDay,
                        StartTime = vm.StartTime,
                        EndTime = vm.EndTime,
                        IsActive = vm.IsActive,
                    })
                    .ToList()
            };
        }

        public static ReadViewModel MapToReadViewModel(this EmployeeResult result)
        {
            return new ReadViewModel
            {
                Employees = new List<EmployeeViewModel>
                {
                    result.MapToEmployeeViewModel()
                },
                EmployeeAvailability = (result.EmployeeAvailability ?? new List<EmployeeAvailability>())
                    .Select(a => new ReadEmployeeAvailabilityViewModel
                    {
                        WeekDay = (Web.Enums.WeekDay)a.WeekDay,
                        StartTime = a.StartTime,
                        EndTime = a.EndTime,
                        IsActive = a.IsActive
                    })
                    .ToList()
            };
        }
    }
}
