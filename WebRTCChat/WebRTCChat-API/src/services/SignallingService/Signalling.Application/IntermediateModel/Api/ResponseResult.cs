namespace Signalling.Application.IntermediateModel.Api;
using System.Collections.Generic;

public class ResponseResult<TResult>
{
	public IEnumerable<string> Errors { get; set; }

	public TResult Result { get; set; }

	public bool Succeeded { get; set; }

}
