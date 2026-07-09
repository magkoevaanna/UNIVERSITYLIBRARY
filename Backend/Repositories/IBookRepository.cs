using UniversityLibrary.Backend.Data.Entities;
using UniversityLibrary.Backend.DTO.Books;

namespace UniversityLibrary.Backend.Repositories;


public interface IBookRepository
{
    List<Books> GetBooks();
    List<TopTwentyBooks> GetTopTwentyBooks(int DistributionPointId, string faculty);
}
