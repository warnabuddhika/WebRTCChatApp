namespace Signalling.Application.Extensions;

using Newtonsoft.Json;
using Signalling.Application.IntermediateModel.Api;

public static class StringExtension
{
	public static ResponseResult<TResult> DeserializeObject<TResult>(this string response)
	{
		var result = JsonConvert.DeserializeObject<ResponseResult<TResult>>(response);

		return result;
	}
}
