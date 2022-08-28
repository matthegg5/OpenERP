using NUnit.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using OpenERP.Controllers.App;
using OpenERP.Data.Repositories;
using OpenERP.ErpDbContext.DataModel;

namespace OpenERP.Tests;

public class AccountControllerTests
{
    [NUnit.Framework.SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }

    [Test]
    public void CompanyRepositoryTest()
    {
        var companyRepoMock = new Mock<IRepository<OpenERP.ErpDbContext.DataModel.Company>>();

        var loggerACMock = new Mock<ILogger<AccountController>>();

        var contextAccessorMock = new Mock<IHttpContextAccessor>();
        var userPrincipalFactory = new Mock<IUserClaimsPrincipalFactory<OpenERP.ErpDbContext.DataModel.User>>();

        var userManagerMock = new Mock<UserManager<OpenERP.ErpDbContext.DataModel.User>>(
            Mock.Of<IUserStore<OpenERP.ErpDbContext.DataModel.User>>(),
        /* IOptions<IdentityOptions> optionsAccessor */null,
        /* IPasswordHasher<TUser> passwordHasher */null,
        /* IEnumerable<IUserValidator<TUser>> userValidators */null,
        /* IEnumerable<IPasswordValidator<TUser>> passwordValidators */null,
        /* ILookupNormalizer keyNormalizer */null,
        /* IdentityErrorDescriber errors */null,
        /* IServiceProvider services */null,
        /* ILogger<UserManager<TUser>> logger */null);

        var signInManagerMock = new Mock<SignInManager<OpenERP.ErpDbContext.DataModel.User>>(
            userManagerMock.Object,
            /* IHttpContextAccessor contextAccessor */contextAccessorMock,
            /* IUserClaimsPrincipalFactory<TUser> claimsFactory */ userPrincipalFactory,
            /* IOptions<IdentityOptions> optionsAccessor */null,
            /* ILogger<SignInManager<TUser>> logger */null,
            /* IAuthenticationSchemeProvider schemes */null,
            /* IUserConfirmation<TUser> confirmation */null);
            
        var accountController = new Mock<AccountController>(
            null,
            signInManagerMock,
            userManagerMock,
            null,
            companyRepoMock.Object
        );


        companyRepoMock.Verify(repository => repository.GetByID(It.IsAny<OpenERP.ErpDbContext.DataModel.Company>()), Times.AtLeastOnce);
    }
}
