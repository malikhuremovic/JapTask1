using AutoMapper;
using JAPManagement.Core.DTOs.StudentDTOs;
using JAPManagement.Core.DTOs.User;
using JAPManagement.Core.Interfaces;
using JAPManagement.Core.Models.Response;
using JAPManagement.Core.Models.StudentModel;
using JAPManagement.Core.Models.UserModel;
using JAPManagement.Database.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JAPManagement.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _mailService;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthService(IMapper mapper, DataContext context, IConfiguration configuration, IEmailService mailService, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _mapper = mapper;
            _context = context;
            _configuration = configuration;
            _mailService = mailService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ServiceResponse<GetUserDto>> RegisterStudentUser(Student student, string password)
        {
            ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto>();
            try
            {
                var result = await _userManager.CreateAsync(student, password);
                StringBuilder stringBuilder = new StringBuilder();
                if (!result.Succeeded)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        stringBuilder.Append(error.Description);
                        Console.WriteLine(error.Description);
                    }
                    throw new Exception(stringBuilder.ToString());
                }
                response.Data = _mapper.Map<GetUserDto>(student);
                response.Message = "User successfully created";
            }
            catch (Exception exc)
            {
                response.Success = false;
                response.Message = exc.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> RegisterAdminUser(AddAdminDto admin)
        {
            ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto>();
            try
            {
                var adminUser = _mapper.Map<Admin>(admin);
                adminUser.FirstName = admin.FirstName.Trim();
                adminUser.LastName = admin.LastName.Trim();
                adminUser.Role = UserRole.Admin;
                adminUser.UserName = admin.FirstName.Trim().ToLower() + admin.LastName.Trim().ToLower();
                var password = RandomCreatePassword(10);
                var result = await _userManager.CreateAsync(adminUser, password);
                StringBuilder stringBuilder = new StringBuilder();
                if (!result.Succeeded)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        stringBuilder.Append(error.Description);
                        Console.WriteLine(error.Description);
                    }
                    throw new Exception(stringBuilder.ToString());
                }
                var adminCreated = _mapper.Map<AdminUserCreatedDto>(adminUser);
                adminCreated.Password = password;
                _mailService.SendConfirmationEmail(adminCreated);
                response.Message = "Admin successfully added.";
                response.Data = _mapper.Map<GetUserDto>(adminUser);
            }
            catch (Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> Login(UserLoginDto user)
        {
            ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto>();
            try
            {
                var fetchedUser = await _userManager.FindByNameAsync(user.UserName);
                if (fetchedUser == null)
                {
                    throw new Exception("Wrong credentials");
                }
                else
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(fetchedUser, user.Password, false);
                    if (!result.Succeeded)
                    {
                        throw new Exception("Wrong credentials");
                    }
                    var getUserDto = _mapper.Map<GetUserDto>(fetchedUser);
                    getUserDto.Token = CreateToken((User)fetchedUser);
                    response.Data = getUserDto;
                    response.Message = "User " + user.UserName + " has successfully logged in.";
                }
            }
            catch (Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
        }

        public StudentUserCreatedDto CreateStudentUser(AddStudentDto newStudent)
        {
            string password = RandomCreatePassword(10);
            return new StudentUserCreatedDto
            {
                FirstName = newStudent.FirstName.Trim(),
                LastName = newStudent.LastName.Trim(),
                Email = newStudent.Email.Trim(),
                Role = UserRole.Student,
                UserName = newStudent.FirstName.Trim().ToLower() + newStudent.LastName.Trim().ToLower(),
                Password = password.Trim()
            };
        }

        public async Task<ServiceResponse<GetUserDto>> GetUserByToken(string token)
        {
            ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto>();
            try
            {
                var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var id = decodedToken.Claims
                    .Where(claim => claim.Type
                    .Equals("nameid"))
                    .Select(claim => claim.Value)
                    .SingleOrDefault();
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id
                    .Equals(id));
                response.Data = _mapper.Map<GetUserDto>(user);
                response.Message = "You have fetched user " + user.UserName;
            }
            catch (Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
        }

        private string CreateToken(User user)
        {
            string role = string.Empty;
            if (user.Role.Equals(UserRole.Admin))
            {
                role = "Admin";
            }
            else if (user.Role.Equals(UserRole.Student))
            {
                role = "Student";
            }
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, role)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_configuration.GetSection("JWTSettings:SecurityKey").Value));

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(int.Parse(_configuration.GetSection("JWTSettings:ExpiryInMinutes").Value)),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
        string RandomCreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890@__@";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}
