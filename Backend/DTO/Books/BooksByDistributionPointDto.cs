namespace UniversityLibrary.Backend.DTO.Books;

public class BooksByDistributionPointDto
{
    public int TotalBooksInHall { get; set; }
    public List<BookInHallDto> Books { get; set; } = new();
}

public class BookInHallDto
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int CopiesInHall { get; set; }
}
