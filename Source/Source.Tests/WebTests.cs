using System.Net;
using Aspire.Hosting;
using Startup;

namespace Source.Tests;

public class WebTests
{
    [Fact]
    public async Task GetWebResourceRootReturnsOkStatusCode()
    {
        // Arrange
        IDistributedApplicationTestingBuilder appHost = await DistributedApplicationTestingBuilder.CreateAsync<AspireProgram>();
        await using DistributedApplication app = await appHost.BuildAsync();
        await app.StartAsync();

        // Act
        HttpClient httpClient = app.CreateHttpClient("webfrontend");
        HttpResponseMessage response = await httpClient.GetAsync("/");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
