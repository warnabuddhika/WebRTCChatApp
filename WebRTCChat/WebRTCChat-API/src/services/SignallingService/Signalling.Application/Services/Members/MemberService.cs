namespace Signalling.Application.Services.Members;

using Microsoft.AspNetCore.Http;
using Signalling.Application.Dtos;
using Signalling.Application.IntermediateModel.Api;
using Signalling.Application.Services.Api;
using System;
using System.Threading.Tasks;

public class MemberService : IMemberService
{
	private readonly IApiService _apiService;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public MemberService(IApiService apiService, IHttpContextAccessor httpContextAccessor)
	{
		_apiService = apiService;
		_httpContextAccessor = httpContextAccessor;
	}
	public async Task<ResponseResult<MemberDto>> GetMember(string userName)
	{
		var apiUrl = string.Format("/api/member/{0}", userName);
		var roomResult = await _apiService.GetAsync<MemberDto>(apiUrl);
		return roomResult;
	}
}
