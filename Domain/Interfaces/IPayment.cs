using Domain.Entities;

namespace Domain.Interfaces;

public interface IPayment : IGenericRepository<Payment> 
{ 
    Task<IEnumerable<Payment>> GetPayment2008();
    Task<IEnumerable<object>> GetPaymentMethod();
    Task<IEnumerable<object>> GetPaymentByYear();
}
