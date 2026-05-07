using UniversityTransportSystem.Business.Interfaces;
using UniversityTransportSystem.Business.Models;

namespace UniversityTransportSystem.DataAccess.Repositories;

public class StudentRepository : BaseRepository<Student>
{
    public StudentRepository() : base("Student")
    {
    }
}
