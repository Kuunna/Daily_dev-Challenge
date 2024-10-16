﻿using DailyDev.Models;
using DailyDev.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DailyDev.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ItemRepository _itemRepository;
        private readonly HttpClient _httpClient;
        private readonly CategoryRepository _categoryRepository;

        public ItemController(ItemRepository itemRepository, HttpClient httpClient, CategoryRepository categoryRepository)
        {
            _itemRepository = itemRepository;
            _httpClient = httpClient;
            _categoryRepository = categoryRepository;
        }


        [HttpGet("{id}")]
        public ActionResult<Item> GetById(int id)
        {
            var item = _itemRepository.GetById(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public ActionResult Add([FromBody] Item item)
        {
            _itemRepository.Add(item);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPatch("{id}")]
        public ActionResult Update(int id, [FromBody] Item item)
        {
            item.Id = id;
            _itemRepository.Update(item);
            return NoContent();
        }

        [HttpPut]

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _itemRepository.Delete(id);
            return NoContent();
        }

        // Method get RSS feed from Category and save data Item
        [HttpGet("fetch-rss")]
        public async Task<IActionResult> FetchRssFeeds(CancellationToken cancellationToken)
        {
            try
            {
                var categories = _categoryRepository.GetAll();
                int batchSize = 10;  
                _httpClient.Timeout = TimeSpan.FromMinutes(5);

                for (int i = 0; i < categories.Count(); i += batchSize)
                {
                    var batchCategories = categories.Skip(i).Take(batchSize);

                    var tasks = batchCategories.Select(async category =>
                    {
                        var response = await _httpClient.GetAsync(category.Source, cancellationToken);
                        response.EnsureSuccessStatusCode();

                        var rssData = await response.Content.ReadAsStringAsync();
                        var rssXml = XDocument.Parse(rssData);

                        _itemRepository.ParseAndSaveRss(rssXml, category.Id);
                    });

                    await Task.WhenAll(tasks); 
                }

                return Ok("RSS data fetched and saved successfully");
            }
            catch (HttpRequestException e)
            {
                return BadRequest($"Error fetching RSS feed: {e.Message}");
            }
            catch (OperationCanceledException)
            {
                return StatusCode(408, "Request timed out.");
            }
        }
    }
}
