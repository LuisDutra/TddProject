using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Tdd.API.Models;
using Tdd.API.Services;
using Tdd.UnitTests.Fixtures;
using Tdd.UnitTests.Helpers;
using Xunit;

namespace Tdd.UnitTests.Systems.Services;

public class TestUsersService
{
    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
    {
        //Arrange
        var expectedResponse = UsersFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        var sut = new UsersService(httpClient);
        //Act
        await sut.GetAllUsers();
        //Assert
        //Verify HTTP is made
        handlerMock.Protected()
            .Verify("SendAsync", 
            Times.Exactly(1), 
            ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
            ItExpr.IsAny<CancellationToken>());
    }
}