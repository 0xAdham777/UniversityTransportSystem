using UniversityTransportSystem.Business.Interfaces;
using UniversityTransportSystem.Business.Models;

namespace UniversityTransportSystem.DataAccess.Repositories;

public class ScheduleRepository : BaseRepository<Schedule>
{
    public ScheduleRepository() : base("Schedule")
    {
    }
}
