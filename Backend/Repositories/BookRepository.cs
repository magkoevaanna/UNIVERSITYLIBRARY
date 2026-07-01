using MySql.Data.MySqlClient;
using UniversityLibrary.Backend.Data.Entities;
namespace UniversityLibrary.Backend.Repositories;



public class BookRepository : IBookRepository
{
    private readonly string ConnectionString;

    public BookRepository(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public List<Books> GetBooks()
    {
        var books = new List<Books>();        
        string sql = "SELECT * FROM books;";

        using (var connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();

            using (var command = new MySqlCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {                    
                    while (reader.Read())
                    {
                        var list = new Books()
                        {
                            BookId =    Convert.ToInt32(reader["book_id"]),
                            ISBN = reader["isbn"].ToString()!,
                            Title = reader["title"].ToString()!,
                            Author = reader["author"].ToString()!,
                            PublishingYear = Convert.ToInt32(reader["publishing_year"]),
                            ImageUrl = reader["image_url"].ToString()!,

                        };
                        books.Add(list);
                    }
                }
            }
        }
        return books;
    }

}