using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
namespace Application.Repository;
public class PaymentRepository : GenericRepository<Payment>, IPayment
{
    private readonly GardeningContext _context;

    public PaymentRepository(GardeningContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<(int totalRegistros, IEnumerable<Payment> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Payments as IQueryable<Payment>;
        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(p=>p.TransactionId.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query 
                            .Skip((pageIndex-1)*pageSize)
                            .Take(pageSize)
                            .ToListAsync();
        return (totalRegistros, registros);
    }

    

}
