namespace Api.Services;

using Api.Entities;

public interface IUserService
{
	Task<User> Authenticate(string username, string password);
	Task<IEnumerable<User>> GetAll();
}

public class UserService : IUserService
{
	//Hardcoded for demonstration
	private readonly List<User> _users = new List<User>
	{
		new User { Id = 1 , FirstName = "Admin", LastName = "User", Username = "Admin", Password = "admin"},
		new User { Id = 2 , FirstName = "User", LastName = " User", Username = "User", Password = "user"}
	};

	public async Task<User> Authenticate(string username, string password)
	{
		//wrapped in "await Task.Run" to mimic fetching user from DB - WILL CHANGE WHEN DB IS IMPLEMENTED
		var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));

		//on auth fail: null is returned because user is not found
		//on auth pass: user object is returned
		return user;
	}

	public async Task<IEnumerable<User>> GetAll()
	{
		return await Task.Run(() => _users);
	}
}