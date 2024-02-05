namespace Signalling.Application.Services.Api;

using Signalling.Application.IntermediateModel.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IApiService
{
	Task<ResponseResult<TResult>> GetAsync<TResult>(string url);
	Task<ResponseResult<TResult>> PostAsync<TResult>(string url, string jsonData);
	Task<ResponseResult<TResult>> PutAsync<TResult>(string url, string jsonData);
	Task<ResponseResult<TResult>> PostAsync<TResult, TRequestData>(string url, TRequestData requestData);
}
