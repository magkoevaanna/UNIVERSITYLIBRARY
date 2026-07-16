public class OrderReportResponse
{
    public int TotalOrders { get; set; }
    public List<OrderReportItem> Items { get; set; } = new();
}

public class OrderReportItem
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public DateTime ActionDate { get; set; }
}
