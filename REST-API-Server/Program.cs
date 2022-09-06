
using Api.Authorization;
using Api.Services;
namespace Api;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		{
			var services = builder.Services;
			services.AddCors();
			services.AddControllers();

			//May not need this
			// services.AddCors(policy => policy.AddPolicy("licenseApi", builder =>
			// {
			// 	builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
			// }));

			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();

			//configure DI for application services
			services.AddScoped<IUserService, UserService>();
		}

		var app = builder.Build();

		{
			app.UseMiddleware<BasicAuthMiddleware>();

			// Configure the HTTP request pipeline.
			if(app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseCors(x => x
				.AllowAnyOrigin()
				.AllowAnyHeader()
				.AllowAnyMethod());

			app.MapControllers();
			//app.UseCors("licenseApi");
			app.UseHttpsRedirection();
			app.UseAuthorization();
		}

		app.Run();
	}
}
