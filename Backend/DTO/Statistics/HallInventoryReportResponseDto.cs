public class HallInventoryReportResponseDto
{
    public int TotalBooksInHall { get; set; }
    public List<HallInventoryItem> Items { get; set; } = new();
}

public class HallInventoryItem
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int CopiesCount { get; set; } 
}
