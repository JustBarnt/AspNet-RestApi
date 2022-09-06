namespace Api.Models.Users;

using System.ComponentModel.DataAnnotations;

public class AuthenticateRequest
{
	[Required]
	public string Username { get; set; }
	public string Password { get; set; } 
}