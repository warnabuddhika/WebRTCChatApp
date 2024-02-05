namespace Messaging.API.Extensions;

using System.Security.Claims;

public static class ClaimsPrincipleExtensions
{
	public static string GetUsername(this ClaimsPrincipal user)
	{
		return user.FindFirst(ClaimTypes.Name)?.Value;
	}

	public static Guid GetUserId(this ClaimsPrincipal user)
	{
		return Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
	}
}
