namespace Signalling.Application.Services.Members;

using Signalling.Application.Dtos;
using Signalling.Application.IntermediateModel.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public  interface IMemberService
{
	Task<ResponseResult<MemberDto>> GetMember(string userName);
}
