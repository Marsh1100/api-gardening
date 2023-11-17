using Domain.Entities;

namespace Domain.Interfaces;

public interface IRequest : IGenericRepository<Request> 
{ 
    Task<IEnumerable<object>> GetStates(); 
    Task<IEnumerable<Request>> GetRequestLate();
    Task<IEnumerable<Request>> GetRequestEarly();
    Task<IEnumerable<Request>> GetRequestReject();
    Task<IEnumerable<Request>> GetRequestDelivered();
}
