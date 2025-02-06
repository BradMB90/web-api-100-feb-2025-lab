using Marten;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SoftwareCatalog.Api.Techs.Endpoints;

public static class GettingTechs
{
    public static async Task<Ok<IReadOnlyList<TechDetailsResponseModel>>> GetTechsAsync(IDocumentSession session)
    {
        var response = await session.Query<TechEntity>()
            .Select(t => new TechDetailsResponseModel
            {
                Id = t.Id,
                Name = t.Name,
                Email = t.Email,
                PhoneNumber = t.PhoneNumber
            })
            .ToListAsync();

        return TypedResults.Ok(response);
    }
}
