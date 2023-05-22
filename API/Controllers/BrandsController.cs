using API.Dtos;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Roles = "Admin")]
public class BrandsController : BaseAPIController
{
    private readonly IMapper _mapper;

    private readonly IUnitOfWork _unitOfWork;

    public BrandsController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProductBrand>>> GetProductBrandsAsync()
    {
        return Ok(await _unitOfWork.Repository<ProductBrand>().ListAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductBrand>> GetProductBrandByIdAsync(Guid id)
    {
        var brand = await _unitOfWork.Repository<ProductBrand>().GetByIdAsync(id);

        if (brand == null)
            return NotFound();

        return brand;
    }

    [HttpPost("add-brand")]
    public async Task<ActionResult?> AddProductBrandAsync([FromBody] ProductBrandDto brand)
    {
        if (brand is null)
        {
            return BadRequest("Invalid product brand");
        }

        brand.Id = Guid.NewGuid();


        _unitOfWork.Repository<ProductBrand>().Add(_mapper.Map<ProductBrand>(brand));

        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return null;
        }

        return Ok();
    }

    [HttpPut("edit-brand")]
    public async Task<ActionResult?> EditProductBrandAsync(ProductBrand productBrand)
    {
        _unitOfWork.Repository<ProductBrand>().Update(productBrand);

        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return null;
        }

        return Ok();
    }

    [HttpDelete("delete-brand")]
    public async Task<ActionResult?> DeleteProductBrandAsync(ProductBrand productBrand)
    {
        _unitOfWork.Repository<ProductBrand>().Delete(productBrand);

        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return null;
        }

        return Ok();
    }
}
