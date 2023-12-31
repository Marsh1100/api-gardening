using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
using API.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]

public class EmployeeController : ApiBaseController
{
    private readonly IUserService _userService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EmployeeController(IUserService userService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _userService = userService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get()
    {
        var results = await _unitOfWork.Employees.GetAllAsync();
        return _mapper.Map<List<EmployeeDto>>(results);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<EmployeeDto>>> GetPagination([FromQuery] Params p)
    {
        var results = await _unitOfWork.Employees.GetAllAsync(p.PageIndex, p.PageSize, p.Search);
        var resultsDto = _mapper.Map<List<EmployeeDto>>(results.registros);
        return  new Pager<EmployeeDto>(resultsDto,results.totalRegistros, p.PageIndex, p.PageSize, p.Search);
    }

    [HttpPost()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmployeeDto>> Post([FromBody] EmployeeDto dto)
    {
        var result = _mapper.Map<Employee>(dto);
        this._unitOfWork.Employees.Add(result);
        await _unitOfWork.SaveAsync();


        if(result == null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(Post), new{id=result.Id}, result);
    }


    [HttpPut()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<EmployeeDto>> put(EmployeeDto dto)
    {
        if(dto == null){ return NotFound(); }
        var result = this._mapper.Map<Employee>(dto);
        this._unitOfWork.Employees.Update(result);
        Console.WriteLine(await this._unitOfWork.SaveAsync());
        return Ok(result);
    }


    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _unitOfWork.Employees.GetByIdAsync(id);
        if(result == null)
        {
            return NotFound();
        }
        this._unitOfWork.Employees.Remove(result);
        await this._unitOfWork.SaveAsync();
        return NoContent();
    }

    //CI 6 Devuelve un listado que muestre el nombre de cada empleados, el nombre de su jefe y el nombre del jefe de sus jefe. 
    [HttpGet("employeesWithBoss")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetEmployeesWithBoss()
    {
        var result = await _unitOfWork.Employees.GetEmployeesWithBoss();
        return Ok(result);
    }

    //CE 3. Devuelve un listado que muestre solamente los empleados que no tienen un cliente asociado junto con los datos de la oficina donde trabajan.
    [HttpGet("employeesWithoutClients")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetEmployeesWithoutClients()
    {
        var result = await _unitOfWork.Employees.GetEmployeesWithoutClients();
        return Ok(result);
    }

    //CE 4.Devuelve un listado que muestre los empleados que no tienen una oficina asociada y los que no tienen un cliente asociado.
    [HttpGet("employeesWithoutClientsAndOffice")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetEmployeesWithoutClientsAndOffice()
    {
        var result = await _unitOfWork.Employees.GetEmployeesWithoutClientsAndOffice();
        return Ok(result);
    }

    //CE 9.Devuelve un listado con los datos de los empleados que no tienen clientes asociados y el nombre de su jefe asociado
    [HttpGet("employeesBossWithoutClients")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetEmployeesBossWithoutClients()
    {
        var result = await _unitOfWork.Employees.GetEmployeesBossWithoutClients();
        return Ok(result);
    }
    
     // sub 14. Devuelve el nombre, apellidos, puesto y teléfono de la oficina de aquellos empleados que no sean representante de ventas de ningún cliente.
    [HttpGet("employeesWithoutClients2")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetEmployeesWithoutClients2()
    {
        var result = await _unitOfWork.Employees.GetEmployeesWithoutClients2();
        return Ok(_mapper.Map<List<EmployeeOfficeDto>>(result));
    }

    //var 5. Devuelve el nombre, apellidos, puesto y teléfono de la oficina de aquellos empleados que no sean representante de ventas de ningún cliente.
    [HttpGet("employeesWithoutClients3")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetEmployeesWithoutClients3()
    {
        var result = await _unitOfWork.Employees.GetEmployeesWithoutClients3();
        return Ok(_mapper.Map<List<EmployeeOfficeDto>>(result));
    }

    //resume 1. ¿Cuántos empleados hay en la compañía?
    [HttpGet("totalEmployees")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetTotalEmployees()
    {
        var result = await _unitOfWork.Employees.GetTotalEmployees();
        return Ok(result);
    }
}