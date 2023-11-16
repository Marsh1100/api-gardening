using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
namespace Application.Repository;
public class ClientRepository : GenericRepository<Client>, IClient
{
    private readonly GardeningContext _context;

    public ClientRepository(GardeningContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<(int totalRegistros, IEnumerable<Client> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Clients as IQueryable<Client>;
        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(p=>p.NameClient.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query 
                            .Skip((pageIndex-1)*pageSize)
                            .Take(pageSize)
                            .ToListAsync();
        return (totalRegistros, registros);
    }

    public async Task<Client> GetSpanishClients()
    {
        FormattableString sql = $"SELECT * FROM gardening.client WHERE client.country = 'Spain'";
        var clients = await _context.Clients.FromSql(sql).FirstOrDefaultAsync();
        return clients;
    }

   




}
