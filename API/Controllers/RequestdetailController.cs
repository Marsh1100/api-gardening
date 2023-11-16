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

public class RequestdetailController : ApiBaseController
{
    private readonly IUserService _userService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RequestdetailController(IUserService userService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _userService = userService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<RequestdetailDto>>> Get()
    {
        var results = await _unitOfWork.Requestdetails.GetAllAsync();
        return _mapper.Map<List<RequestdetailDto>>(results);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<RequestdetailDto>>> GetPagination([FromQuery] Params p)
    {
        var results = await _unitOfWork.Requestdetails.GetAllAsync(p.PageIndex, p.PageSize, p.Search);
        var resultsDto = _mapper.Map<List<RequestdetailDto>>(results.registros);
        return  new Pager<RequestdetailDto>(resultsDto,results.totalRegistros, p.PageIndex, p.PageSize, p.Search);
    }

    [HttpPost()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RequestdetailDto>> Post([FromBody] RequestdetailDto dto)
    {
        var result = _mapper.Map<Requestdetail>(dto);
        this._unitOfWork.Requestdetails.Add(result);
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

    public async Task<ActionResult<RequestdetailDto>> put(RequestdetailDto dto)
    {
        if(dto == null){ return NotFound(); }
        var result = this._mapper.Map<Requestdetail>(dto);
        this._unitOfWork.Requestdetails.Update(result);
        Console.WriteLine(await this._unitOfWork.SaveAsync());
        return Ok(result);
    }


    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]


    public async Task<IActionResult> Delete(int id)
    {
        var result = await _unitOfWork.Requestdetails.GetByIdAsync(id);
        if(result == null)
        {
            return NotFound();
        }
        this._unitOfWork.Requestdetails.Remove(result);
        await this._unitOfWork.SaveAsync();
        return NoContent();
    }

   
    
}