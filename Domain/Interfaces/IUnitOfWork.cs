using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces;

public interface IUnitOfWork
{
    IRolRepository Roles { get; }
    IUserRepository Users { get; }

    IClient Clients { get; }
    IEmployee Employees { get; }
    IOffice Offices {   get; }
    IPayment Payments   { get; }
    IProduct Products { get; }
    IProducttype Producttypes { get; }
    IRequest Requests { get; }
    IRequestdetail Requestdetails { get; }
    Task<int> SaveAsync();
}
