using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Tdd.API.Config;
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
        var endpoint = "https://example.com";
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var sut = new UsersService(httpClient, config);
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
    [Fact]
    public async Task GetAllUsers_WhenHits404_ReturnsEmptyListOfUsers()
    {
        var expectedResponse = UsersFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
        var httpClient = new HttpClient(handlerMock.Object);
        var endpoint = "https://example.com";
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var sut = new UsersService(httpClient, config);
        //Act
       var result = await sut.GetAllUsers();
        //Assert
        //Verify HTTP is made
        result.Count.Should().Be(0);
    }
    
    [Fact]
    public async Task GetAllUsers_WhenCalled_ReturnListOfUsersOfExpectedSize()
    {
        var expectedResponse = UsersFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        var endpoint = "https://example.com";
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var sut = new UsersService(httpClient, config);
        //Act
        var result = await sut.GetAllUsers();
        //Assert
        result.Count.Should().Be(expectedResponse.Count);
    }
    
    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalUrl()
    {
        var expectedResponse = UsersFixture.GetTestUsers();
        var endpoint = "https://example.com/users";
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicResourceList(expectedResponse, endpoint);
        var httpClient = new HttpClient(handlerMock.Object);

        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var sut = new UsersService(httpClient, config);
        //Act
        var result = await sut.GetAllUsers();

        var uri = new Uri(endpoint);
        //Assert
        handlerMock.Protected().Verify("SendAsync",
            Times.Exactly(1),
            ItExpr
                .Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get 
                                               && req.RequestUri == uri),
            ItExpr.IsAny<CancellationToken>());
    }
}