using Domain.Entities;

namespace Domain.Interfaces;

public interface IEmployee : IGenericRepository<Employee> 
{ 
    Task<IEnumerable<object>> GetEmployeesWithoutClientsAndOffice();

    Task<IEnumerable<object>> GetEmployeesWithoutClients();
    Task<IEnumerable<object>> GetEmployeesBossWithoutClients();
    Task<IEnumerable<Employee>> GetEmployeesWithoutClients2();

}
