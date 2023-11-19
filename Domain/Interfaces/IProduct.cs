using Domain.Entities;

namespace Domain.Interfaces;

public interface IProduct : IGenericRepository<Product> 
{ 
    
    Task<IEnumerable<Product>> GetProductsOrnamentales();
    Task<IEnumerable<Product>> GetProductsWithoutRequest();
    Task<IEnumerable<object>> GetProductsWithoutRequest2();
    Task<IEnumerable<object>> GetProductsWithoutRequest3();
    Task<IEnumerable<object>> GetMoreExpensivePrice();
    Task<IEnumerable<object>> GetMostSold();
    Task<IEnumerable<object>> GetMoreExpensivePrice2();



}
