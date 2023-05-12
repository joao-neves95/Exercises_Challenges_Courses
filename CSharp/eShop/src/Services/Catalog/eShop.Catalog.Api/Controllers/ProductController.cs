using eShop.Catalog.Api.Entities;
using eShop.Catalog.Api.Repositories;

using Microsoft.AspNetCore.Mvc;

using System.Net;

namespace eShop.Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/v1/catalog/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        private readonly ILogger<ProductController> _logger;

        public ProductController(
            IProductRepository productRepository,
            ILogger<ProductController> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<DataProduct>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<DataProduct>>> GetAll()
        {
            return Ok(await _productRepository.GetAllAsync());
        }

        [HttpGet]
        [Route("id/{id:length(24)}")]
        [ProducesResponseType(typeof(DataProduct), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<DataProduct>> GetById(string id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet]
        [Route("name/{name}")]
        [ProducesResponseType(typeof(IEnumerable<DataProduct>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<DataProduct>>> GetByName(string name)
        {
            var product = await _productRepository.GetByNameAsync(name);

            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet]
        [Route("category/{category}")]
        [ProducesResponseType(typeof(IEnumerable<DataProduct>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<DataProduct>>> GetByCategory(string category)
        {
            var product = await _productRepository.GetByCategoryAsync(category);

            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(DataProduct), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<DataProduct>> Post([FromBody] DataProduct product)
        {
            await _productRepository.CreateAsync(product);

            return CreatedAtRoute("by-id", product);
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType(typeof(DataProduct), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<DataProduct>> Put([FromBody] DataProduct product)
        {
            var result = await _productRepository.UpdateAsync(product);

            if (!result)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpDelete]
        [Route("{id:length(24)}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<bool>> Delete(string id)
        {
            var result = await _productRepository.DeleteAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
