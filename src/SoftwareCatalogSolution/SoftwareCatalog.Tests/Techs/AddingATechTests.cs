using Alba;
using Alba.Security;
using SoftwareCatalog.Api.Techs;
using System.Security.Claims;

namespace SoftwareCatalog.Tests.Techs;

[Trait("Category", "System")]
public class AddingATechTests
{
    [Fact]
    public async Task CanAddATech()
    {
        var fakeIdentity = new AuthenticationStub().WithName("manager-user")
            .With(new Claim(ClaimTypes.Role, "manager"))
            .With(new Claim(ClaimTypes.Role, "software-center"));

        var host = await AlbaHost.For<Program>(fakeIdentity);

        var requestModel = new TechCreateModel
        {
            Name = "Alice Johnson",
            Email = "alice@techcompany.com",
            Sub = "alice123"
        };

        var postResponse = await host.Scenario(api =>
        {
            api.Post.Json(requestModel).ToUrl("/techs");
            api.StatusCodeShouldBe(201);
        });

        var location = postResponse.Context.Response.Headers.Location.ToString();
        var postBody = postResponse.ReadAsJson<TechDetailsResponseModel>();

        Assert.NotNull(postBody);

        var getResponse = await host.Scenario(api =>
        {
            api.Get.Url(location);
        });

        var getBody = getResponse.ReadAsJson<TechDetailsResponseModel>();
        Assert.NotNull(getBody);

        Assert.Equal(postBody, getBody);
    }

    [Fact]
    public async Task InputsAreValidated()
    {
        var fakeIdentity = new AuthenticationStub().WithName("manager-user")
            .With(new Claim(ClaimTypes.Role, "manager"))
            .With(new Claim(ClaimTypes.Role, "software-center"));

        var host = await AlbaHost.For<Program>(fakeIdentity);

        var postResponse = await host.Scenario(api =>
        {
            api.Post.Json(new { }).ToUrl("/techs");
            api.StatusCodeShouldBe(400);
        });
    }
}
