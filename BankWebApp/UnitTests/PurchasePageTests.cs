using BankWebApp.Areas.Identity.Data;
using BankWebApp.Data;
using BankWebApp.Models;
using BankWebApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Identity;

namespace BankWebApp.UnitTests
{
    public class PurchasePageTests
    {
        [Fact]
        public async Task OnPostAsync_ValidModel_RedirectsToIndexPage()
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance using an in-memory database and 
            // IServiceProvider that the context should resolve all of its 
            // services from.
            var builder = new DbContextOptionsBuilder<BankWebAppContext>()
                .UseInMemoryDatabase("InMemoryDb")
                .UseInternalServiceProvider(serviceProvider);

            var mockUserStore = new Mock<IUserStore<BankWebAppUser>>();
            var mockUserManager = new Mock<UserManager<BankWebAppUser>>(
                mockUserStore.Object, null, null, null, null, null, null, null, null);
            var mockContext = new Mock<BankWebAppContext>(builder.Options);
            var model = new PurchaseModel(mockContext.Object, mockUserManager.Object);
            model.Transaction = new Transaction { Amount = 100, ItemName = "Test Item" };

            // Act
            var result = await model.OnPostAsync();

            // Assert
            var redirectToPageResult = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("./Index", redirectToPageResult.PageName);
        }
    }
}
