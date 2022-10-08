using AutoMapper;
using JAPManagementSystem.Data;
using JAPManagementSystem.DTOs.StudentDto;
using JAPManagementSystem.DTOs.User;
using JAPManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JAPManagementSystem.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(IMapper mapper, DataContext context, IConfiguration configuration)
        {
            _mapper = mapper;
            _context = context;
            _configuration = configuration;
        }
        public async Task<ServiceResponse<GetUserDto>> Login(UserLoginDto user)
        {
            ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto>();
            try
            {
                var fetchedUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName.ToLower().Equals(user.UserName.ToLower()));
                if (fetchedUser == null || !VerifyHashedPassword(user.Password, fetchedUser.PasswordHash, fetchedUser.PasswordSalt))
                {
                    throw new Exception("Wrong credentials");
                }
                else
                {
                    var getUserDto = _mapper.Map<GetUserDto>(fetchedUser);
                    getUserDto.Token = CreateToken(fetchedUser);
                    response.Data = getUserDto;
                    response.Message = "User " + user.UserName + " has successfully logged in.";
                }
            }catch(Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
        }

        string RandomCreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        bool VerifyHashedPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var hashedPassword = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return hashedPassword.SequenceEqual(passwordHash);
            }
        }

        public StudentUserCreatedDto CreateUser(AddStudentDto newStudent)
        {
            string password = RandomCreatePassword(10);
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            return new StudentUserCreatedDto
            {
                FirstName = newStudent.FirstName,
                LastName = newStudent.LastName,
                Email = newStudent.Email,
                Role = UserRole.Student,
                UserName = newStudent.FirstName.ToLower() + newStudent.LastName.ToLower(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Password = password
            };
        }
        private string CreateToken(User user)
        {
            string role = String.Empty;
            if (user.Role.Equals(UserRole.Admin))
            {
                role = "Admin";
            }else if (user.Role.Equals(UserRole.Student))
            {
                role = "Student";
            }
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, role)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}
