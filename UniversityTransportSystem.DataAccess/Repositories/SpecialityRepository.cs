using UniversityTransportSystem.Business.Interfaces;
using UniversityTransportSystem.Business.Models;

namespace UniversityTransportSystem.DataAccess.Repositories;

public class SpecialityRepository : BaseRepository<Speciality>
{
    public SpecialityRepository() : base("Speciality")
    {
    }
}
