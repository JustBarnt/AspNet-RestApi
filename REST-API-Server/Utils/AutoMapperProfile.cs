namespace Api.Utils;

using AutoMapper;
using Api.Entities;
using Api.Models.Users;

public class AutoMapperProfile : Profile
{
	public AutoMapperProfile()
	{
		// User -> AuthenticateResponse
		CreateMap<User, AuthenticateResponse>();

		// RegisterRequest -> User
		CreateMap<RegisterRequest, User>();

		//UpdateRequest -> User
		CreateMap<UpdateRequest, User>()
			.ForAllMembers(x => x.Condition((src, dest, prop) => 
			{
				if(prop == null) return false;
				if(prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

				return true;
			}));
	}	
}