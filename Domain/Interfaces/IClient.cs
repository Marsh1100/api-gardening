using Domain.Entities;

namespace Domain.Interfaces;

public interface IClient : IGenericRepository<Client> 
{ 
    
    Task<Client> GetSpanishClients();
    Task<IEnumerable<Client>> GetClientsPay2008();
    Task<IEnumerable<Client>> GetClientsMadrid();

}
