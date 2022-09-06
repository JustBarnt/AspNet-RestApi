using Api.Models;

using Microsoft.Data.SqlClient;

using System.Data;

namespace Api.Utils;

//TODO: (BRENT) Need to create a single method that handles the boilerplate for creating a SQL Command
// This means SQLDataAdapter, SqlCommandBuilder, and SQL Command based on parameters
public class SqlUtilities
{
	/// <summary>
	/// A re-usable SQL Select command
	/// </summary>
	/// <param name="connectionString">The SQL connection string.</param>
	/// <param name="query">LicenseQuery object</param>
	/// <returns>A DataSet object</returns>
	public static DataSet SqlSelectTop(string connectionString, LicenseQuery query)
	{
		using(SqlConnection connection = new SqlConnection(connectionString))
		{
			SqlDataAdapter adapter = new SqlDataAdapter();
			SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
			DataSet dataset = new DataSet();

			connection.Open();

			string cmdText = CommandBuilder(query, builder);

			SqlCommand command = new SqlCommand(cmdText, connection);
			command.Parameters.Add("@amount", SqlDbType.Int).Value = query.Amount;
			command.Parameters.Add("@value", SqlDbType.VarChar, 160).Value = query.Value;

			if(!String.IsNullOrEmpty(query.StartDate) && !String.IsNullOrEmpty(query.EndDate))
			{
				command.Parameters.Add("@endDate", SqlDbType.DateTime).Value = query.EndDate;
				command.Parameters.Add("@startDate", SqlDbType.DateTime).Value = query.StartDate;
			}

			adapter.SelectCommand = command;
			adapter.Fill(dataset);

			return dataset;
		}
	}

	/// <summary>
	/// Creates a command for return a single license based on its GUID with all its information.
	/// </summary>
	/// <param name="connectionString">The SQL connection string.</param>
	/// <param name="query">LicenseId object</param>
	/// <returns>A DataSet object</returns>
	public static DataSet SqlSelectSingleAll(string connectionString, LicenseId query)
	{
		Guid guid = new Guid(query.Id);
		using(SqlConnection connection = new SqlConnection(connectionString))
		{
			SqlDataAdapter adapter = new SqlDataAdapter();
			SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
			DataSet dataset = new DataSet();

			connection.Open();

			string cmdText = GetCompleteLicense(query, builder);
			SqlCommand command = new SqlCommand(cmdText, connection);
			command.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = guid;

			adapter.SelectCommand = command;
			adapter.Fill(dataset);

			return dataset;
		}
	}

	/// <summary>
	/// Converts a DataSet into the License model
	/// </summary>
	/// <param name="data">DataSet to be converted</param>
	/// <returns>A list folling the license model.</returns>
	public static List<PartialLicense> DataSetToPartialLicense(DataSet data)
	{
		List<PartialLicense> list = data.Tables[0].AsEnumerable()
			.Select(dataRow => new PartialLicense
			{
				Id = dataRow.Field<Guid>("id"),
				CustomerName = dataRow.Field<string?>("customerName"),
				ProductId = dataRow.Field<byte>("productId"),
				ProductName = dataRow.Field<string?>("productName"),
				CreatedBy = dataRow.Field<string?>("createdBy"),
				Created = dataRow.Field<DateTime>("created"),
				Expires = dataRow.Field<DateTime?>("expires")
			}).ToList();

		return list;
	}

	public static List<FullLicense> DataSetToFullLicense(DataSet data)
	{
		List<FullLicense> list = data.Tables[0].AsEnumerable()
		.Select(dataRow => new FullLicense
		{
			Id = dataRow.Field<Guid>("id"),
			Customer = dataRow.Field<Guid>("customer"),
			EndUser = dataRow.Field<Guid>("enduser"),
			CustomerName = dataRow.Field<string?>("customerName"),
			EndUserName = dataRow.Field<string?>("enduserName"),
			TrackingNubmer = dataRow.Field<string?>("trackingNumber"),
			ProductId = dataRow.Field<byte>("productId"),
			ProductName = dataRow.Field<string?>("productName"),
			ActivationCode = dataRow.Field<string?>("activationCode"),
			HardwareId = dataRow.Field<Int64>("hardwareId"),
			CreatedBy = dataRow.Field<string?>("createdBy"),
			Created = dataRow.Field<DateTime>("created"),
			Expires = dataRow.Field<DateTime?>("expires"),
			Invalid = dataRow.Field<int>("invalid"),
			Description = dataRow.Field<string?>("description"),
		}).ToList();

		return list;
	}

	private static string CommandBuilder(LicenseQuery query, SqlCommandBuilder builder)
	{
		string command = "";
		string column = builder.QuoteIdentifier(query.Column);

		if(!String.IsNullOrEmpty(query.StartDate) && !String.IsNullOrEmpty(query.EndDate))
		{
			command = "SELECT TOP(@amount) [id], [customerName], [productId], [productName], [createdBy], [created], [expires] "
					+ "FROM [CommSysLicenses]..[license]"
					+ $"WHERE {column} = @value AND [created] BETWEEN @startDate AND @endDate "
					+ "ORDER BY [created], [expires] DESC";
		}
		else
		{
			command = "SELECT TOP(@amount) [id], [customerName], [productId], [productName], [createdBy], [created], [expires] "
		 		  + "FROM [CommSysLicenses]..[license] "
		 		  + $"WHERE {column} = @value "
		 		  + "ORDER BY [created], [expires] DESC";
		}

		return command;
	}

	private static string GetCompleteLicense(LicenseId query, SqlCommandBuilder builder) =>
		"SELECT * FROM LICENSE WHERE [id] = @id";
}
