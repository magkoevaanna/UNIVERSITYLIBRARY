using Microsoft.AspNetCore.Mvc;
using UniversityLibrary.Backend.Repositories;
namespace UniversityLibrary.Backend.Controllers;


[ApiController]
[Route("api/[controller]")]

public class MembersController : ControllerBase
{
    private readonly IMemberRepository memberRepository;

    public MembersController(IMemberRepository memberRepository)
    {
        this.memberRepository = memberRepository;
    }
    
    [HttpGet("Список всех пользователей")]// перечень всех пользователей
    public IActionResult GetMembers()
    {
        var result = memberRepository.GetMembers();
        return Ok(result);
    }


}