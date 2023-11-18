using Domain.Entities;

namespace Domain.Interfaces;

public interface IRequest : IGenericRepository<Request> 
{ 
    Task<IEnumerable<object>> GetStates(); 
    Task<IEnumerable<Request>> GetRequestLate();
    Task<IEnumerable<Request>> GetRequestEarly();
    Task<IEnumerable<Request>> GetRequestReject();
    Task<IEnumerable<Request>> GetRequestDelivered();

    Task<IEnumerable<object>> GetQuantityProducts();
    Task<IEnumerable<object>> GetSumProductsRequest();
    Task<IEnumerable<object>> GetProducts20Sold();
    Task<IEnumerable<object>> GetProductsCode20Sold();
    Task<IEnumerable<object>> GetProductsCode20StartOR();
    Task<IEnumerable<object>> GetProductsTotal3000();
    Task<object> GetRequestByState();

}
