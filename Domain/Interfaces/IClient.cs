using Domain.Entities;

namespace Domain.Interfaces;

public interface IClient : IGenericRepository<Client> 
{ 
    
    Task<Client> GetSpanishClients();
    Task<IEnumerable<Client>> GetClientsPay2008();
    Task<IEnumerable<Client>> GetClientsMadrid();
    Task<IEnumerable<Client>> GetClientsWithoutPayments();
    Task<IEnumerable<Client>> GetClientsWithoutPaymentsANDrequest();
    Task<IEnumerable<object>> GetClientsRequestWithoutPayments();
}
