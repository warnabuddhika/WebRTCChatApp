namespace UserManagement.API.Configurations;

using IdentityServer4.Models;

public class Config
{
	public static IEnumerable<IdentityResource> GetIdentityResources()
	{
		return new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Email(),
				new IdentityResources.Profile(),
				new IdentityResource
				{
					Name = "role",
					UserClaims = new List<string> {"role"}
				}
			};
	}

	public static IEnumerable<ApiResource> GetApiResources()
	{
		return new List<ApiResource>
			{
				new ApiResource("Messaging.API", "Messaging API"),
				new ApiResource("Signalling.API", "Signalling API")
				{
					UserClaims = new List<string> {"role", "openid", "profile", "email", "webrtc.scope" }
				}
			};
	}

	public static IEnumerable<ApiScope> GetApiScopes()
	{
		return new ApiScope[]
		{
				new ApiScope("webrtc.scope")
		};
	}
}
