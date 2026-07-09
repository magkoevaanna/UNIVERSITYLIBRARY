namespace UniversityLibrary.Backend.DTO.Books;


public class TopTwentyBooks
{
    public int BookId {get; set;}
    public string Title {get; set;} = string.Empty;
    public string Author {get; set;} = string.Empty;
    public int FacultyOrders {get; set;}
    public int UniversityOrders {get; set;}
}