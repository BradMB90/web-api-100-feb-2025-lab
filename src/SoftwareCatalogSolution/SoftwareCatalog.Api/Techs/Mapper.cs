using Riok.Mapperly.Abstractions;

namespace SoftwareCatalog.Api.Techs;

[Mapper]
public static partial class TechMappers
{
    public static partial IQueryable<TechDetailsResponseModel> ProjectToModel(this IQueryable<TechEntity> entity);

    public static partial TechDetailsResponseModel MapToModel(this TechEntity entity);
}
