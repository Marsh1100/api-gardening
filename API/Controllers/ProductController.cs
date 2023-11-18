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

public class ProductController : ApiBaseController
{
    private readonly IUserService _userService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductController(IUserService userService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _userService = userService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
    {
        var results = await _unitOfWork.Products.GetAllAsync();
        return _mapper.Map<List<ProductDto>>(results);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ProductDto>>> GetPagination([FromQuery] Params p)
    {
        var results = await _unitOfWork.Products.GetAllAsync(p.PageIndex, p.PageSize, p.Search);
        var resultsDto = _mapper.Map<List<ProductDto>>(results.registros);
        return  new Pager<ProductDto>(resultsDto,results.totalRegistros, p.PageIndex, p.PageSize, p.Search);
    }

    [HttpPost()]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDto>> Post([FromBody] ProductDto dto)
    {
        var result = _mapper.Map<Product>(dto);
        this._unitOfWork.Products.Add(result);
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
    public async Task<ActionResult<ProductDto>> put(ProductDto dto)
    {
        if(dto == null){ return NotFound(); }
        var result = this._mapper.Map<Product>(dto);
        this._unitOfWork.Products.Update(result);
        Console.WriteLine(await this._unitOfWork.SaveAsync());
        return Ok(result);
    }


    [HttpDelete("{id}")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _unitOfWork.Products.GetByIdAsync(id);
        if(result == null)
        {
            return NotFound();
        }
        this._unitOfWork.Products.Remove(result);
        await this._unitOfWork.SaveAsync();
        return NoContent();
    }

    //15.  Devuelve un listado con todos los productos que pertenecen a la gama Ornamentales y que tienen más de 100 unidades en stock. El listado  deberá estar ordenado por su precio de venta, mostrando en primer lugar los de mayor precio.
    [HttpGet("productsOrnamentales")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductTypeDto>>> GetProductsOrnamentales()
    {
        var result = await _unitOfWork.Products.GetProductsOrnamentales();
        return _mapper.Map<List<ProductTypeDto>>(result);
    }

    //CE 5.Devuelve un listado de los productos que nunca han aparecido en un pedido.
    [HttpGet("productsWithoutRequest")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsWithoutRequest()
    {
        var result = await _unitOfWork.Products.GetProductsWithoutRequest();
        return  _mapper.Map<List<ProductDto>>(result);
    }

    //CE 6.Devuelve un listado de los productos que nunca han aparecido en un pedido. El resultado debe mostrar el nombre, la descripción y la imagen del producto.
    [HttpGet("productsWithoutRequest2")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<object>>> GetProductsWithoutRequest2()
    {
        var result = await _unitOfWork.Products.GetProductsWithoutRequest2();
        return  Ok(result);
    }
    
    //sub 13. Devuelve un listado de los productos que nunca han aparecido en un pedido.
    [HttpGet("productsWithoutRequest3")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductTypeDto>>> GetProductsWithoutRequest3()
    {
        var result = await _unitOfWork.Products.GetProductsWithoutRequest3();
        return  _mapper.Map<List<ProductTypeDto>>(result);
    }
}