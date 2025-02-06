using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SoftwareCatalog.Api.Techs.Endpoints;

public static class AddingATech
{
    public static async Task<Results<Created<TechDetailsResponseModel>, BadRequest>> CanAddTechAsync(
        [FromBody] TechCreateModel request,
        [FromServices] IValidator<TechCreateModel> validator,
        [FromServices] IDocumentSession session,
        [FromServices] IHttpContextAccessor httpContextAccessor
    )
    {
        var user = httpContextAccessor.HttpContext?.User;
        var managerSub = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(managerSub))
        {
            return TypedResults.BadRequest();
        }

        var validations = await validator.ValidateAsync(request);
        if (!validations.IsValid)
        {
            return TypedResults.BadRequest();
        }

        var entity = new TechEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Sub = request.Sub,
            AddedBySub = managerSub
        };

        session.Store(entity);
        await session.SaveChangesAsync();

        var response = new TechDetailsResponseModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber
        };

        return TypedResults.Created($"/techs/{entity.Id}", response);
    }
}
