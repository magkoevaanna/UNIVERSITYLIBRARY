namespace UniversityLibrary.Backend.Data.Entities;
public class Books
{
    public int BookId {get; set;}  
    public string ISBN {get; set;} = string.Empty;
    public string Title {get; set;}  = string.Empty;
    public string Author {get; set;}  = string.Empty;
    public int PublishingYear {get; set;}  
    public string ImageUrl {get; set;}  = string.Empty;
}
