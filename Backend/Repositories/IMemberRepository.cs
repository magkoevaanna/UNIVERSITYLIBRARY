using System.Runtime.InteropServices;
using UniversityLibrary.Backend.Data.Entities;
using UniversityLibrary.Backend.DTO.Members;

namespace UniversityLibrary.Backend.Repositories;


public interface IMemberRepository
{
    List<Members> GetMembers();
    MemberListDto GetMembersByDistributionPoint(int pointId, int course);
    List<MemberFineReportDto> GetMemberByName(string name);

}
