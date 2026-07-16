using Microsoft.AspNetCore.Mvc;
using UniversityLibrary.Backend.Repositories;

namespace UniversityLibrary.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController : ControllerBase
{
    private readonly IStatisticsRepository StatisticsRepository;

    public StatisticsController(IStatisticsRepository StatisticsRepository)
    {
        this.StatisticsRepository = StatisticsRepository;
    }

    [HttpGet("Список всех читателей-задолжников.")]
    public IActionResult GetActiveDebtors([FromQuery] int pointId = 1)
    {
        var result = StatisticsRepository.GetActiveDebtors(pointId);
        return Ok(result);
    }

    [HttpGet("Список всех заказанных книг.")]
    public IActionResult GetReservedBooksReport()
    {
        var result = StatisticsRepository.GetReservedBooksReport();
        return Ok(result);
    }

    [HttpGet("Список книг и колличества их экземпляров по пунктам выдачи.")]
    public IActionResult GetBooksDistributionByHall([FromQuery] int pointId = 1)
    {
        var result = StatisticsRepository.GetBooksDistributionByHall(pointId);
        return Ok(result);
    }
}
