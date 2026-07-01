using UniversityLibrary.Backend.Data.Entities;

namespace UniversityLibrary.Backend.Repositories;


public interface IBookRepository
{
    List<Books> GetBooks();
}
