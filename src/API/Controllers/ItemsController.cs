using Application.DTOs.Item;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[ApiController]
[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemsController(IItemService itemService)
    {
        _itemService = itemService;
    }

    // GET: api/v1/Items
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _itemService.GetAllAsync();
        return Ok(items);
    }

    // GET: api/v1/Items/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _itemService.GetByIdAsync(id);
        return Ok(item);
    }

    // POST: api/v1/Items
    [HttpPost]
    public async Task<IActionResult> Create(CreateItemDto dto)
    {
        var item = await _itemService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { version = "1.0", id = item.Id },
            item);
    }

    // PUT: api/v1/Items/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateItemDto dto)
    {
        await _itemService.UpdateAsync(id, dto);
        return NoContent();
    }

    // DELETE: api/v1/Items/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _itemService.DeleteAsync(id);
        return NoContent();
    }
}