using System.Runtime.InteropServices;
using UniversityLibrary.Backend.DTO.Statistics;

namespace UniversityLibrary.Backend.Repositories;


public interface IStatisticsRepository
{
    ActiveDebtorsDto GetActiveDebtors(int pointId);
    OrderReportResponse GetReservedBooksReport();
}
