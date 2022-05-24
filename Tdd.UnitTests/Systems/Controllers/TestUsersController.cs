using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Tdd.API.Controllers;
using Tdd.API.Models;
using Tdd.API.Services;
using Tdd.UnitTests.Fixtures;
using Xunit;

namespace Tdd.UnitTests.Systems.Controllers;

public class TestUsersController
{
    [Fact]
    public async Task Get_OnSucess_ReturnsStatusCode200()
    {
        //Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService.Setup(service => service.GetAllUsers()).ReturnsAsync(UsersFixture.GetTestUsers());
        // sut = System under test
        var sut = new UsersController(mockUsersService.Object);
        //Act
        var result = (OkObjectResult)await sut.Get();
        //Asert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSucess_InvokesUserServicesExactlyOnce()
    {
        //Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService.Setup(service => service.GetAllUsers()).ReturnsAsync(new List<User>());
        var sut = new UsersController(mockUsersService.Object);
        //Act
        var result = await sut.Get();
        //Asert
        mockUsersService.Verify(service => service.GetAllUsers(), Times.Once);
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnListOfUsers()
    {
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService.Setup(service => service.GetAllUsers()).ReturnsAsync(UsersFixture.GetTestUsers());
        
        var sut = new UsersController(mockUsersService.Object);

        var result = await sut.Get();

        result.Should().BeOfType<OkObjectResult>();
        var objectResult = (OkObjectResult)result;
        objectResult.Value.Should().BeOfType<List<User>>();
    }
    
    [Fact]
    public async Task Get_OnNoUsersFound_Returns404()
    {
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService.Setup(service => service.GetAllUsers()).ReturnsAsync(new List<User>());
        
        var sut = new UsersController(mockUsersService.Object);

        var result = await sut.Get();

        result.Should().BeOfType<NotFoundResult>();
        var objectResult = (NotFoundResult)result;
        objectResult.StatusCode.Should().Be(404);
    }
}