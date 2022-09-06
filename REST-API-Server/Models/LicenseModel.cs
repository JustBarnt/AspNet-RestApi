using Microsoft.AspNetCore.Mvc;
namespace Api.Models;

public class PartialLicense
{ 
	public Guid Id { get; set; }
	public string? CustomerName { get; set; } 
	public byte ProductId { get; set; }
	public string? ProductName { get; set; }
	public string? CreatedBy { get; set; }
	public DateTime Created { get; set; }
	public DateTime? Expires { get; set; }
}

public class FullLicense : PartialLicense
{
	public Guid Customer { get; set; }
	public Guid EndUser { get; set; }
	public string? EndUserName { get; set; }
	public string? TrackingNubmer { get; set; }
	public string? ActivationCode { get; set; }
	public Int64 HardwareId { get; set; }
	public int Invalid { get; set; }
	public string? Description { get; set; }
}

public class LicenseId
{
	[FromQuery(Name = "selector")]
	public string Id
	{ get; set; } = "";
}

public class LicenseQuery
{
	[FromQuery(Name = "Amount")]
	public int? Amount
	{ get; set; } = 10;

	[FromQuery(Name = "Column")]
	public string Column
	{ get; set; } = "createdBy";

	[FromQuery(Name = "Value")]
	public string Value
	{ get; set; } = "joshr";

	[FromQuery(Name = "StartDate")]
	public string? StartDate
	{ get; set; }

	[FromQuery(Name = "EndDate")]
	public string? EndDate
	{ get; set; }
}