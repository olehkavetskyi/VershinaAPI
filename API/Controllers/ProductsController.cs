using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _storeContext;

        public ProductsController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [HttpGet]
        public async Task<ActionResult<IQueryable<Product>>> GetProductsAsync()
        {
            return Ok(await _storeContext.Products.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
            //Product product = await _storeContext.Products.FirstOrDefaultAsync(pr => pr.Id == id);

            return Ok(await _storeContext.Products.FindAsync(id));
        }
    }
}
