namespace Api.Utils;

using Microsoft.Data.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Api.Entities;

public class DataContext : DbContext
{
	protected readonly IConfiguration Configuration;

	public DataContext(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(Configuration.GetConnectionString("UsersDatabase"));
    }

	public DbSet<User> Users {get; set;}
}