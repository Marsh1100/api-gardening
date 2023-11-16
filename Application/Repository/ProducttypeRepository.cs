using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
namespace Application.Repository;
public class ProducttypeRepository : GenericRepository<Producttype>, IProducttype
{
    private readonly GardeningContext _context;

    public ProducttypeRepository(GardeningContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<(int totalRegistros, IEnumerable<Producttype> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Producttypes as IQueryable<Producttype>;
        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(p=>p.Type.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query 
                            .Skip((pageIndex-1)*pageSize)
                            .Take(pageSize)
                            .ToListAsync();
        return (totalRegistros, registros);
    }

    

}
