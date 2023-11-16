using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repository;
using Domain.Interfaces;
using Persistence;
using Persistence.Data;

namespace Application.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly GardeningContext _context;
    private IRolRepository _roles;
    private IUserRepository _users;
    private IClient _clients;
    private IEmployee _employees;
    private IOffice _offices;
    private IPayment _payments;
    private IProduct _products;
    private IProducttype _producttypes;
    private IRequest _request;
    private IRequestdetail _requestdetails;
  
    public UnitOfWork(GardeningContext context)
    {
        _context = context;
    }
    public IClient Clients
    {
        get
        {
            if (_clients == null)
            {
                _clients = new ClientRepository(_context);
            }
            return _clients;
        }
    }
    public IEmployee Employees
    {
        get
        {
            if (_employees == null)
            {
                _employees = new EmployeeRepository(_context);
            }
            return _employees;
        }
    }
    public IOffice Offices
    {
        get
        {
            if (_offices == null)
            {
                _offices = new OfficeRepository(_context);
            }
            return _offices;
        }
    }
    public IPayment Payments
    {
        get
        {
            if (_payments == null)
            {
                _payments = new PaymentRepository(_context);
            }
            return _payments;
        }
    }
    public IProduct Products
    {
        get
        {
            if (_products == null)
            {
                _products = new ProductRepository(_context);
            }
            return _products;
        }
    }
    public IProducttype Producttypes
    {
        get
        {
            if (_producttypes == null)
            {
                _producttypes = new ProducttypeRepository(_context);
            }
            return _producttypes;
        }
    }
    public IRequest Requests
    {
        get
        {
            if (_request == null)
            {
                _request = new RequestRepository(_context);
            }
            return _request;
        }
    }
    public IRequestdetail Requestdetails
    {
        get
        {
            if (_requestdetails == null)
            {
                _requestdetails = new RequestdetailRepository(_context);
            }
            return _requestdetails;
        }
    }
    public IRolRepository Roles
    {
        get
        {
            if (_roles == null)
            {
                _roles = new RolRepository(_context);
            }
            return _roles;
        }
    }

    public IUserRepository Users
    {
        get
        {
            if (_users == null)
            {
                _users = new UserRepository(_context);
            }
            return _users;
        }
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}