using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
namespace Application.Repository;
public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly GardeningContext _context;

    public UserRepository(GardeningContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<(int totalRegistros, IEnumerable<User> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Users as IQueryable<User>;
        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(p=>p.IdenNumber.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query 
                            .Skip((pageIndex-1)*pageSize)
                            .Take(pageSize)
                            .ToListAsync();
        return (totalRegistros, registros);
    }

    public async Task<User> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _context.Users
            .Include(u => u.Roles)
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken));
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(u => u.Roles)
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.UserName.ToLower() == username.ToLower());
    }

    public async Task<int> GetIDUserAsync(string username)
    {
        var user = await _context.Users
                         .Include(u => u.Id)
                         .FirstOrDefaultAsync(u => u.UserName.ToLower() == username.ToLower());
        return  user.Id;

    
    }
    public async Task<IEnumerable<User>> GetAllRolesAsync()
    {
    var users = await _context.Users
        .Select(u => new User
        {
            Id = u.Id,
            UserName = u.UserName,
            Roles = u.Roles.FirstOrDefault() != null ? new List<Rol> { u.Roles.First() } : new List<Rol>()
        })
        .ToListAsync();
    return users;
    }

    public override async Task<IEnumerable<User>> GetAllAsync()
    {
        var users = await _context.Users
                    .Include(u => u.Roles)
                    .Include(u=> u.UsersRoles)
                    .ToListAsync();
        return users;
    }

}
