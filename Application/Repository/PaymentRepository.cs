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
    //3
    public async Task<object> GetAveragePay2009()
    {
        var payments = await _context.Payments.ToListAsync();
        var average = payments
                        .Where(d=> d.PaymentDate.Year.Equals(2009))
                        .Average(a=> a.Total);
        return average;
    }
    

    //13
    public async Task<IEnumerable<Payment>> GetPayment2008()
    {
        FormattableString sql = $"SELECT * FROM payment WHERE paymentMethod = 'PayPal' AND DATE_FORMAT(paymentDate , '%Y') = 2008 ORDER BY total DESC";
        return await _context.Payments.FromSql(sql).ToListAsync();
    }
    //14
    public async Task<IEnumerable<object>> GetPaymentMethod()
    {
        var result = await _context.Payments
                            .Select(a=> a.PaymentMethod)
                            .Distinct()
                            .ToListAsync();
        return result;
    }

    //resume 16
    public async Task<IEnumerable<object>> GetPaymentByYear()
    {
        return await _context.Payments
                        .GroupBy(a=>a.PaymentDate.Year)
                        .Select(s=> new{
                            year = s.Key,
                            total_payment = s.Select(a=> a.Total).Sum()
                        }).ToListAsync();
    }



}
