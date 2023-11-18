using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
namespace Application.Repository;
public class EmployeeRepository : GenericRepository<Employee>, IEmployee
{
    private readonly GardeningContext _context;

    public EmployeeRepository(GardeningContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<(int totalRegistros, IEnumerable<Employee> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Employees as IQueryable<Employee>;
        if(!string.IsNullOrEmpty(search))
        {
            query = query.Where(p=>p.Name.ToLower().Contains(search));
        }
        var totalRegistros = await query.CountAsync();
        var registros = await query 
                            .Skip((pageIndex-1)*pageSize)
                            .Take(pageSize)
                            .ToListAsync();
        return (totalRegistros, registros);
    }
    
    //CE 9

    public async Task<IEnumerable<object>> GetEmployeesBossWithoutClients()
    {
        var employees = await _context.Employees.ToListAsync();
        
        return from emp in employees
                join empBoss in employees on emp.IdBoss equals empBoss.Id into h
                from all in h.DefaultIfEmpty()
                select new 
                {
                    empleado = emp.Name+" "+emp.FirstSurname+" "+emp.SecondSurname,
                    jefe = all?.Name
                };


    }

    //ce 3

    public async Task<IEnumerable<object>> GetEmployeesWithoutClients()
    {
        var employees = await _context.Employees.ToListAsync();
        var offices = await _context.Offices.ToListAsync();
        var clients = await _context.Clients.ToListAsync();
        return from employee in employees
                            join client in clients on employee.Id equals client.IdEmployee into h
                            join office in offices on employee.IdOffice equals office.Id
                            from all in h.DefaultIfEmpty()
                            where all?.Id == null
                            select new {
                                employee.Id,
                                Employee = employee.Name+" "+employee.FirstSurname+" "+employee.SecondSurname,
                                office.OfficineCode,
                                office.City,
                                office.Region,
                                office.ZipCode,
                                office.Phone,
                                office.Address1,
                                office.Address2
                            };
    }

    //CE 4
    public async Task<IEnumerable<object>> GetEmployeesWithoutClientsAndOffice()
    {
        var employees = await _context.Employees.ToListAsync();
        var offices = await _context.Offices.ToListAsync();
        var clients = await _context.Clients.ToListAsync();
        return from employee in employees
                            join client in clients on employee.Id equals client.IdEmployee into h
                            join office in offices on employee.IdOffice equals office.Id into h2
                            from all in h.DefaultIfEmpty()
                            from all2 in h2.DefaultIfEmpty()
                            where all?.Id == null && all2?.Id == null
                            select new {
                                employee.Id,
                                Employee = employee.Name+" "+employee.FirstSurname+" "+employee.SecondSurname,
                                
                            };
    }

    //sub 14
    public async Task<IEnumerable<Employee>> GetEmployeesWithoutClients2()
    {
       return  await _context.Employees
                        .Include(s=>s.IdOfficeNavigation)
                        .Where(emp=> 
                        !_context.Clients.Select(a=>a.IdEmployee)
                        .Contains(emp.Id)).ToListAsync();
    }
}
