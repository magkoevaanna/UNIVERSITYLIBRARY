using MySql.Data.MySqlClient;
using UniversityLibrary.Backend.Data.Entities;
using UniversityLibrary.Backend.DTO.Books;

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


    public List<TopTwentyBooks> GetTopTwentyBooks(int DistributionPointId, string faculty) // Запрос 2
    {
        var books = new List<TopTwentyBooks>();
        string sql = @"SELECT DISTINCT
                            b.book_id,
                            b.title,
                            b.author,
                            COUNT(rl.log_id) OVER(PARTITION BY b.book_id) AS `faculty_orders`,
                            COUNT(rl.log_id) OVER(PARTITION BY b.book_id) AS `university_orders`
                        FROM reader_logs rl
                        JOIN book_inventories bi ON rl.book_inventory_id = bi.inventory_id
                        JOIN books b ON bi.book_id = b.book_id
                        JOIN library_members lm ON rl.member_card_number = lm.card_number
                        WHERE bi.distribution_point_id = @distribution_point_id
                        AND lm.faculty = @faculty
                        AND rl.action_status IN ('borrowed', 'reserved')
                        ORDER BY `faculty_orders` DESC
                        LIMIT 20;";

        using (var connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();

            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@faculty", faculty);
                command.Parameters.AddWithValue("@distribution_point_id", DistributionPointId);
                using (var reader = command.ExecuteReader())
                {                    
                    while (reader.Read())
                    {
                        var list = new TopTwentyBooks()
                        {
                            BookId =    Convert.ToInt32(reader["book_id"]),
                            Title = reader["title"].ToString()!,
                            Author = reader["author"].ToString()!,
                            FacultyOrders = Convert.ToInt32(reader["faculty_orders"]),
                            UniversityOrders = Convert.ToInt32(reader["university_orders"]),

                        };
                        books.Add(list);
                    }
                }
            }
        }
        return books;
    }




}