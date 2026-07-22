using Application.DTOs.Item;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemsController(IItemService itemService)
    {
        _itemService = itemService;
    }

    // GET: api/v1/items
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _itemService.GetAllAsync();
        return Ok(items);
    }

    // GET: api/v1/items/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _itemService.GetByIdAsync(id);

        if (item == null)
            return NotFound(new { message = "Item not found." });

        return Ok(item);
    }

    // POST: api/v1/items
    [HttpPost]
    public async Task<IActionResult> Create(CreateItemDto dto)
    {
        var item = await _itemService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById),
            new { id = item.Id },
            item);
    }

    // PUT: api/v1/items/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateItemDto dto)
    {
        var updated = await _itemService.UpdateAsync(id, dto);

        if (!updated)
            return NotFound(new { message = "Item not found." });

        return NoContent();
    }

    // DELETE: api/v1/items/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _itemService.DeleteAsync(id);

        if (!deleted)
            return NotFound(new { message = "Item not found." });

        return NoContent();
    }
}