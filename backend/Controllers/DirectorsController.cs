using Microsoft.AspNetCore.Mvc;
using backend.DTOs;
using backend.Services;
using System.Globalization;

namespace backend.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DirectorsController : ControllerBase
{
    private readonly IDirectorService _service;

    public DirectorsController(IDirectorService service)
    {
        _service = service;
    }

    // GET: api/directors
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DirectorDto>>> GetAll()
    {
        var directors = await _service.GetAllAsync();
        return Ok(directors);
    }

    // GET: api/directors/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<DirectorDto>> GetById(int id)
    {
        var director = await _service.GetByIdAsync(id);
        if (director is null)
        {
            return NotFound();
        }
        return Ok(director);
    }

    // POST: api/directors
    [HttpPost]
    public async Task<ActionResult<DirectorDto>> Create(CreateDirectorDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: api/directors/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateDirectorDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        if (!updated) return NotFound();
        return NoContent();
    }

    // DELETE: api/directors/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }

}