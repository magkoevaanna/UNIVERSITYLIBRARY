using MySql.Data.MySqlClient;
using UniversityLibrary.Backend.Data.Entities;
using UniversityLibrary.Backend.DTO.Members;

namespace UniversityLibrary.Backend.Repositories;


public class MemberRepository : IMemberRepository
{
    private readonly string ConnectionString;

    public MemberRepository(string connectionString)
    {
        ConnectionString = connectionString;
    }

    public List<Members> GetMembers()
    {
        var result = new List<Members>();        
        string sql = @"SELECT * FROM library_members;" ;

        using (var connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();

            using (var command = new MySqlCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {                    
                    while (reader.Read())
                    {
                        var list = new Members()
                        {
                            CardNumber = Convert.ToInt32(reader["card_number"]),
                            FullName = reader["full_name"].ToString()!,
                            ReaderCategoryId = Convert.ToInt32(reader["reader_category_id"]),
                            Faculty = reader["faculty"] == DBNull.Value ? null : reader["faculty"].ToString(),
                            Department = reader["department"] == DBNull.Value ? null : reader["department"].ToString()!,
                            Course = reader["course"] == DBNull.Value ? null : Convert.ToInt32(reader["course"]),
                            GroupName = reader["group_name"] == DBNull.Value ? null : reader["group_name"].ToString()!,
                            IsSuspended = Convert.ToBoolean(reader["is_suspended"]),
                            SuspendedUntil = reader["suspended_until"] == DBNull.Value ? null : Convert.ToDateTime(reader["suspended_until"]),
                            RegistrationDate = Convert.ToDateTime(reader["registration_date"]),
                            ExitDate = reader["exit_date"] == DBNull.Value ? null : Convert.ToDateTime(reader["exit_date"]),
                        };
                        result.Add(list);
                    }
                }
            }
        }
        return result;
    }


    public MemberListDto GetMembersByDistributionPoint(int pointId, int course)
    {
        var result = new MemberListDto();        
        string sql = @"SELECT DISTINCT
                        lm.card_number,
                        lm.full_name,
                        lm.faculty,
                        lm.department,
                        lm.course,
                        lm.group_name,
                        COUNT(*) OVER() AS total_readers_count
                    FROM library_members lm
                    JOIN reader_logs rl ON lm.card_number = rl.member_card_number
                    JOIN book_inventories bi ON rl.book_inventory_id = bi.inventory_id
                    WHERE bi.distribution_point_id = @pointId   
                    AND lm.course = @course;" ;

        using (var connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();

            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@pointId", pointId);
                command.Parameters.AddWithValue("@course", course);

                using (var reader = command.ExecuteReader())
                {                    
                    while (reader.Read())
                    {
                        var member = new ReaderShortDto()
                        {
                            CardNumber = Convert.ToInt32(reader["card_number"]),
                            FullName = reader["full_name"].ToString()!,
                            Faculty = reader["faculty"] == DBNull.Value ? null : reader["faculty"].ToString(),
                            Department = reader["department"] == DBNull.Value ? null : reader["department"].ToString(),
                            Course = reader["course"] == DBNull.Value ? null : Convert.ToInt32(reader["course"]),
                            GroupName = reader["group_name"] == DBNull.Value ? null : reader["group_name"].ToString()
                        };
                        result.TotalReadersCount = Convert.ToInt32(reader["total_readers_count"]);
                        result.Readers.Add(member);

                    }
                }
            }
        }
        return result;
    }




    public List<MemberFineReportDto> GetMemberByName(string name)
    {
        var result = new List<MemberFineReportDto>();        
        string sql = @"SELECT 
                        lm.full_name,
                        lm.faculty,
                        lm.department,
                        lm.course,
                        lm.group_name,
                        CASE WHEN lm.is_suspended = 1 THEN 'Заблокирован' ELSE 'Активен' END AS access_status,
                        COUNT(CASE WHEN rl.action_status = 'lost' THEN 1 END) AS lost_books_count,
                        IFNULL(SUM(rl.fine_amount), 0.00) AS total_fine_amount,
                        IFNULL(GROUP_CONCAT(DISTINCT b.title SEPARATOR ', '), 'Нет утерянных книг') AS lost_books_list
                    FROM library_members lm
                    LEFT JOIN reader_logs rl ON lm.card_number = rl.member_card_number
                    LEFT JOIN book_inventories bi ON rl.book_inventory_id = bi.inventory_id
                    LEFT JOIN books b ON bi.book_id = b.book_id
                    WHERE lm.full_name LIKE @name
                    GROUP BY lm.card_number, lm.full_name, lm.faculty, lm.department, lm.course, lm.group_name, lm.is_suspended;" ;

        using (var connection = new MySqlConnection(ConnectionString))
        {
            connection.Open();

            using (var command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@name", name + "%");

                using (var reader = command.ExecuteReader())
                {                    
                    while (reader.Read())
                    {
                        var member = new MemberFineReportDto()
                        {
                            FullName = reader["full_name"].ToString()!,
                            Faculty = reader["faculty"] == DBNull.Value ? null : reader["faculty"].ToString(),
                            Department = reader["department"] == DBNull.Value ? null : reader["department"].ToString(),
                            Course = reader["course"] == DBNull.Value ? null : Convert.ToInt32(reader["course"]),
                            GroupName = reader["group_name"] == DBNull.Value ? null : reader["group_name"].ToString(),
                            AccessStatus = reader["access_status"].ToString()!,
                            LostBooksCount = Convert.ToInt32(reader["lost_books_count"]),
                            TotalFineAmount = Convert.ToDecimal(reader["total_fine_amount"]),
                            LostBooksList = reader["lost_books_list"].ToString()!
                        };
                        result.Add(member);

                    }
                }
            }
        }
        return result;
    }





}