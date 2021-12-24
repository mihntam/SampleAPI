using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleAPI.Data;
using SampleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CategoryController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var categoryList = _context.Categories.ToList();
                return Ok(categoryList);
            }
            catch
            {

                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var category = _context.Categories.SingleOrDefault(item =>
                    item.CategoryID == Guid.Parse(id)
                );

                if (category == null)
                {
                    return NotFound();
                }

                return Ok(category);
            }
            catch
            {

                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult CreateNew(CategoryModel model)
        {
            try
            {
                var category = new Category
                {
                    CategoryID = Guid.NewGuid(),
                    Name = model.Name
                };

                _context.Add(category);
                _context.SaveChanges();

                return Ok(new
                {
                    Success = true,
                    Data = category
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateById(string id, CategoryModel model)
        {
            try
            {
                var category = _context.Categories.SingleOrDefault(item =>
                    item.CategoryID == Guid.Parse(id)
                );

                if (category == null)
                {
                    return NotFound();
                }

                category.Name = model.Name;
                _context.SaveChanges();

                return Ok(category);
            }
            catch
            {

                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveCategory(string id)
        {
            try
            {
                var category = _context.Categories.SingleOrDefault(item =>
                    item.CategoryID == Guid.Parse(id)
                );
                if (category == null)
                {
                    return NotFound();
                }

                _context.Remove(category);
                _context.SaveChanges();

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
