using Microsoft.OpenApi.Models;

namespace SSO.Api.Extensions
{
    public static class SwaggerInfo
    {
        public static OpenApiInfo GetInfo()
        {

            var contact = new OpenApiContact()
            {
                Name = "FirstName LastName",
                Email = "user@example.com",
                Url = new Uri("http://www.example.com")
            };

            var license = new OpenApiLicense()
            {
                Name = "My License",
                Url = new Uri("http://www.example.com")
            };

            return new OpenApiInfo()
            {
                Version = "v1",
                Title = "Swagger Jwt Authentication Api",
                Description = "Swagger API Description",
                TermsOfService = new Uri("http://www.example.com"),
                Contact = contact,
                License = license
            };
        }
    }
}