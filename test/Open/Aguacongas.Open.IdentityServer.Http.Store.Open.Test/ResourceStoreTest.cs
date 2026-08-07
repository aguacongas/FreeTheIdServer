// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store;
using Aguacongas.Open.IdentityServer.Store.Entity;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Aguacongas.Open.IdentityServer.Http.Store.Test;

public class ResourceStoreTest
{
    [Fact]
    public async Task GetAllResourcesAsync_should_call_store_GetAsync()
    {
        CreateSut(out Mock<IAdminStore<ProtectResource>> apiStoreMock,
            out Mock<IAdminStore<IdentityResource>> identityStoreMock,
            out Mock<IAdminStore<ApiScope>> apiScopeStoreMock,
            out Mock<IAdminStore<ApiApiScope>> _,
            out ResourceStore sut);

        await sut.GetAllResourcesAsync();

        apiStoreMock.Verify(m => m.GetAsync(It.Is<PageRequest>(p =>
            p.Expand == $"{nameof(ProtectResource.ApiClaims)},{nameof(ProtectResource.Secrets)},{nameof(ProtectResource.ApiScopes)},{nameof(ProtectResource.Properties)},{nameof(ProtectResource.Resources)}")));
        identityStoreMock.Verify(m => m.GetAsync(It.Is<PageRequest>(p =>
            p.Expand == $"{nameof(IdentityResource.IdentityClaims)},{nameof(IdentityResource.Properties)},{nameof(IdentityResource.Resources)}")));
        apiScopeStoreMock.Verify(m => m.GetAsync(It.Is<PageRequest>(p =>
            p.Expand == $"{nameof(ApiScope.ApiScopeClaims)},{nameof(ApiScope.Properties)},{nameof(ApiScope.Resources)}")));
    }

    [Fact]
    public async Task FindIdentityResourcesByScopeNameAsync_should_call_store_GetAsync()
    {
        CreateSut(out Mock<IAdminStore<ProtectResource>> _,
            out Mock<IAdminStore<IdentityResource>> identityStoreMock,
            out Mock<IAdminStore<ApiScope>> _,
            out Mock<IAdminStore<ApiApiScope>> _,
            out ResourceStore sut);

        await sut.FindIdentityResourcesByScopeNameAsync(["test"]);

        identityStoreMock.Verify(m => m.GetAsync(It.Is<PageRequest>(p =>
            p.Filter == "Id eq 'test'")));
    }

    [Fact]
    public async Task FindApiResourcesByScopeNameAsync_should_call_store_GetAsync()
    {
        CreateSut(out Mock<IAdminStore<ProtectResource>> apiStoreMock,
            out Mock<IAdminStore<IdentityResource>> _,
            out Mock<IAdminStore<ApiScope>> _,
            out Mock<IAdminStore<ApiApiScope>> apiApiScopeStoreMock,
            out ResourceStore sut);

        apiApiScopeStoreMock.Setup(m => m.GetAsync(It.Is<PageRequest>(p =>
            p.Filter == $"{nameof(ApiApiScope.ApiScopeId)} eq 'test'"))).ReturnsAsync(new PageResponse<ApiApiScope>
            {
                Count = 1,
                Items =
                [
                    new ApiApiScope
                    {
                        ApiId = "test"
                    }
                ]
            });

        apiStoreMock.Setup(m => m.GetAsync(It.IsAny<PageRequest>())).ReturnsAsync(new PageResponse<ProtectResource>
        {
            Count = 1,
            Items =
                [
                    new ProtectResource
                    {
                        Id = "test"
                    }
                ]
        });

        await sut.FindApiResourcesByScopeNameAsync(["test"]);

        apiApiScopeStoreMock.Verify(m => m.GetAsync(It.Is<PageRequest>(p =>
            p.Filter == $"{nameof(ApiApiScope.ApiScopeId)} eq 'test'")));
        apiStoreMock.Verify(m => m.GetAsync(It.Is<PageRequest>(p =>
            p.Filter == $"{nameof(ProtectResource.Id)} eq 'test'")));
    }

    [Fact]
    public async Task FindApiResourcesByNameAsync_should_call_store_GetAsync()
    {
        CreateSut(out Mock<IAdminStore<ProtectResource>> apiStoreMock,
            out Mock<IAdminStore<IdentityResource>> _,
            out Mock<IAdminStore<ApiScope>> _,
            out Mock<IAdminStore<ApiApiScope>> _,
            out ResourceStore sut);

        await sut.FindApiResourcesByNameAsync(["test"]);

        apiStoreMock.Verify(m => m.GetAsync(It.Is<PageRequest>(p =>
            p.Expand == $"{nameof(ProtectResource.ApiClaims)},{nameof(ProtectResource.Secrets)},{nameof(ProtectResource.ApiScopes)},{nameof(ProtectResource.Properties)},{nameof(ProtectResource.Resources)}")));
    }

    [Fact]
    public async Task FindApiScopesByNameAsync_should_call_store_GetAsync()
    {
        CreateSut(out Mock<IAdminStore<ProtectResource>> _,
            out Mock<IAdminStore<IdentityResource>> _,
            out Mock<IAdminStore<ApiScope>> apiScopeStoreMock,
            out Mock<IAdminStore<ApiApiScope>> _,
            out ResourceStore sut);

        await sut.FindApiScopesByNameAsync(["test"]);

        apiScopeStoreMock.Verify(m => m.GetAsync(It.Is<PageRequest>(p =>
            p.Expand == $"{nameof(ApiScope.ApiScopeClaims)},{nameof(ApiScope.Properties)},{nameof(ApiScope.Resources)}")));
    }

    private static void CreateSut(out Mock<IAdminStore<ProtectResource>> apiStoreMock,
        out Mock<IAdminStore<IdentityResource>> identityStoreMock,
        out Mock<IAdminStore<ApiScope>> apiScopeStoreMock,
        out Mock<IAdminStore<ApiApiScope>> apiApiScopeStoreMock,
        out ResourceStore sut)
    {
        apiStoreMock = new Mock<IAdminStore<ProtectResource>>();
        identityStoreMock = new Mock<IAdminStore<IdentityResource>>();
        apiScopeStoreMock = new Mock<IAdminStore<ApiScope>>();
        apiApiScopeStoreMock = new Mock<IAdminStore<ApiApiScope>>();

        sut = new ResourceStore(apiStoreMock.Object, identityStoreMock.Object, apiScopeStoreMock.Object, apiApiScopeStoreMock.Object);

        apiStoreMock.Setup(m => m.GetAsync(It.IsAny<PageRequest>()))
            .ReturnsAsync(new PageResponse<ProtectResource>
            {
                Items = []
            }).Verifiable();

        identityStoreMock.Setup(m => m.GetAsync(It.IsAny<PageRequest>()))
            .ReturnsAsync(new PageResponse<IdentityResource>
            {
                Items = []
            }).Verifiable();

        apiScopeStoreMock.Setup(m => m.GetAsync(It.IsAny<PageRequest>()))
            .ReturnsAsync(new PageResponse<ApiScope>
            {
                Items = []
            }).Verifiable();
    }
}
