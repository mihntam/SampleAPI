using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleAPI.Data;
using SampleAPI.Models;
using SampleAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetAll(string search, double? from, double? to, string sortBy, int page = 1)
        {
            try
            {
                var result = _productRepository.GetAll(search, from, to, sortBy, page);
                return Ok(result);
            }
            catch
            {

                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(string id)
        {
            try
            {
                var data = _productRepository.GetById(id);
                if (data == null)
                {
                    return NotFound();
                }

                return Ok(data);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(Models.Product product)
        {
            try
            {
                var result = _productRepository.Add(product);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("id")]
        [Authorize]
        public IActionResult Update(string id, Models.Product product)
        {
            try
            {
                var result = _productRepository.Update(id, product);
                return Ok(new
                {
                    Success = true,
                    Data = result
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(string id)
        {
            try
            {

                var isRemove = _productRepository.Delete(id);

                if (!isRemove)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    Success = true,

                });

            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
