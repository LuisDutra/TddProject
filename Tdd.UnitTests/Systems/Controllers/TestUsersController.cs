using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Tdd.API.Controllers;
using Xunit;

namespace Tdd.UnitTests.Systems.Controllers;

public class TestUsersController
{
    [Fact]
    public async Task Get_OnSucess_ReturnsStatusCode200()
    {
        //Arrange
        var sut = new UsersController();
        //Act
        var result = (OkObjectResult)await sut.Get();
        //Asert
        result.StatusCode.Should().Be(200);
    }
}