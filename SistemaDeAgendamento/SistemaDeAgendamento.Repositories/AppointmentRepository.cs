using EnglishNow.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeAgendamento.Repositories
{
    public interface IAppointmentRepository
    {

    }

    public class AppointmentRepository : BaseRepository, IAppointmentRepository
    {
        public AppointmentRepository(string connectionString) : base(connectionString)
        {

        }
    }
}
