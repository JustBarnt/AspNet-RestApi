using Microsoft.Data.SqlClient;

namespace Api.Models;

//Creating a basic configuration class for the License Database
public class Configuration
{
	public Database LicenseDb = new Database("Host", "DB", "USER", "PASS");
}

public class Database
{
	public string Host { get; set; }
	public string DatabaseName { get; set; }
	public string UserName { get; set; }
	public string Password { get; set; }

	public Database(string host, string databaseName, string user, string password)
	{
		Host = host;
		DatabaseName = databaseName;
		UserName = user;
		Password = password;
	}

	public override string ToString()
	{
		var builder = new SqlConnectionStringBuilder()
		{
			DataSource = Host,
			InitialCatalog = DatabaseName,
			UserID = UserName,
			Password = Password
		};

		return builder.ToString();
	}
}