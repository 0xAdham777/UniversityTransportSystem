using UniversityTransportSystem.Business.Interfaces;
using UniversityTransportSystem.Business.Models;

namespace UniversityTransportSystem.DataAccess.Repositories;

public class PersonRepository : BaseRepository<Person>
{
    public PersonRepository() : base("Person")
    {
    }
}
