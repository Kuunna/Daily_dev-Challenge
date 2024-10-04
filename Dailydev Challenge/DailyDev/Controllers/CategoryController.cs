﻿using DailyDev.Models;
using DailyDev.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DailyDev.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAll()
        {
            return Ok(_categoryRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetById(int id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public ActionResult Add([FromBody] Category category)
        {
            _categoryRepository.Add(category);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Category category)
        {
            category.Id = id;
            _categoryRepository.Update(category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _categoryRepository.Delete(id);
            return NoContent();
        }
    }

}
