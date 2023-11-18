using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
    //12
    public async Task<IEnumerable<Request>> GetRequestDelivered()
    {
        FormattableString sql = $"SELECT  * FROM request WHERE state = 'Entregado' AND DATE_FORMAT(requestDate, '%m') = 01";
        return await _context.Requests.FromSql(sql).ToListAsync();
    }
    //11
    public async Task<IEnumerable<Request>> GetRequestReject()
    {
        FormattableString sql = $"SELECT  * FROM request WHERE state = 'Rechazado' AND YEAR(requestDate) = 2009";
        return await _context.Requests.FromSql(sql).ToListAsync();
    }

    //10
    public async Task<IEnumerable<Request>> GetRequestEarly()
    {
        FormattableString sql = $"SELECT  * FROM request WHERE ADDDATE(expectedDate, INTERVAL -2 day ) >= deliveryDate";
        return await _context.Requests.FromSql(sql).ToListAsync();
    }

    //9.
    public async Task<IEnumerable<Request>> GetRequestLate()
    {
        FormattableString sql = $"SELECT  * FROM request WHERE deliveryDate > expectedDate";
        return await _context.Requests.FromSql(sql).ToListAsync();
   
    }

    //2
    public async Task<IEnumerable<object>> GetStates()
    {
        var states = await _context.Requests
                        .Select(a=> a.State)
                        .Distinct()
                        .ToListAsync();
        return states;
    }
    //resume 10
    public async Task<IEnumerable<object>> GetQuantityProducts()
    {
        var products = await _context.Products.ToListAsync();
        var requestdetails = await _context.Requestdetails.ToListAsync();
        var requests =  await _context.Requests.ToListAsync();

        return from request in requests
                join requestdetail in requestdetails on request.Id equals requestdetail.IdRequest
                join product in products on requestdetail.IdProduct equals product.Id
                group  requestdetail by requestdetail.IdRequest into newRequests
                select new {
                    Id_Request = newRequests.Key,
                    Quantity_products = newRequests.Select(a=>a.IdProduct).Count()
                };
    }

    //resume 11
    public async Task<IEnumerable<object>> GetSumProductsRequest()
    {
        var products = await _context.Products.ToListAsync();
        var requestdetails = await _context.Requestdetails.ToListAsync();
        var requests =  await _context.Requests.ToListAsync();

        return from request in requests
                join requestdetail in requestdetails on request.Id equals requestdetail.IdRequest
                join product in products on requestdetail.IdProduct equals product.Id
                group  requestdetail by requestdetail.IdRequest into newRequests
                select new {
                    Id_Request = newRequests.Key,
                    Total_products = newRequests.Select(a=>a.UnitPrice * a.Quantity).Sum()
                };
    }

    //resume 12
    public async Task<IEnumerable<object>> GetProducts20Sold()
    {
       
        return await (from request in _context.Requests
                join requestdetail in _context.Requestdetails on request.Id equals requestdetail.IdRequest
                join product in _context.Products on requestdetail.IdProduct equals product.Id
                group  requestdetail by requestdetail.IdProduct into newRequests
                select new {
                    Product_name = newRequests.Select(a=>a.IdProductNavigation.Name).FirstOrDefault(),
                    Total_products = newRequests.Select(a=>a.Quantity).Sum()
                })
                .OrderByDescending(r=>r.Total_products).Take(20).ToListAsync()
                ;
    }
    //resume 13
    public async Task<IEnumerable<object>> GetProductsCode20Sold()
    {

        return await (from request in _context.Requests
                join requestdetail in _context.Requestdetails on request.Id equals requestdetail.IdRequest
                join product in _context.Products on requestdetail.IdProduct equals product.Id
                group  requestdetail by requestdetail.IdProduct into newRequests
                select new {
                    Code_product = newRequests.Select(a=>a.IdProductNavigation.ProductCode).FirstOrDefault(),
                    Total_products = newRequests.Select(a=>a.Quantity).Sum()
                })
                .OrderByDescending(r=>r.Total_products).Take(20).ToListAsync()
                ;
    }

    //resume 14
    public async Task<IEnumerable<object>> GetProductsCode20StartOR()
    {
      
        return await (from request in _context.Requests
                join requestdetail in _context.Requestdetails on request.Id equals requestdetail.IdRequest
                join product in _context.Products on requestdetail.IdProduct equals product.Id
                where product.ProductCode.StartsWith("OR")
                group  requestdetail by requestdetail.IdProduct into newRequests
                select new {
                    Code_product = newRequests.Select(a=>a.IdProductNavigation.ProductCode).FirstOrDefault(),
                    Total_products = newRequests.Select(a=>a.Quantity).Sum()
                })
                .OrderByDescending(r=>r.Total_products).Take(20).ToListAsync();
    }
    //resume 15
    public async Task<IEnumerable<object>> GetProductsTotal3000()
    {
      
        return await (from request in _context.Requests
                join requestdetail in _context.Requestdetails on request.Id equals requestdetail.IdRequest
                join product in _context.Products on requestdetail.IdProduct equals product.Id
                group  requestdetail by requestdetail.IdProduct into newRequests
                select new {
                    Product_name = newRequests.Select(a=>a.IdProductNavigation.Name).FirstOrDefault(),
                    Total_sold = Math.Round((decimal)newRequests.Select(a=>a.UnitPrice * a.Quantity).Sum(),2),
                    Total_tax = Math.Round((decimal)(newRequests.Select(a=>a.UnitPrice * a.Quantity).Sum()*(decimal)1.21),2)

                })
                .Where(a=>a.Total_sold>3000).ToListAsync();
    }
}
