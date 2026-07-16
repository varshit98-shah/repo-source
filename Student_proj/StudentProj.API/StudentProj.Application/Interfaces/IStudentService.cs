using StudentProj.Application.DTOs;
using StudentProj.Domain.Entities;

namespace StudentProj.Application.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentDTO>> GetAllStudentsasync();
        Task<StudentDTO> GetStudentbyid(int id);
        Task<int> Createstudentasync(RegisterDTO dto, string? createdBy = null, string? ipAddress = null);
        Task<(bool Success, string Error)> UpdateStudentasync(int id, StudentDTO dto, string? updatedBy = null, string? ipAddress = null);
        Task<IEnumerable<StudentDTO>> Getstudentbynameasync(string name);
        Task<StudentDTO> GetStudentbyemailasync(string email);
        Task<bool> DeleteStudentasync(int id, string? deletedBy = null);
        Task<int> UpsertStudentAsync(StudentDTO student);
        Task<PaginatedResultDTO<StudentDTO>> GetPaginatedStudentsAsync(PaginatedRequestDTO request);

    }
}
