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
    Task<IEnumerable<object>> GetClientsDatePayments();
    Task<IEnumerable<object>> GetClientsWithoutPayments2();
    Task<IEnumerable<object>> GetClientsWithPayments();
    Task<IEnumerable<object>> GetClientsWithoutPayments3();
    Task<IEnumerable<object>> GetClientPayments();
    Task<IEnumerable<object>> GetClientQuantityPayments();
    Task<IEnumerable<object>> GetClientsRequest2008();
    Task<IEnumerable<object>> GetClientsWithoutPayments4();
    Task<IEnumerable<object>> GetClientsWihtEmployeeAndOffice();



}
