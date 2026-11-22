using CUPPEP.API.Infrastructure;
using CUPPEP.Application.Features.Extensions.StudentAchievements;
using CUPPEP.Application.Features.Students.Queries.GetStudentByCode;
using CUPPEP.Application.Features.Students.Queries.GetStudentsIncremental;
using CUPPEP.Domain.Common;
using CUPPEP.Domain.Entities.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Shouldly;

namespace CUPPEP.Application.UnitTests;

[TestFixture]
public sealed class IntegrationApiTests
{
    [Test]
    public void GetStudentsIncrementalValidator_WhenPageSizeExceedsMax_ShouldFail()
    {
        var validator = new GetStudentsIncrementalValidator();
        var result = validator.Validate(new GetStudentsIncrementalQuery(null, Page: 1, PageSize: 1500));

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.PropertyName == nameof(GetStudentsIncrementalQuery.PageSize));
    }

    [Test]
    public void GetStudentByCodeValidator_WhenCodeIsBlank_ShouldFail()
    {
        var validator = new GetStudentByCodeValidator();
        var result = validator.Validate(new GetStudentByCodeQuery("   "));

        result.IsValid.ShouldBeFalse();
    }

    [Test]
    public void StudentAchievementExt_WithNegativeGrant_ShouldThrowDomainException()
    {
        Should.Throw<DomainException>(() =>
            new StudentAchievementExt("6433012533", 2569, 6, "Competition", "1st Prize", new DateOnly(2026, 2, 18), -100m));
    }

    [Test]
    public void CreateStudentAchievementValidator_WithValidCommand_ShouldPass()
    {
        var validator = new CreateStudentAchievementValidator();
        var result = validator.Validate(new CreateStudentAchievementCommand(
            "6433012533",
            2569,
            6,
            "14th National Patient Counseling Competition",
            "1st Prize",
            new DateOnly(2026, 2, 18),
            5000m));

        result.IsValid.ShouldBeTrue();
    }

    [Test]
    public async Task ApiKeyAuthMiddleware_WhenMissingOrInvalidKey_ShouldReturn401()
    {
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                [ApiKeyAuthMiddleware.ConfigSectionKey] = "super-secret-key"
            })
            .Build();

        var nextCalled = false;
        var middleware = new ApiKeyAuthMiddleware(_ => { nextCalled = true; return Task.CompletedTask; }, config);

        var missingCtx = new DefaultHttpContext();
        missingCtx.Request.Path = "/api/students";
        await middleware.InvokeAsync(missingCtx);
        missingCtx.Response.StatusCode.ShouldBe(StatusCodes.Status401Unauthorized);
        nextCalled.ShouldBeFalse();

        var wrongCtx = new DefaultHttpContext();
        wrongCtx.Request.Path = "/api/students";
        wrongCtx.Request.Headers[ApiKeyAuthMiddleware.HeaderName] = "wrong-key";
        await middleware.InvokeAsync(wrongCtx);
        wrongCtx.Response.StatusCode.ShouldBe(StatusCodes.Status401Unauthorized);
        nextCalled.ShouldBeFalse();

        var validCtx = new DefaultHttpContext();
        validCtx.Request.Path = "/api/students";
        validCtx.Request.Headers[ApiKeyAuthMiddleware.HeaderName] = "super-secret-key";
        await middleware.InvokeAsync(validCtx);
        nextCalled.ShouldBeTrue();
    }

    [Test]
    public async Task ApiKeyAuthMiddleware_ForHealthEndpoint_ShouldAllowAnonymous()
    {
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                [ApiKeyAuthMiddleware.ConfigSectionKey] = "super-secret-key"
            })
            .Build();

        var nextCalled = false;
        var middleware = new ApiKeyAuthMiddleware(_ => { nextCalled = true; return Task.CompletedTask; }, config);

        var ctx = new DefaultHttpContext();
        ctx.Request.Path = "/api/health";
        await middleware.InvokeAsync(ctx);

        nextCalled.ShouldBeTrue();
    }
}
