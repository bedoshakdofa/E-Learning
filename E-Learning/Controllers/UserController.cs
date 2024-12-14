using E_Learning.Data;
using E_Learning.Extenstions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DbContextApp _context;
        public UserController(DbContextApp context)
        {
            _context = context;
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("GetAll")]
        public async Task<ActionResult> GatAllUser()
        {
            var userID = User.GetUserId();
            var users = await _context.Users.Where(x => x.SSN != userID).ToListAsync();

            if (users.Count == 0) return NotFound("not user found");

            return Ok(users);
        }
        [Authorize]
        [HttpDelete("DeleteMe")]
        public async Task<ActionResult> DeleteMe()
        {
            var userID = User.GetUserId();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.SSN == userID);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [Authorize]
        [HttpGet("Me")]
        public async Task<ActionResult> GetMe()
        {
            var userID = User.GetUserId();

            return Ok(await _context.Users.FindAsync(userID));
        }
        [Authorize(Roles ="Admin")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult>DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null) return NotFound("this user is not found");

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return Ok("User deleted Successfully");
        }
    }
}
