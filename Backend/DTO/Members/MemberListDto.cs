namespace UniversityLibrary.Backend.DTO.Members;


public class MemberListDto
{
    public int TotalReadersCount { get; set; }
    public List<ReaderShortDto> Readers { get; set; } = new();
}

public class ReaderShortDto
{
    public int CardNumber { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? Faculty { get; set; }
    public string? Department { get; set; }
    public int? Course { get; set; }
    public string? GroupName { get; set; }
}
