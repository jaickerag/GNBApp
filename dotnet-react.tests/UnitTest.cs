using System;
using System.Collections.Generic;
using dotnet_react.Controllers;
using dotnet_react.Models;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Xunit;


namespace dotnet_react.tests
{
    public class DataControllerTest
    {
        [Fact]

        public void VerifyreRefresh()
        {
            // Arrange
            var controller = new DataController();

            // Act

            var result = controller.refresh();

            //Assert

            Assert.IsType<List<Transactions>>(result);

        }
    }
}
