using System;
using System.Collections.Generic;
using Tdd.API.Models;

namespace Tdd.UnitTests.Fixtures;

public static class UsersFixture
{
    public static List<User> GetTestUsers() => new()
    {
        new User
        {
            Id = 1,
            Name = "Luis",
            Address = new Address()
            {
                Stree = "Castro Alves",
                City = "Dois Vizinhos",
                ZipCode = "12343"
            },
            Email = "luis@gmail.com"
        },
        new User
        {
            Id = 2,
            Name = "Luis2",
            Address = new Address()
            {
                Stree = "Castro Alves",
                City = "Tres Vizinhos",
                ZipCode = "12341"
            },
            Email = "luis2@gmail.com"
        },
        new User
        {
            Id = 3,
            Name = "Luis3",
            Address = new Address()
            {
                Stree = "Castro Alves",
                City = "Quatro Vizinhos",
                ZipCode = "12351"
            },
            Email = "luis3@gmail.com"
        }
    };
}