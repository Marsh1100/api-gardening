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

public class ClientController : ApiBaseController
{
    private readonly IUserService _userService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ClientController(IUserService userService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _userService = userService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<ClientDto>>> Get()
    {
        var results = await _unitOfWork.Clients.GetAllAsync();
        return _mapper.Map<List<ClientDto>>(results);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ClientDto>>> GetPagination([FromQuery] Params p)
    {
        var results = await _unitOfWork.Clients.GetAllAsync(p.PageIndex, p.PageSize, p.Search);
        var resultsDto = _mapper.Map<List<ClientDto>>(results.registros);
        return  new Pager<ClientDto>(resultsDto,results.totalRegistros, p.PageIndex, p.PageSize, p.Search);
    }

    [HttpPost()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClientDto>> Post([FromBody] ClientDto dto)
    {
        var result = _mapper.Map<Client>(dto);
        this._unitOfWork.Clients.Add(result);
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

    public async Task<ActionResult<ClientDto>> put(ClientDto dto)
    {
        if(dto == null){ return NotFound(); }
        var result = this._mapper.Map<Client>(dto);
        this._unitOfWork.Clients.Update(result);
        Console.WriteLine(await this._unitOfWork.SaveAsync());
        return Ok(result);
    }


    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]


    public async Task<IActionResult> Delete(int id)
    {
        var result = await _unitOfWork.Users.GetByIdAsync(id);
        if(result == null)
        {
            return NotFound();
        }
        this._unitOfWork.Users.Remove(result);
        await this._unitOfWork.SaveAsync();
        return NoContent();
    }

    //1.Devuelve un listado con el nombre de los todos los clientes españoles.
    [HttpGet("spanishClients")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetSpanishClients()
    {
        var result = await _unitOfWork.Clients.GetSpanishClients();
        return Ok(_mapper.Map<ClientDto>(result));
    }


    //3.Devuelve un listado con el código de cliente de aquellos clientes que realizaron algún pago en 2008. Tenga en cuenta que deberá eliminar aquellos códigos de cliente que aparezcan repetidos.
    [HttpGet("clientsPay2008")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetClientsPay2008()
    {
        var result = await _unitOfWork.Clients.GetClientsPay2008();
        return Ok(_mapper.Map<IEnumerable<ClientIdDto>>(result));
    }

    //16.Devuelve un listado con todos los clientes que sean de la ciudad de Madrid y cuyo representante de ventas tenga el código de empleado 11 o 30
    [HttpGet("clientsMadrid")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetClientsMadrid()
    {
        var result = await _unitOfWork.Clients.GetClientsMadrid();
        return Ok(_mapper.Map<IEnumerable<ClientDto>>(result));
    }

    // CE 1.Devuelve un listado que muestre solamente los clientes que no han realizado ningún pago.
    [HttpGet("clientsWithoutPayments")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetClientsWithoutPayments()
    {
        var result = await _unitOfWork.Clients.GetClientsWithoutPayments();
        return Ok(_mapper.Map<IEnumerable<ClientDto>>(result));
    }

    //CE 2. Devuelve un listado que muestre los clientes que no han realizado ningún pago y los que no han realizado ningún pedido.
    [HttpGet("clientsWithoutPaymentsANDrequest")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetClientsWithoutPaymentsANDrequest()
    {
        var result = await _unitOfWork.Clients.GetClientsWithoutPaymentsANDrequest();
        return Ok(_mapper.Map<IEnumerable<ClientDto>>(result));
    }
     //CE 8.Devuelve un listado con los clientes que han realizado algún pedido pero no han realizado ningún pago.
    [HttpGet("clientsRequestWithoutPayments")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetClientsRequestWithoutPayments()
    {
        var result = await _unitOfWork.Clients.GetClientsRequestWithoutPayments();
        return Ok(_mapper.Map<IEnumerable<ClientDto>>(result));
    }

    //Resume 9.Calcula la fecha del primer y último pago realizado por cada uno de los clientes. El listado deberá mostrar el nombre y los apellidos de cada cliente.
    [HttpGet("clientsPaymentsDate")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetClientsDatePayments()
    {
        var result = await _unitOfWork.Clients.GetClientsDatePayments();
        return Ok(result);
    }

    // sub 11. Devuelve un listado que muestre solamente los clientes que no han realizado ningún pago.
    [HttpGet("clientsWithoutPayments2")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetClientsWithoutPayments2()
    {
        var result = await _unitOfWork.Clients.GetClientsWithoutPayments2();
        return Ok(_mapper.Map<IEnumerable<Client2Dto>>(result));
    }
    // sub 12. Devuelve un listado que muestre solamente los clientes que no han realizado ningún pago.Devuelve un listado que muestre solamente los clientes que sí han realizado algún pago.
    [HttpGet("clientsWithPayments")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetClientsWithPayments()
    {
        var result = await _unitOfWork.Clients.GetClientsWithPayments();
        return Ok(_mapper.Map<IEnumerable<Client2Dto>>(result));
    }

     // sub 18. Devuelve un listado que muestre solamente los clientes que no han realizado ningún pago..
    [HttpGet("clientsWithoutPayments3")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetClientsWithoutPayments3()
    {
        var result = await _unitOfWork.Clients.GetClientsWithoutPayments3();
        return Ok(_mapper.Map<IEnumerable<Client2Dto>>(result));
    }

    // sub 19. Devuelve un listado que muestre solamente los clientes que sí han realizado algún pago
    [HttpGet("clientPayments")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetClientPayments()
    {
        var result = await _unitOfWork.Clients.GetClientPayments();
        return Ok(_mapper.Map<IEnumerable<Client2Dto>>(result));
    }
    
    // var 1.Devuelve el listado de clientes indicando el nombre del cliente y cuántos pedidos ha realizado. Tenga en cuenta que pueden existir clientes que no han realizado ningún pedido.
    [HttpGet("clientQuantityPayments")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetClientQuantityPayments()
    {
        var result = await _unitOfWork.Clients.GetClientQuantityPayments();
        return Ok(result);
    }

    //var 2 . Devuelve el nombre de los clientes que hayan hecho pedidos en 2008 ordenados alfabéticamente de menor a mayor
     [HttpGet("clientsRequest2008")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetClientsRequest2008()
    {
        var result = await _unitOfWork.Clients.GetClientsRequest2008();
        return Ok(result);
    }
    //var 3. Devuelve el nombre del cliente, el nombre y primer apellido de su representante de ventas y el número de teléfono de la oficina del representante de ventas, de aquellos clientes que no hayan realizado ningún pago.

    [HttpGet("clientsWithoutPayments4")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetClientsWithoutPayments4()
    {
        var result = await _unitOfWork.Clients.GetClientsWithoutPayments4();
        return Ok(result);
    }

    // var 4. Devuelve el listado de clientes donde aparezca el nombre del cliente, el nombre y primer apellido de su representante de ventas y la ciudad donde está su oficina.
    [HttpGet("clientsWihtEmployeeAndOffice")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetClientsWihtEmployeeAndOffice()
    {
        var result = await _unitOfWork.Clients.GetClientsWihtEmployeeAndOffice();
        return Ok(result);
    }
    //Resumen 2.¿Cuántos clientes tiene cada país?
    [HttpGet("totalEmployeesByCountry")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetTotalEmployeesByCountry()
    {
        var result = await _unitOfWork.Clients.GetTotalEmployeesByCountry();
        return Ok(result);
    }

    //Resumen 5. ¿Cuántos clientes existen con domicilio en la ciudad de Madrid?
    [HttpGet("totalClientsMadrid")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetTotalClientsMadrid()
    {
        var result = await _unitOfWork.Clients.GetTotalClientsMadrid();
        return Ok(result);
    }

    //Resumen 6. ¿Calcula cuántos clientes tiene cada una de las ciudades que empiezan por M?
    [HttpGet("totalClientsM")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetTotalClientsM()
    {
        var result = await _unitOfWork.Clients.GetTotalClientsM();
        return Ok(result);
    }

    //Resumen 7. Devuelve el nombre de los representantes de ventas y el número de clientes al que atiende cada uno
    [HttpGet("totalclientsByEmployee")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetTotalclientsByEmployee()
    {
        var result = await _unitOfWork.Clients.GetTotalclientsByEmployee();
        return Ok(result);
    }
}


