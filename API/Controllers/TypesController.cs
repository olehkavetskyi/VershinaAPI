using API.Dtos;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Roles = "Admin")]
public class TypesController : BaseAPIController
{
    private readonly IMapper _mapper;

    private readonly IUnitOfWork _unitOfWork;

    public TypesController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<Pagination<ProductType>>> GetProductTypesAsync()
    {
        return Ok(await _unitOfWork.Repository<ProductType>().ListAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductType>> GetProductTypeByIdAsync(Guid id)
    {
        var type = await _unitOfWork.Repository<ProductType>().GetByIdAsync(id);

        if (type == null)
            return NotFound();

        return type;
    }

    [HttpPost("add-type")]
    public async Task<ActionResult?> AddProductTypeAsync([FromBody] ProductTypeDto type)
    {
        if (type is null)
        {
            return BadRequest("Invalid type");
        }

        type.Id = new Guid();

        _unitOfWork.Repository<ProductType>().Add(_mapper.Map<ProductType>(type));

        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return null;
        }

        return Ok();
    }

    [HttpPut("edit-type")]
    public async Task<ActionResult?> EditProductTypeAsync([FromForm] ProductTypeDto type)
    {
        _unitOfWork.Repository<ProductType>().Update(_mapper.Map<ProductType>(type));

        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return null;
        }

        return Ok(/*something*/);
    }

    [HttpDelete("delete-type")]
    public async Task<ActionResult?> DeleteProductTypeAsync(ProductType productType)
    {
        _unitOfWork.Repository<ProductType>().Delete(productType);

        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return null;
        }

        return Ok();
    }
}
