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

public class PaymentController : ApiBaseController
{
    private readonly IUserService _userService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PaymentController(IUserService userService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _userService = userService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<PaymentDto>>> Get()
    {
        var results = await _unitOfWork.Payments.GetAllAsync();
        return _mapper.Map<List<PaymentDto>>(results);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PaymentDto>>> GetPagination([FromQuery] Params p)
    {
        var results = await _unitOfWork.Payments.GetAllAsync(p.PageIndex, p.PageSize, p.Search);
        var resultsDto = _mapper.Map<List<PaymentDto>>(results.registros);
        return  new Pager<PaymentDto>(resultsDto,results.totalRegistros, p.PageIndex, p.PageSize, p.Search);
    }

    [HttpPost()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaymentDto>> Post([FromBody] PaymentDto dto)
    {
        var result = _mapper.Map<Payment>(dto);
        this._unitOfWork.Payments.Add(result);
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

    public async Task<ActionResult<PaymentDto>> put(PaymentDto dto)
    {
        if(dto == null){ return NotFound(); }
        var result = this._mapper.Map<Payment>(dto);
        this._unitOfWork.Payments.Update(result);
        Console.WriteLine(await this._unitOfWork.SaveAsync());
        return Ok(result);
    }


    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]


    public async Task<IActionResult> Delete(int id)
    {
        var result = await _unitOfWork.Payments.GetByIdAsync(id);
        if(result == null)
        {
            return NotFound();
        }
        this._unitOfWork.Payments.Remove(result);
        await this._unitOfWork.SaveAsync();
        return NoContent();
    }

    //13.  Devuelve un listado con todos los pagos que se realizaron en el año 2008 mediante Paypal. Ordene el resultado de mayor a menor
    [HttpGet("payment2008")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PaymentDto>>> GetPayment2008()
    {
        var result = await _unitOfWork.Payments.GetPayment2008();
        return _mapper.Map<List<PaymentDto>>(result);
    }

    //14.  Devuelve un listado con todas las formas de pago que aparecen en la  tabla pago. Tenga en cuenta que no deben aparecer formas de pago repetidas.
    [HttpGet("paymentMethod")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetPaymentMethod()
    {
        var result = await _unitOfWork.Payments.GetPaymentMethod();
        return Ok(result);
    }

    //Resume 16. Muestre la suma total de todos los pagos que se realizaron para cada uno de los años que aparecen en la tabla pagos..
    [HttpGet("paymentByYear")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetPaymentByYear()
    {
        var result = await _unitOfWork.Payments.GetPaymentByYear();
        return Ok(result);
    }
    //Resumen 3.¿Cuál fue el pago medio en 2009?
    [HttpGet("averagePay2009")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetAveragePay2009()
    {
        var result = await _unitOfWork.Payments.GetAveragePay2009();
        return Ok(result);
    }

}
   
    
