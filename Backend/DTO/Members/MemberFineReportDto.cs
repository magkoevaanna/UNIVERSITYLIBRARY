namespace UniversityLibrary.Backend.DTO.Members;

public class MemberFineReportDto
{
    public string FullName { get; set; } = string.Empty;
    public string? Faculty { get; set; }
    public string? Department { get; set; }
    public int? Course { get; set; }
    public string? GroupName { get; set; }
    public string AccessStatus { get; set; } = string.Empty;
    public int LostBooksCount { get; set; }
    public decimal TotalFineAmount { get; set; }
    public string LostBooksList { get; set; } = string.Empty;
}