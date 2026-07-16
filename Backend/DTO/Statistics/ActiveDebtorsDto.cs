namespace UniversityLibrary.Backend.DTO.Statistics;

public class ActiveDebtorsDto
{
    public int TotalDebtorsCount { get; set; }
    public List<DebtorDetailsDto> Debtors { get; set; } = new();
}

public class DebtorDetailsDto
{
    public int CardNumber { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? Faculty { get; set; }
    public string? Department { get; set; }
    public int? Course { get; set; }
    public string? GroupName { get; set; }
    public string CategoryName { get; set; } = string.Empty; 
}
