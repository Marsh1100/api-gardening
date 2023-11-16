using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
namespace Application.Repository;
public class OfficeRepository : GenericRepository<Office>, IOffice
{
    private readonly GardeningContext _context;

    public OfficeRepository(GardeningContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<(int totalRegistros, IEnumerable<Office> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Offices as IQueryable<Office>;
        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(p=>p.OfficineCode.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query 
                            .Skip((pageIndex-1)*pageSize)
                            .Take(pageSize)
                            .ToListAsync();
        return (totalRegistros, registros);
    }

    

}
