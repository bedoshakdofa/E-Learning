using E_Learning.DTOs;
using E_Learning.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize(Roles ="Admin")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _deptRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _deptRepository = departmentRepository;
        }

        [HttpPost]
        public async Task<ActionResult> CraeteDepartment(DepartmentDTO departmentDTO)
        {
            var Department=await _deptRepository.GetDepartmentByName(departmentDTO.Name.ToLower());

            if (Department !=null) return BadRequest("this course is found before");

            await _deptRepository.AddDeptAsync(departmentDTO);

            if (await _deptRepository.SaveAllAsync()) return Ok("Department add successfully");

            return BadRequest("something went wrong");

        }

        [HttpGet("{DeptName}")]
        public async Task<ActionResult<DepartmentResponse>> GetDepartmentByName([FromRoute] string DeptName)
        {
            
            var dept=await _deptRepository.GetDepartmentByName(DeptName);

            if (dept == null) return BadRequest("can't find department with this id");

            return Ok(dept);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteDepartment([FromRoute] int id)
        {
            var dept = await _deptRepository.GetDeptByIdAsync(id);

            if (dept == null) return NotFound("this id is not found");

            _deptRepository.DeleteDeptAsync(dept);

            if (await _deptRepository.SaveAllAsync()) return Ok("Department Deleted successfully");

            return BadRequest("something went wrong");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult>UpdateDepartment([FromRoute]int id,[FromBody]string name)
        {
            var dept = await _deptRepository.GetDeptByIdAsync(id);

            dept.Name = name.ToLower();

            if(await _deptRepository.SaveAllAsync()) return Ok("Department updated successfully");

            return BadRequest("something went wrong");
        }


        [HttpGet("GetAll")]

        public async Task<ActionResult<IEnumerable<DepartmentDTO>>> GetAllDepartment()
        {
            var dept= await _deptRepository.GetAllDepartmentAsync();

            if (dept.Count()==0) return NotFound("there is not any department");

            return Ok(dept);
        }
    }
}
