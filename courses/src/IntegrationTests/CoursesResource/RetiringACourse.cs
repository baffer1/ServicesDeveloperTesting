using Alba;

using CoursesApi.IntegrationTests.CoursesResource.Fixtures;

namespace CoursesApi.IntegrationTests.CoursesResource;

public class RetiringACourse : IClassFixture<CoursesSeededResourceFixture>
{
    private readonly IAlbaHost _host;

    public RetiringACourse(CoursesSeededResourceFixture fixture)
    {
        _host = fixture.AlbaHost;
    }

    [Fact]
    public async Task CanRetireACourse()
    {
        await _host.Scenario(api =>
        {
            api.Get.Url("/courses/1");
            api.StatusCodeShouldBeOk();

        });

        await _host.Scenario(api =>
        {
            api.Delete.Url("/courses/1");
            api.StatusCodeShouldBe(204);

        });
        await _host.Scenario(api =>
        {
            api.Get.Url("/courses/1");
            api.StatusCodeShouldBe(404);

        });

    }


}
