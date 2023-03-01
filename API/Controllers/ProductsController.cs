using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;
using API.Errors;
using API.Helpers;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[Authorize(Roles = "Admin")]
public class ProductsController : BaseAPIController
{
    private readonly IMapper _mapper;

    private readonly IUnitOfWork _unitOfWork;

    public ProductsController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProductsAsync([FromQuery] ProductSpecParams specParams)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(specParams);

        var countSpec = new ProductWithFiltersForCountSpecification(specParams);

        var totalItems = await _unitOfWork.Repository<Product>().CountAsync(countSpec);

        var products = await _unitOfWork.Repository<Product>().ListAsync(spec);

        var data = _mapper.Map<IReadOnlyList<ProductToReturnDto>>(products);

        return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize, totalItems, data));
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductToReturnDto>> GetProductAsync(Guid id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(id);

        var prod = await _unitOfWork.Repository<Product>().GetEntityWithSpec(spec);

        if (prod == null)
            return NotFound(new ApiResponse(400));

        return _mapper.Map<ProductToReturnDto>(prod);
    }

    // it's odd


    [HttpPost("add-product")]
    public async Task<ActionResult?> AddProductAsync([FromForm] ProductDto product)
    {
        if (product is null)
        {
            return BadRequest("Invalid product");
        }

        product.Id = new Guid();

        var prod = _mapper.Map<Product>(product);

        prod.PictureUrl = await FileUploader.UploadAsync(product.Picture!, "images/products", "Content/images/products");

        _unitOfWork.Repository<Product>().Add(prod);

        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return null;
        }

        return Ok(/*something*/);
    }

    [HttpPut("edit-product")]
    public async Task<ActionResult?> EditProductAsync([FromForm] ProductDto product)
    {
        var prod = _mapper.Map<Product>(product);
         
        if (product.PictureUrl is null)
        {
            prod.PictureUrl = await FileUploader.UploadAsync(product.Picture!, "images/products", "Content/images/products");
        }

        _unitOfWork.Repository<Product>().Update(prod);

        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return null;
        }

        return Ok(/*something*/);
    }

    [HttpDelete("delete-product/{id}")]
    public async Task<ActionResult?> DeleteProductAsync(Guid id)
    {
        _unitOfWork.Repository<Product>().DeleteByIdAsync(id);

        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            return null;
        }

        return Ok(/*something*/);
    }
}
