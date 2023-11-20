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
    Task<object> GetTotalEmployeesByCountry();
    Task<object> GetTotalClientsMadrid();
    Task<object> GetTotalclientsByEmployee();
    Task<object> GetTotalClientsM();
    Task<object> GetClientCreditlimit();
    Task<object> GetClientCreditlimitGreaterPayments();
    Task<object> GetClientCreditlimit2();

    Task<IEnumerable<object>> GetclientsWithoutPaymentsAndSellerOffice();
    Task<IEnumerable<object>> GetclientsPaymentsAndSellerOffice();
    Task<IEnumerable<object>> GetclientsWithoutPaymentsAndSeller();
    Task<IEnumerable<object>> GetclientsAndSeller();
    Task<IEnumerable<object>> GetclientsPaymentsAndSeller();
    Task<IEnumerable<object>> GetRequestLate();
    Task<IEnumerable<object>> GetProducttypeByClient();
    Task<object> GetQuantityWithoutSeller();




}
