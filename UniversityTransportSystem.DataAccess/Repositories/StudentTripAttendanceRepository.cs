using UniversityTransportSystem.Business.Interfaces;
using UniversityTransportSystem.Business.Models;

namespace UniversityTransportSystem.DataAccess.Repositories;

public class StudentTripAttendanceRepository : BaseRepository<StudentTripAttendance>
{
    public StudentTripAttendanceRepository() : base("StudentTripAttendance")
    {
    }
}
