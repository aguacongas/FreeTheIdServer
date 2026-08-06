// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store;
using Aguacongas.Open.IdentityServer.Store.Entity;
using Moq;
using Open.IdentityServer.Stores.Serialization;
using System.Threading.Tasks;
using Xunit;
using ISModels = Open.IdentityServer.Models;

namespace Aguacongas.Open.IdentityServer.Http.Store.Test;

public class UserConsentStoreTest
{
    [Fact]
    public async Task GetUserConsentAsync_should_call_store_GetAsync()
    {
        CreateSut(out Mock<IAdminStore<UserConsent>> storeMock,
            out UserConsentStore sut);

        storeMock.Setup(m => m.GetAsync(It.IsAny<PageRequest>()))
            .ReturnsAsync(new PageResponse<UserConsent>
            {
                Count = 1,
                Items =
                [
                    new UserConsent
                    {
                        Id = "id"
                    }
                ]
            })
            .Verifiable();

        await sut.GetUserConsentAsync("test", "test");

        storeMock.Verify(m => m.GetAsync(It.Is<PageRequest>(p => p.Filter == "UserId eq 'test' and ClientId eq 'test'")));
    }

    [Fact]
    public async Task RemoveUserConsentAsync_should_call_store_DeleteAsync()
    {
        CreateSut(out Mock<IAdminStore<UserConsent>> storeMock,
            out UserConsentStore sut);

        storeMock.Setup(m => m.DeleteAsync(It.IsAny<string>())).Verifiable();

        storeMock.Setup(m => m.GetAsync(It.IsAny<PageRequest>()))
            .ReturnsAsync(new PageResponse<UserConsent>
            {
                Count = 1,
                Items =
                [
                    new UserConsent
                    {
                        Id = "id"
                    }
                ]
            })
            .Verifiable();

        await sut.RemoveUserConsentAsync("test", "test");

        storeMock.Verify(m => m.GetAsync(It.Is<PageRequest>(p => p.Filter == "UserId eq 'test' and ClientId eq 'test'")));
        storeMock.Verify(m => m.DeleteAsync(It.Is<string>(r => r == "id")));
    }

    [Fact]
    public async Task StoreUserConsentAsync_should_call_store_CreateAsync()
    {
        CreateSut(out Mock<IAdminStore<UserConsent>> storeMock,
            out UserConsentStore sut);

        storeMock.Setup(m => m.CreateAsync(It.IsAny<UserConsent>()))
            .ReturnsAsync(new UserConsent())
            .Verifiable();

        storeMock.Setup(m => m.GetAsync(It.IsAny<PageRequest>()))
            .ReturnsAsync(new PageResponse<UserConsent>
            {
                Count = 0,
                Items = []
            })
            .Verifiable();

        await sut.StoreUserConsentAsync(new ISModels.Consent
        {
            ClientId = "test",
            SubjectId = "test"
        });

        storeMock.Verify(m => m.GetAsync(It.Is<PageRequest>(p => p.Filter == "UserId eq 'test' and ClientId eq 'test'")));
        storeMock.Verify(m => m.CreateAsync(It.IsAny<UserConsent>()));
    }

    private static void CreateSut(out Mock<IAdminStore<UserConsent>> storeMock,
        out UserConsentStore sut)
    {
        storeMock = new Mock<IAdminStore<UserConsent>>();
        var serializerMock = new Mock<IPersistentGrantSerializer>();
        sut = new UserConsentStore(storeMock.Object, serializerMock.Object);
    }
}
