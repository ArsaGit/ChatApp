using AutoMapper;
using ChatApp.Contexts;
using ChatApp.DTOs;
using ChatApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ChatAppContext _context;
    private readonly IMapper _mapper;

    public UserController(ChatAppContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.Users.Where(u => !u.IsDeleted).ToListAsync();
        var userDtos = _mapper.Map<ICollection<UserDto>>(users);
        return Ok(userDtos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUser([FromRoute] Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is { IsDeleted: true }) return NotFound();
        
        var userDto = _mapper.Map<UserDto>(user);
        return Ok(userDto);
    }

    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] CreateUserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return Created($"/{user.Id}", user);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();
        
        user.IsDeleted = true;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateUser(
        [FromRoute] Guid id, 
        [FromBody] UpdateUserDto updateUserDto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();

        user.Name = updateUserDto.Name;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}