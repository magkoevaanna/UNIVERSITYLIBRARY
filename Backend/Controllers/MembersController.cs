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

    [HttpGet("Список пользователей по пункту выдачи и курсу.")]
    public IActionResult GetMembersByDistributionPoint([FromQuery] int pointId = 1, int course = 2)
    {
        var result = memberRepository.GetMembersByDistributionPoint(pointId, course);
        return Ok(result);
    }

    [HttpGet("Вывести всю информацию о пользователе по фамилии")]
    public IActionResult GetMemberByName([FromQuery] string name = "Иванов")
    {
        var result = memberRepository.GetMemberByName(name);
        return Ok(result);
    }


}