using System.Data;

using Api.Models;
using Api.Utils;

using Microsoft.Data.SqlClient;

namespace Api.Services;


public static class LicenseServices
{
	static readonly Configuration config = new Configuration();

	static LicenseServices()
	{
	}

	public static List<PartialLicense> Query(LicenseQuery query)
	{
		string connectionString = config.LicenseDb.ToString();
		DataSet DataTable = SqlUtilities.SqlSelectTop(connectionString, query);
		return SqlUtilities.DataSetToPartialLicense(DataTable);
	}

	public static List<FullLicense> QuerySingleLicense(LicenseId query)
	{
		string connectionString = config.LicenseDb.ToString();
		DataSet DataTable = SqlUtilities.SqlSelectSingleAll(connectionString, query);
		return SqlUtilities.DataSetToFullLicense(DataTable);
	}
}