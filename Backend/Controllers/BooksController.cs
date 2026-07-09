using Microsoft.AspNetCore.Mvc;
using UniversityLibrary.Backend.Repositories;
namespace UniversityLibrary.Backend.Controllers;


[ApiController]
[Route("api/[controller]")]

public class BooksController : ControllerBase
{
    private readonly IBookRepository bookRepository;

    public BooksController(IBookRepository bookRepository)
    {
        this.bookRepository = bookRepository;
    }
    
    [HttpGet("Список всех книг")]// перечень всех книг
    public IActionResult GetBooks()
    {
        var result = bookRepository.GetBooks();
        return Ok(result);
    }


    [HttpGet("ЗАПРОС 2. Список двадцати популярных книг")]// Запрос 2
    public IActionResult GetTopTwentyBooks([FromQuery] int DistributionPointId = 1, string faculty = "ЭФ")
    {
        var result = bookRepository.GetTopTwentyBooks(DistributionPointId, faculty);
        return Ok(result);
    }
}