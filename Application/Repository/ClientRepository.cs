using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

    //CE 8
    public async Task<IEnumerable<object>> GetClientsRequestWithoutPayments()
    {
        var clients = await _context.Clients.ToListAsync();
        var payments = await _context.Payments.ToListAsync();
        var requests =  await _context.Requests.ToListAsync();
        return  (from client in clients
                join request in requests on client.Id equals request.IdClient 
                join payment in payments on client.Id equals payment.IdClient into h
                from all in h.DefaultIfEmpty()
                where all?.IdClient == null
                select client).Distinct();
    
    }
    //resume 9
    public async Task<IEnumerable<object>> GetClientsDatePayments()
    {

        return await (from client in _context.Clients
                        join payment in _context.Payments on client.Id equals  payment.IdClient
                        select client)
                        .GroupBy(a=> a.Id)
                        .Select(s=> new {
                           Client =  s.Select(d=> d.NameContact).Distinct(),
                           FirstPayment = s.Select(o=> o.Payments.Min(u=>u.PaymentDate)).FirstOrDefault(),
                           SecondPayment = s.Select(o=> o.Payments.Max(u=>u.PaymentDate)).FirstOrDefault()
                        }).ToListAsync();
    }
    //sub 1
    public async Task<object> GetClientCreditlimit()
    {

        var maxCredit = await _context.Clients.MaxAsync(c=> c.CreditLimit);
        return  await _context.Clients
                        .Where(c=> c.CreditLimit == maxCredit)
                        .Select(s=> new { client = s.NameClient})
                        .ToListAsync();
    }

     //sub 2
    public async Task<object> GetClientCreditlimitGreaterPayments()
    {

        return  await (from client in _context.Clients
                         where client.CreditLimit > 
                               (from payment in _context.Payments
                                where payment.IdClient == client.Id
                                select (decimal?)payment.Total).Sum() 
                         select new { client_name  = client.NameClient}).ToListAsync();
    }

    //sub 8
    public async Task<object> GetClientCreditlimit2()
    {

        var maxCredit = await _context.Clients.MaxAsync(c=> c.CreditLimit);
        return  await _context.Clients
                        .Where(c=> c.CreditLimit == maxCredit)
                        .Select(s=> new { client = s.NameClient})
                        .ToListAsync();
    }


    //sub 11
    public async Task<IEnumerable<object>> GetClientsWithoutPayments2()
    {
        return  await _context.Clients
                        .Where(client=> 
                        !_context.Payments.Select(a=>a.IdClient)
                        .Contains(client.Id)).ToListAsync();
    }

    //sub 12
    public async Task<IEnumerable<object>> GetClientsWithPayments()
    {
        return  await _context.Clients
                        .Where(client=> 
                        _context.Payments.Select(a=>a.IdClient)
                        .Contains(client.Id)).ToListAsync();
    }

     //sub 18
    public async Task<IEnumerable<object>> GetClientsWithoutPayments3()
    {
        return  await _context.Clients
                        .Where(client=> 
                        !_context.Payments.Any(p=> p.IdClient == client.Id )).ToListAsync();
    }
     //sub 19
    public async Task<IEnumerable<object>> GetClientPayments()
    {
        return  await _context.Clients
                        .Where(client=> 
                        _context.Payments.Any(p=> p.IdClient == client.Id )).ToListAsync();
    }

    //var 1
    public async Task<IEnumerable<object>> GetClientQuantityPayments()
    {
        var clients = await _context.Clients.ToListAsync();
        var requests = await _context.Requests.ToListAsync();

        return  from client in clients
                join request in requests on client.Id equals request.IdClient into h
                from allrequest in h.DefaultIfEmpty()
                group new { client, allrequest }  by client.Id into newgroup
                select new {
                    client = newgroup.Select(a=> a.client.NameContact +" "+a.client.LastnameContact).FirstOrDefault(),
                    quantity_payments = newgroup.Count(s=> s.allrequest != null)
                };
                       
    }

    //var 2
    public async Task<IEnumerable<object>> GetClientsRequest2008()
    {
        var clients = await _context.Clients.ToListAsync();
        var requests = await _context.Requests.ToListAsync();

        return  (from client in clients
                join request in requests on client.Id equals request.IdClient 
                where request.RequestDate.Year.Equals(2008)
                select new {
                    client = client.NameClient
                }).Distinct();                
    }
    //var 3
    public async Task<IEnumerable<object>> GetClientsWithoutPayments4()
    {

        return  await (from client in _context.Clients
                join employee in _context.Employees on client.IdEmployee equals employee.Id
                join office in _context.Offices on employee.IdOffice equals office.Id
                where !_context.Payments.Any(p=> p.IdClient == client.Id )
                select new {
                    client = client.NameClient,
                    sale_representative = employee.Name+" "+employee.SecondSurname,
                    office_phone = office.Phone
                })
                .OrderBy(a=>a.client)
                .ToListAsync();             
    }

    //var 4
    public async Task<IEnumerable<object>> GetClientsWihtEmployeeAndOffice()
    {
        return  await (from client in _context.Clients
                join employee in _context.Employees on client.IdEmployee equals employee.Id
                join office in _context.Offices on employee.IdOffice equals office.Id
                select new {
                    id_client   = client.Id,
                    client = client.NameClient,
                    sale_representative = employee.Name+" "+employee.SecondSurname,
                    office_city = office.City
                }).OrderBy(a=>a.id_client).ToListAsync();            
    }
    //resume 2
    public async Task<object> GetTotalEmployeesByCountry()
    {
        return await _context.Clients.GroupBy(a=> a.Country)
                    .Select(s=> new {
                        country = s.Key,
                        quantity_clients = s.Count()
                    }).FirstAsync();  
    
    }

    //resume 5
    public async Task<object> GetTotalClientsMadrid()
    {
        return await _context.Clients.GroupBy(a=> a.City)
                    .Where(d=> d.Key == "Madrid")
                    .Select(s=> new {
                        city = s.Key,
                        quantity_clients = s.Count()
                    }).FirstAsync();  
    
    }
    //resume 6
    public async Task<object> GetTotalClientsM()
    {
        return await _context.Clients.GroupBy(a=> a.City)
                    .Where(d=> d.Key.StartsWith("M"))
                    .Select(s=> new {
                        city = s.Key,
                        quantity_clients = s.Count()
                    }).ToListAsync(); 
    }
    //resume 7
    public async Task<object> GetTotalclientsByEmployee()
    {
        return await (from client in _context.Clients
                        join employee in _context.Employees on client.IdEmployee equals employee.Id
                        group employee by employee.Id into newgroup
                        select new 
                        {
                            employee = newgroup.Select(d=> d.Name).FirstOrDefault(),
                            quantity_clients = newgroup.Select(a=> a.Clients).Count()
                        }).ToListAsync();
    }
}

