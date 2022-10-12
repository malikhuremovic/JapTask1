using AutoMapper;
using JAPManagementSystem.Data;
using JAPManagementSystem.DTOs.StudentDto;
using JAPManagementSystem.DTOs.User;
using JAPManagementSystem.Models;
using JAPManagementSystem.Models.StudentModel;
using JAPManagementSystem.Models.UserModel;
using JAPManagementSystem.Services.EmailService;
using MailKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            catch(Exception exc)
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
                adminUser.Role = UserRole.Admin;
                adminUser.UserName = admin.FirstName.ToLower() + admin.LastName.ToLower();
                var password = "mojasifra01";
                var result = await _userManager.CreateAsync(adminUser, password);
                var adminCreated = _mapper.Map<AdminUserCreatedDto>(adminUser);
                adminCreated.Password = password;
                _mailService.SendConfirmationEmail(adminCreated);
                response.Message = "Admin successfully added.";
                response.Data = _mapper.Map<GetUserDto>(adminUser);
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
            }catch(Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
        }

        public StudentUserCreatedDto CreateStudentUser(AddStudentDto newStudent)
        {
            string password = RandomCreatePassword(30);
            return new StudentUserCreatedDto
            {
                FirstName = newStudent.FirstName,
                LastName = newStudent.LastName,
                Email = newStudent.Email,
                Role = UserRole.Student,
                UserName = newStudent.FirstName.ToLower() + newStudent.LastName.ToLower(),
                Password = password
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
            }catch(Exception exc)
            {
                response.Message = exc.Message;
                response.Success = false;
            }
            return response;
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
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, role)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("JWTSettings:SecurityKey").Value));

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
