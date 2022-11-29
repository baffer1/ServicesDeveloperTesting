
using Alba;

using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace IntegrationTests.CoursesResource.Fixtures;

public class CoursesResourceFixture : IAsyncLifetime
 {
    public IAlbaHost AlbaHost = null;

    public async Task DisposeAsync()
    {
        await AlbaHost.DisposeAsync();
    }

    public async Task InitializeAsync()
    {
        AlbaHost = await Alba.AlbaHost.For<global::Program>(builder =>
        {

        });
    }
}
