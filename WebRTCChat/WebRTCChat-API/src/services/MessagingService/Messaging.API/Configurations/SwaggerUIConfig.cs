namespace Users.API.Configurations
{
    public static class SwaggerUIConfig
    {
        public static void ConfigureSwaggerUI(this IApplicationBuilder app, IConfiguration configuration)
        {
            var keycloakVariables = configuration.GetSection("Keycloak");

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserAPI-V1");

                // Configure OAuth2 for Swagger UI with Keycloak
                c.OAuthClientId(keycloakVariables["resource"]);
                c.OAuthClientSecret(keycloakVariables["resource-secret"]);
                c.OAuthAppName("Swagger UI");
                c.OAuthUsePkce();

                // Set the Keycloak-specific parameters for authorization
                c.OAuthAdditionalQueryStringParams(new Dictionary<string, string>
            {
                { "redirect_uri", keycloakVariables["redirect_uri"] }, // Replace with your redirect URI
                { "client_id", keycloakVariables["resource"] }, // Replace with your Keycloak client ID
                { "response_mode", "code" }, // Depending on your Keycloak setup, this might need to be adjusted
                { "scope", "openid profile" }, // Replace with the desired scope
            });

                // Add the Keycloak-specific realm authorization URL
                c.OAuthAdditionalQueryStringParams(new Dictionary<string, string>
            {
                { "authorizationUrl", keycloakVariables["authorizationUrl"] }, //  your Keycloak realm URL
            });
            });
        }
    }
}
