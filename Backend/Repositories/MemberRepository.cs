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





}