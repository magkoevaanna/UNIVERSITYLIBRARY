using UniversityLibrary.Backend.Data.Entities;

namespace UniversityLibrary.Backend.Repositories;


public interface IMemberRepository
{
    List<Members> GetMembers();
}
