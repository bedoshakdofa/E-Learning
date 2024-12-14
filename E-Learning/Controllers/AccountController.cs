using E_Learning.Data;
using E_Learning.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System;
using System.Text;
using E_Learning.Data.Model;
using E_Learning.Interfaces;

namespace E_Learning.Controllers
{
    [ApiController]
    [Route("/api/[Controller]")]
    public class AccountController:ControllerBase
    {
        private readonly DbContextApp _context;

        private readonly ITokenServices _Jwt;

        private readonly ILogger<AccountController> _logger;
        public AccountController(DbContextApp context,ITokenServices Jwt, ILogger<AccountController> logger)
        {
            _context = context;
            _Jwt = Jwt;
            _logger = logger;
        }

        [HttpPost("sginup")]
        public async Task<ActionResult> SginUp([FromBody]RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            if (await CheckUserExist(registerDTO.SSN)) return BadRequest("this user is already sginup");

            using var hmac = new HMACSHA512();

            var user = new User
            {
                SSN = registerDTO.SSN,
                FirstName = registerDTO.FirstName.ToLower(),
                LastName = registerDTO.LastName.ToLower(),
                Email = registerDTO.Email,
                password = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.password)),
                passwordSalt = hmac.Key,
                Dept_id = registerDTO.Dept_id,
                phone=registerDTO.phone,
                Role = registerDTO.Role,

            };
            if (user.Role == "Admin") user.Dept_id = null;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok("user registred successfully");
        }

        [HttpPost("login")]

        public async Task<ActionResult<NewUserDTO>> Login([FromBody] LoginDTO loginDto) {

            var user= await _context.Users.SingleOrDefaultAsync(x=>x.Email == loginDto.Email);

            if (user == null) return Unauthorized("invaild Email or password");

            using var hmac = new HMACSHA512(user.passwordSalt);

            var computedHased = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHased.Length; i++) 
            {
                if (computedHased[i] != user.password[i])
                    return Unauthorized("invaild email or password");
            }

            var Token = _Jwt.GetToken(user);

            return new NewUserDTO
            {
                Email = loginDto.Email,
                Token = Token
            };
        }


        private async Task<bool> CheckUserExist(string SSN)
        {
            return await _context.Users.AnyAsync(x=>x.SSN == SSN);
        }
    }
}
