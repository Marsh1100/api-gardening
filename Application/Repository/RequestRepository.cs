using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
namespace Application.Repository;
public class RequestRepository : GenericRepository<Request>, IRequest
{
    private readonly GardeningContext _context;

    public RequestRepository(GardeningContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<(int totalRegistros, IEnumerable<Request> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Requests as IQueryable<Request>;
        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(p=>p.State.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query 
                            .Skip((pageIndex-1)*pageSize)
                            .Take(pageSize)
                            .ToListAsync();
        return (totalRegistros, registros);
    }

    

}
