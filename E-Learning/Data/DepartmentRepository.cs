using AutoMapper;
using E_Learning.DTOs;
using E_Learning.Data.Model;
using E_Learning.Interfaces;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Data
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DbContextApp _context;
        private readonly IMapper _mapper;
        public DepartmentRepository(DbContextApp context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddDeptAsync(DepartmentDTO deptDTO)
        {
            deptDTO.Name=deptDTO.Name.ToLower();
            await _context.Departments.AddAsync(_mapper.Map<Department>(deptDTO));
        }

        public void DeleteDeptAsync(Department dept)
        {
            _context.Departments.Remove(dept);
        }

        public async Task<Department> GetDeptByIdAsync(int id)
        {
            var dept = await _context.Departments.FindAsync(id);
            return dept;
        }

        public async Task<DepartmentResponse> GetDepartmentByName(string name)
        {
            return await _context.Departments.ProjectTo<DepartmentResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Name.Contains(name));
        }

        public async Task<bool> SaveAllAsync()
        {
            if (await _context.SaveChangesAsync() > 0) return true;

            return false;

        }

        public async Task<IEnumerable<DepartmentDTO>> GetAllDepartmentAsync()
        {
            return await _context.Departments.ProjectTo<DepartmentDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }


    }
}
