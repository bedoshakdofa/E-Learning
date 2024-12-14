using E_Learning.Data.Model;
using E_Learning.DTOs;
namespace E_Learning.Interfaces
{
    public interface IDepartmentRepository
    {
        Task AddDeptAsync(DepartmentDTO deptDTO);

        void DeleteDeptAsync(Department dept);

        Task<Department> GetDeptByIdAsync(int id);
        Task<DepartmentResponse> GetDepartmentByName(string name);

        Task <bool> SaveAllAsync();

        Task<IEnumerable<DepartmentDTO>> GetAllDepartmentAsync();

    }
}
