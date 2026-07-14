namespace UniversityLibrary.Backend.Data.Entities;

public class Members
{
    public int CardNumber { get; set; }
    
    public string FullName { get; set; } = string.Empty;
    
    public int ReaderCategoryId { get; set; }
    public string? Faculty { get; set; }
    
    public string? Department { get; set; }
    
    public int? Course { get; set; }
    
    public string? GroupName { get; set; }
    public bool IsSuspended { get; set; }
    
    public DateTime? SuspendedUntil { get; set; }
    
    public DateTime RegistrationDate { get; set; }
    
    public DateTime? ExitDate { get; set; }
}
