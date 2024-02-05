namespace Signalling.Application.Services.IdentityUsers;

using Signalling.Application.IntermediateModel;
using Signalling.Application.IntermediateModel.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IIdentityUserService
{
	Task<ResponseResult<ApplicationUser>> GetUserAsync(string id);
	Task<ApplicationUser> GetCurrentUserAsync();
	Task<ResponseResult<ApplicationUser>> GetCurrentUserResultAsync();
	Task<ApplicationUser> GetUserByRocbnUserIdAsync(int? id);
	Task<ApplicationUser> GetCurrentUserDetailAsync();
}
