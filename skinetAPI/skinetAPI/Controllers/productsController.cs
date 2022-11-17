using System;
using Core.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Infrastructure.Data;
using Core.Interfaces;
using Core.Specifications;
using skinetAPI.Dtos;
using AutoMapper;
using skinetAPI.Errors;

namespace skinetAPI.Controllers
{

    public class productsController : BaseApiController
    {
        private readonly IGenericRepository<product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IMapper _mapper;
        public productsController(IGenericRepository<product> productRepository, IGenericRepository<ProductType> productTypeRepository, IGenericRepository<ProductBrand> productBrandRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            _productBrandRepository = productBrandRepository;
            _mapper = mapper;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> Getproduct()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();

            var products = await _productRepository.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<product>, IReadOnlyList<ProductToReturnDto>>(products));
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> Getproduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await _productRepository.GetEntityWithSpec(spec);

            return Ok(_mapper.Map<product, ProductToReturnDto>(product));
        }
        // GET: api/products/5
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var brand = await _productBrandRepository.ListAllAsync();

            if (brand == null)
            {
                return NotFound();
            }

            return Ok(brand);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var type = await _productTypeRepository.ListAllAsync();

            if (type == null)
            {
                return NotFound();
            }

            return Ok(type);
        }


        //    // PUT: api/products/5
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPut("{id}")]
        //    public async Task<IActionResult> Putproduct(int id, product product)
        //    {
        //        if (id != product.Id)
        //        {
        //            return BadRequest();
        //        }

        //        _context.Entry(product).State = EntityState.Modified;

        //        try
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!productExists(id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //        return NoContent();
        //    }

        //    // POST: api/products
        //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //    [HttpPost]
        //    public async Task<ActionResult<product>> Postproduct(product product)
        //    {
        //        _context.product.Add(product);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction("Getproduct", new { id = product.Id }, product);
        //    }

        //    // DELETE: api/products/5
        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> Deleteproduct(int id)
        //    {
        //        var product = await _context.product.FindAsync(id);
        //        if (product == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.product.Remove(product);
        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }

        //    private bool productExists(int id)
        //    {
        //        return _context.product.Any(e => e.Id == id);
        //    }
    }
}
