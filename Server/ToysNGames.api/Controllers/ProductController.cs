using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToysNGames.api.DTOS;
using ToysNGames.Data;
using ToysNGames.Data.Entities;

namespace ToysNGames.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, ILogger<ProductController> logger, IMapper mapper)
        {
            _productRepository = productRepository;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _productRepository.GetAllAsync();
                return Ok(_mapper.Map<ProductDTO[]>(result));
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get products. {e}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/Product/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            try
            {
                var result = await _productRepository.GetAsync(id);

                if (result is null)
                    return NotFound();

                return Ok(_mapper.Map<ProductDTO>(result));
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to get product. {e}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody] ProductDTO product)
        {
            if (product is null)
                return BadRequest();

            try
            {
                var productEntity = _mapper.Map<Product>(product);

                if (!ValidateFields(productEntity))
                    return BadRequest();

                if (await _productRepository.Add(productEntity))
                    return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to add product. {e}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return BadRequest();
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Put(int id, [FromBody] ProductDTO product)
        {
            try
            {
                var productFound = await _productRepository.GetAsync(id);
                if (productFound is null)
                    return NotFound();

                if (product is null)
                    return BadRequest();

                var productEntity = _mapper.Map<Product>(product);

                if (!ValidateFields(productEntity))
                    return BadRequest();

                if (await _productRepository.Update(productEntity))
                    return Ok(product);
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to update product. {e}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return BadRequest();
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            if (id < 1)
                return BadRequest();

            try
            {
                var productFound = await _productRepository.GetAsync(id);

                if (productFound is null)
                    return NotFound();

                if (await _productRepository.Delete(productFound))
                    return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to delete product. {e}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return BadRequest();
        }

        private bool ValidateFields(Product product)
        {
            if (string.IsNullOrEmpty(product.Name))
                return false;

            if (string.IsNullOrEmpty(product.Company))
                return false;

            if (product.AgeRestriction > 100 || product.AgeRestriction < 0)
                return false;

            if (product.Price > 1000 || product.Price < 1)
                return false;

            return true;
        }
    }
}
