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

    //CE7 
    public async Task<IEnumerable<object>> GetOfficesWithoutEmployee()
    {
        var employees = await _context.Employees.ToListAsync();
        var offices = await _context.Offices.ToListAsync();
        var clients =  await _context.Clients.ToListAsync();
        var requesdetails =await _context.Requestdetails.ToListAsync();
        var requests =await _context.Requests.ToListAsync();
        var products = await _context.Products.ToListAsync(); 
        var producttypes = await _context.Producttypes.ToListAsync();

        return  (from employee in employees
                join office in offices on employee.IdOffice equals office.Id 
                join client in clients on employee.Id equals client.IdEmployee
                join request in requests on client.Id equals request.IdClient
                join requesdetail in requesdetails on request.Id equals requesdetail.IdRequest
                join product in products on requesdetail.IdProduct equals product.Id
                join producttype in producttypes on product.IdProductType equals producttype.Id
                where producttype.Type != "Frutales"  
                select new {
                    office.Id,
                    office.OfficineCode,
                    office.City,
                    office.Country,
                    office.Region,
                    office.ZipCode,
                    office.Phone,
                    office.Address1,
                    office.Address2
                }).Distinct();
    
    }
}
