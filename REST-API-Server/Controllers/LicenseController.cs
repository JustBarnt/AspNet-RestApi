using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Api.Models;
using Api.Services;

namespace Api.Controller;

[ApiController]
[Route("[action]")]

public class LicenseController : ControllerBase
{
	
	//Parameterless Contstructor: Because MS says so. I can't find a reason why that makes sense.
	public LicenseController(){}


	[EnableCors("licenseApi")]
	[HttpGet("search")]
	public ActionResult<List<PartialLicense>> Licenses([FromQuery] LicenseQuery query)
	{
		List<PartialLicense> licenses = LicenseServices.Query(query);

		if(!licenses.Any())
			return NotFound();

		return licenses;
	}

	[EnableCors("licenseApi")]
	[HttpGet("search")]
	public ActionResult<List<FullLicense>> License([FromQuery] LicenseId query)
	{
		List<FullLicense> license = LicenseServices.QuerySingleLicense(query);

		if(!license.Any())
			return NotFound();

		return license;
	}
}
