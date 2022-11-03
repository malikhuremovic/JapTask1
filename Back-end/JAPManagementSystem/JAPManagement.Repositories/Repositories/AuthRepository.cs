using AutoMapper;
using JAPManagement.Core.Interfaces.Repositories;
using JAPManagement.Core.Models.ProgramModel;
using JAPManagement.Core.Models.UserModel;
using JAPManagement.Database.Data;
using Microsoft.EntityFrameworkCore;

namespace JAPManagement.Repositories.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AuthRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<User> Add(User obj)
        {
            _context.Users.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
        public async Task<User> Update(User obj)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id.Equals(obj.Id));
            user = _mapper.Map<User>(obj);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Delete(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(item => item.Id.Equals(id));
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return user;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Id.Equals(id));
        }
    }
}
