using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Tdd.API.Controllers;
using Tdd.API.Models;
using Tdd.API.Services;
using Xunit;

namespace Tdd.UnitTests.Systems.Controllers;

public class TestUsersController
{
    [Fact]
    public async Task Get_OnSucess_ReturnsStatusCode200()
    {
        //Arrange
        var mockUsersService = new Mock<IUsersService>();
        mockUsersService.Setup(service => service.GetAllUsers()).ReturnsAsync(new List<User>());
        var sut = new UsersController(mockUsersService.Object);
        //Act
        var result = (OkObjectResult)await sut.Get();
        //Asert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSucess_InvokesUserServices()
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
}