using Domain.Entities;

namespace Domain.Interfaces;

public interface IProduct : IGenericRepository<Product> 
{ 
    
    Task<IEnumerable<Product>> GetProductsOrnamentales();
    Task<IEnumerable<Product>> GetProductsWithoutRequest();
    Task<IEnumerable<object>> GetProductsWithoutRequest2();

}
