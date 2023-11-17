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

    //3
    public async Task<IEnumerable<Client>> GetClientsPay2008()
    {
        var clients = await (from client in _context.Clients
                            join payment in _context.Payments on client.Id equals payment.IdClient
                            where payment.PaymentDate.Year == 2008
                            select client).Distinct().ToListAsync();
        return clients;
    }

    //1
    public async Task<Client> GetSpanishClients()
    {
        FormattableString sql = $"SELECT * FROM gardening.client WHERE client.country = 'Spain'";
        var clients = await _context.Clients.FromSql(sql).FirstOrDefaultAsync();
        return clients;
    }

    //16
    public async Task<IEnumerable<Client>> GetClientsMadrid()
    {
        FormattableString sql = $"SELECT * FROM client WHERE city = 'Madrid' AND (idEmployee= 11 OR idEmployee = 30)";
        return await _context.Clients.FromSql(sql).ToListAsync();
        
    }

    //CE 1
    public async Task<IEnumerable<Client>> GetClientsWithoutPayments()
    {
        FormattableString sql = $"SELECT client.id, client.nameClient, client.nameContact, client.lastnameContact, client.phoneNumber, client.fax, client.address1, client.address2, client.city, client.region, client.country, client.zipCode, client.idEmployee, client.creditLimit FROM client LEFT JOIN payment ON client.id = payment.idClient WHERE payment.idClient IS NULL";
        return await _context.Clients.FromSql(sql).ToListAsync();
        
    }

    //CE 2
    public async Task<IEnumerable<Client>> GetClientsWithoutPaymentsANDrequest()
    {
        FormattableString sql = $"SELECT client.id, client.nameClient, client.nameContact, client.lastnameContact, client.phoneNumber, client.fax,  client.address1, client.address2, client.city, client.region, client.country, client.zipCode, client.idEmployee, client.creditLimit FROM client LEFT JOIN payment ON client.id = payment.idClient LEFT JOIN request ON client.id = request.idClient WHERE payment.idClient IS NULL AND request.idClient IS NULL";
        return await _context.Clients.FromSql(sql).ToListAsync();
        
    }

   




}
