using FluentValidation;
using SoftwareCatalog.Api.Techs.Endpoints;

namespace SoftwareCatalog.Api.Techs;

public static class Extensions
{
    public static IServiceCollection AddTechs(this IServiceCollection services)
    {
        services.AddScoped<IValidator<TechCreateModel>, TechCreateModelValidator>();

        services.AddAuthorizationBuilder()
            .AddPolicy("canAddTechs", p =>
            {
                p.RequireRole("software-center");
                p.RequireRole("manager");
            });

        return services;
    }

    public static IEndpointRouteBuilder MapTechs(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("techs")
            .WithTags("Software Center Techs")
            .WithDescription("The API Techs for the Software Center");

        group.MapPost("/", AddingATech.CanAddTechAsync).RequireAuthorization("canAddTechs");
        group.MapGet("/", GettingTechs.GetTechsAsync);

        return group;
    }
}
