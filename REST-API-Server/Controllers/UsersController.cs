namespace Api.Controller;

using Microsoft.AspNetCore.Mvc;
using Api.Authorization;
using Api.Models;
using Api.Services;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
	private readonly IUserService _userService;

	public UsersController(IUserService userService)
	{
		_userService = userService;
	}

	[AllowAnonymous]
	[HttpPost("authenticate")]
	public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
	{
		var user = await _userService.Authenticate(model.Username, model.Password);

		if(user == null)
			return BadRequest(new { message = "Username/password is incorrect" });

		return Ok(user);
	}

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var users = await _userService.GetAll();
		return Ok(users);
	}
}