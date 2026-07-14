using MySql.Data.MySqlClient;
using UniversityLibrary.Backend.Data.Entities;

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





}