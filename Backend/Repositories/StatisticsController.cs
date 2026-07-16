using MySql.Data.MySqlClient;
using UniversityLibrary.Backend.DTO.Statistics;

namespace UniversityLibrary.Backend.Repositories;


public class StatisticsRepository : IStatisticsRepository
{
    private readonly string ConnectionString;

    public StatisticsRepository(string connectionString)
    {
        ConnectionString = connectionString;
    }


    public ActiveDebtorsDto GetActiveDebtors(int pointId)
    {
        var result = new ActiveDebtorsDto();
        string sql = @"SELECT DISTINCT
                        lm.card_number,
                        lm.full_name,
                        lm.faculty,
                        lm.department,
                        lm.course,
                        lm.group_name,
                        rc.name AS category_name,
                        COUNT(*) OVER() AS total_debtors_count
                    FROM library_members lm
                    JOIN reader_categories rc ON lm.reader_category_id = rc.category_id
                    JOIN reader_logs rl ON lm.card_number = rl.member_card_number
                    JOIN book_inventories bi ON rl.book_inventory_id = bi.inventory_id
                    WHERE rl.action_status = 'borrowed'
                    AND NOT EXISTS (
                        SELECT 1 FROM reader_logs rl2 
                        WHERE rl2.book_inventory_id = rl.book_inventory_id 
                            AND rl2.member_card_number = rl.member_card_number
                            AND rl2.action_status IN ('returned', 'lost')
                            AND rl2.action_date >= rl.action_date
                    )
                    AND CURDATE() > rl.action_date + INTERVAL (rc.max_days + 10) DAY
                    AND bi.distribution_point_id = @pointId ;";
        
        using(var connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();

            using(var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@pointId", pointId);

                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var list = new DebtorDetailsDto()
                        {
                            CardNumber = Convert.ToInt32(reader["card_number"]),
                            FullName = reader["full_name"].ToString()!,
                            Faculty = reader["faculty"] == DBNull.Value ? null : reader["faculty"].ToString(),
                            Department = reader["department"] == DBNull.Value ? null : reader["department"].ToString(),
                            Course = reader["course"] == DBNull.Value ? null : Convert.ToInt32(reader["course"]),
                            GroupName = reader["group_name"] == DBNull.Value ? null : reader["group_name"].ToString(),
                            CategoryName = reader["category_name"].ToString()!
                        };
                        result.TotalDebtorsCount = Convert.ToInt32(reader["total_debtors_count"]);
                        result.Debtors.Add(list);
                    }
                }

            }
        }
        return result;
    }



    public OrderReportResponse GetReservedBooksReport()
    {
        var result = new OrderReportResponse();
        string sql = @"SELECT 
                        b.title,
                        b.author,
                        rl.action_date,
                        COUNT(*) OVER() AS `total_orders`
                    FROM reader_logs rl
                    JOIN book_inventories bi ON rl.book_inventory_id = bi.inventory_id
                    JOIN books b ON bi.book_id = b.book_id
                    WHERE rl.action_status = 'reserved';";
        
        using(var connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();

            using(var command = new MySqlCommand(sql, connection))
            {

                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var list = new OrderReportItem()
                        {
                            Title = reader["title"].ToString()!,
                            Author = reader["author"].ToString()!,
                            ActionDate = Convert.ToDateTime(reader["action_date"])
                        };
                        result.TotalOrders = Convert.ToInt32(reader["total_orders"]);
                        result.Items.Add(list);
                    }
                }

            }
        }
        return result;
    }



}