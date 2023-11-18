using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
namespace Application.Repository;
public class ProductRepository : GenericRepository<Product>, IProduct
{
    private readonly GardeningContext _context;

    public ProductRepository(GardeningContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<(int totalRegistros, IEnumerable<Product> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Products as IQueryable<Product>;
        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(p=>p.Name.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query 
                            .Skip((pageIndex-1)*pageSize)
                            .Take(pageSize)
                            .ToListAsync();
        return (totalRegistros, registros);
    }

    //15
    public async Task<IEnumerable<Product>> GetProductsOrnamentales()
    {
        return await (from product in _context.Products
                                join producttype in _context.Producttypes on product.IdProductType equals producttype.Id
                                select product).OrderByDescending(a=> a.Stock).ToListAsync();
    
    }

    //CE 5
    public async Task<IEnumerable<Product>> GetProductsWithoutRequest()
    {
        var products = await _context.Products.ToListAsync();
        var requestdetails =  await _context.Requestdetails.ToListAsync();
        return  from product in products
                join requesdetail in requestdetails on product.Id equals requesdetail.IdProduct into h
                from all in h.DefaultIfEmpty()
                where all?.Id == null
                select product;
    }
    //CE 6
    public async Task<IEnumerable<object>> GetProductsWithoutRequest2()
    {
        var products = await _context.Products.ToListAsync();
        var producttypes = await _context.Producttypes.ToListAsync();
        var requestdetails =  await _context.Requestdetails.ToListAsync();
        return  (from product in products
                join requesdetail in requestdetails on product.Id equals requesdetail.IdProduct into h
                join producttype in producttypes on product.IdProductType equals producttype.Id
                from all in h.DefaultIfEmpty()
                where all?.Id == null
                select new {
                    product = product.Name,
                    type = producttype.Type,
                    producttype.DescriptionText,
                    producttype.Image

                });
    
    }

    //sub 13
    public async Task<IEnumerable<object>> GetProductsWithoutRequest3()
    {
        return  await _context.Products
                        .Where(product=> 
                        !_context.Requestdetails.Select(a=>a.IdProduct)
                        .Contains(product.Id)).ToListAsync();
    }
}
