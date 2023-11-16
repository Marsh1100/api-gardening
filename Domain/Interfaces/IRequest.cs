using Domain.Entities;

namespace Domain.Interfaces;

public interface IRequest : IGenericRepository<Request> 
{ 
    Task<IEnumerable<object>> GetStates(); 

}
