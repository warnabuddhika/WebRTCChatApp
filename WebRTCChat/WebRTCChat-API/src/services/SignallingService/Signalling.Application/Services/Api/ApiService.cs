namespace Signalling.Application.Services.Api;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Signalling.Application.Extensions;
using Signalling.Application.IntermediateModel.Api;
using System;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

public class ApiService : IApiService
{
	private readonly IConfiguration _configuration;
	IHttpContextAccessor _httpContextAccessor;
	public ApiService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
	{
		_configuration = configuration;
		_httpContextAccessor = httpContextAccessor;
	}
	public async Task<ResponseResult<TResult>> GetAsync<TResult>(string url)
	{
		using var client = await GetHttpClientAsync();
		var response = await client.GetAsync(url);

		if (response.IsSuccessStatusCode)
		{
			var apiResponse = await response.Content.ReadAsStringAsync();
			try
			{
				return apiResponse.DeserializeObject<TResult>();
			}
			catch (Exception ex)
			{
				return GetErrorResponse<TResult>(new string[] { ex.Message });
			}
		}
		else
		{
			return GetErrorResponse<TResult>(new string[] { string.Format("Error with Status Code : {0}", response.StatusCode.ToString()) });
		}
	}

	public async Task<ResponseResult<TResult>> PutAsync<TResult>(string url, string jsonData)
	{
		var data = new StringContent(jsonData, Encoding.UTF8, "application/json");

		using var client = await GetHttpClientAsync();
		var response = await client.PutAsync(url, data);

		if (response.IsSuccessStatusCode)
		{
			var apiResponse = await response.Content.ReadAsStringAsync();
			try
			{
				return apiResponse.DeserializeObject<TResult>();
			}
			catch (Exception ex)
			{
				return GetErrorResponse<TResult>(new string[] { ex.Message });
			}
		}
		else
		{
			return GetErrorResponse<TResult>(new string[] { string.Format("Error with Status Code : {0}", response.StatusCode.ToString()) });
		}
	}
	public async Task<ResponseResult<TResult>> PostAsync<TResult>(string url, string jsonData)
	{
		var data = new StringContent(jsonData, Encoding.UTF8, "application/json");

		using var client = await GetHttpClientAsync();
		var response = await client.PostAsync(url, data);

		if (response.IsSuccessStatusCode)
		{
			var apiResponse = await response.Content.ReadAsStringAsync();
			try
			{
				return apiResponse.DeserializeObject<TResult>();
			}
			catch (Exception ex)
			{
				return GetErrorResponse<TResult>(new string[] { ex.Message });
			}
		}
		else
		{
			return GetErrorResponse<TResult>(new string[] { string.Format("Error with Status Code : {0}", response.StatusCode.ToString()) });
		}
	}

	public async Task<ResponseResult<TResult>> PostAsync<TResult, TRequestData>(string url, TRequestData requestData)
	{
		var jsonData = JsonConvert.SerializeObject(requestData);
		var data = new StringContent(jsonData, Encoding.UTF8, "application/json");

		using var client = await GetHttpClientAsync();
		var response = await client.PostAsync(url, data);

		if (response.IsSuccessStatusCode)
		{
			var apiResponse = await response.Content.ReadAsStringAsync();
			try
			{
				return apiResponse.DeserializeObject<TResult>();
			}
			catch (Exception ex)
			{
				return GetErrorResponse<TResult>(new string[] { ex.Message });
			}
		}
		else
		{
			return GetErrorResponse<TResult>(new string[] { string.Format("Error with Status Code : {0}", response.StatusCode.ToString()) });
		}
	}

	#region private methods
	private async Task<HttpClient> GetHttpClientAsync()
	{
		var token = _httpContextAccessor.HttpContext != null ? await _httpContextAccessor.HttpContext.GetTokenAsync("access_token") : null;
		int timeOutInMinutes = _configuration.GetValue<int>("Immigration:ApiTimeOutInMinutes");
		var baseUrl = _configuration.GetValue<string>("IdentityServer:Url");
		var client = new HttpClient();
		client.Timeout = TimeSpan.FromMinutes(timeOutInMinutes);
		client.BaseAddress = new Uri(baseUrl);
		if (string.IsNullOrWhiteSpace(token))
		{
			client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		}

		return client;
	}

	private ResponseResult<TResult> GetErrorResponse<TResult>(string[] errorMessages)
	{
		return new ResponseResult<TResult>() { Errors = errorMessages, Result = default(TResult), Succeeded = false };
	}
	#endregion
}
