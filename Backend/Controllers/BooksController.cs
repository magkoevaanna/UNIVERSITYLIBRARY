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
    
    [HttpGet("GetBooks")]// перечень всех книг
    public IActionResult GetBooks()
    {
        var result = bookRepository.GetBooks();
        return Ok(result);
    }
}