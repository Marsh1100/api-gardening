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

public class RequestController : ApiBaseController
{
    private readonly IUserService _userService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RequestController(IUserService userService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _userService = userService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<RequestDto>>> Get()
    {
        var results = await _unitOfWork.Requests.GetAllAsync();
        return _mapper.Map<List<RequestDto>>(results);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<RequestDto>>> GetPagination([FromQuery] Params p)
    {
        var results = await _unitOfWork.Requests.GetAllAsync(p.PageIndex, p.PageSize, p.Search);
        var resultsDto = _mapper.Map<List<RequestDto>>(results.registros);
        return  new Pager<RequestDto>(resultsDto,results.totalRegistros, p.PageIndex, p.PageSize, p.Search);
    }

    [HttpPost()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RequestDto>> Post([FromBody] RequestDto dto)
    {
        var result = _mapper.Map<Request>(dto);
        this._unitOfWork.Requests.Add(result);
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

    public async Task<ActionResult<RequestDto>> put(RequestDto dto)
    {
        if(dto == null){ return NotFound(); }
        var result = this._mapper.Map<Request>(dto);
        this._unitOfWork.Requests.Update(result);
        Console.WriteLine(await this._unitOfWork.SaveAsync());
        return Ok(result);
    }


    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]


    public async Task<IActionResult> Delete(int id)
    {
        var result = await _unitOfWork.Requests.GetByIdAsync(id);
        if(result == null)
        {
            return NotFound();
        }
        this._unitOfWork.Requests.Remove(result);
        await this._unitOfWork.SaveAsync();
        return NoContent();
    }

    //2.Devuelve un listado con los distintos estados por los que puede pasar un pedido
    [HttpGet("states")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetStates()
    {
        var result = await _unitOfWork.Requests.GetStates();
        return Ok(result);
    }
   
    //9.Devuelve un listado con el código de pedido, código de cliente, fecha esperada y fecha de entrega de los pedidos que no han sido entregados a tiempo.
    [HttpGet("requestLate")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ResquestLateDto>>> GetRequestLate()
    {
        var result = await _unitOfWork.Requests.GetRequestLate();
        return _mapper.Map<List<ResquestLateDto>>(result);
    }

    //10.Devuelve un listado con el código de pedido, código de cliente, fecha esperada y fecha de entrega de los pedidos cuya fecha de entrega ha sido al menos dos días antes de la fecha esperada.
    [HttpGet("requestEarly")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ResquestLateDto>>> GetRequestEarly()
    {
        var result = await _unitOfWork.Requests.GetRequestEarly();
        return _mapper.Map<List<ResquestLateDto>>(result);
    }
    
    //11.Devuelve un listado de todos los pedidos que fueron rechazados en 2009.
    [HttpGet("requestReject")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<RequestDto>>> GetRequestReject()
    {
        var result = await _unitOfWork.Requests.GetRequestReject();
        return _mapper.Map<List<RequestDto>>(result);
    }

    //12. Devuelve un listado de todos los pedidos que han sido entregados en el mes de enero de cualquier año.
    [HttpGet("requestDelivered")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<RequestDto>>> GetRequestDelivered()
    {
        var result = await _unitOfWork.Requests.GetRequestDelivered();
        return _mapper.Map<List<RequestDto>>(result);
    }

    //Resume 10.Calcula el número de productos diferentes que hay en cada uno de los pedidos.

    [HttpGet("quantityProducts")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetQuantityProducts()
    {
        var result = await _unitOfWork.Requests.GetQuantityProducts();
        return Ok(result);
    }
    
}