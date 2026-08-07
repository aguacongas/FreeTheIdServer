// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store;
using Aguacongas.Open.IdentityServer.Store.Entity;
using Moq;
using Open.IdentityServer.Stores.Serialization;
using System;
using System.Threading.Tasks;
using Xunit;
using ISModels = Open.IdentityServer.Models;

namespace Aguacongas.Open.IdentityServer.Http.Store.Test;

public class DeviceFlowStoreTest
{
    [Fact]
    public async Task FindByDeviceCodeAsync_should_call_store_GetAsync()
    {
        CreateSut(out Mock<IAdminStore<DeviceCode>> storeMock,
            out DeviceFlowStore sut);

        storeMock.Setup(m => m.GetAsync(It.IsAny<PageRequest>()))
            .ReturnsAsync(new PageResponse<DeviceCode>
            {
                Items = []
            })
            .Verifiable();

        await sut.FindByDeviceCodeAsync("test");

        storeMock.Verify(m => m.GetAsync(It.Is<PageRequest>(r => r.Filter == "Code eq 'test'")));

        storeMock.Setup(m => m.GetAsync(It.IsAny<PageRequest>()))
            .ReturnsAsync(new PageResponse<DeviceCode>
            {
                Count = 1,
                Items =
                [
                    new()
                ]
            })
            .Verifiable();

        await sut.FindByDeviceCodeAsync("test");

        storeMock.Verify(m => m.GetAsync(It.Is<PageRequest>(r => r.Filter == "Code eq 'test'")));
    }

    [Fact]
    public async Task FindByUserCodeAsync_should_call_store_GetAsync()
    {
        CreateSut(out Mock<IAdminStore<DeviceCode>> storeMock,
            out DeviceFlowStore sut);

        storeMock.Setup(m => m.GetAsync(It.IsAny<PageRequest>()))
            .ReturnsAsync(new PageResponse<DeviceCode>
            {
                Items = []
            })
            .Verifiable();

        await sut.FindByUserCodeAsync("test");

        storeMock.Verify(m => m.GetAsync(It.Is<PageRequest>(r => r.Filter == "UserCode eq 'test'")));

        storeMock.Setup(m => m.GetAsync(It.IsAny<PageRequest>()))
            .ReturnsAsync(new PageResponse<DeviceCode>
            {
                Count = 1,
                Items =
                [
                    new DeviceCode()
                ]
            })
            .Verifiable();

        await sut.FindByUserCodeAsync("test");

        storeMock.Verify(m => m.GetAsync(It.Is<PageRequest>(r => r.Filter == "UserCode eq 'test'")));
    }

    [Fact]
    public async Task RemoveDeviceCodeAsync_should_call_store_DeleteAsync()
    {
        CreateSut(out Mock<IAdminStore<DeviceCode>> storeMock,
            out DeviceFlowStore sut);

        storeMock.Setup(m => m.DeleteAsync(It.IsAny<string>())).Verifiable();
        storeMock.Setup(m => m.GetAsync(It.IsAny<PageRequest>()))
            .ReturnsAsync(new PageResponse<DeviceCode>
            {
                Count = 1,
                Items =
                [
                    new DeviceCode
                    {
                        Id = "id"
                    }
                ]
            })
            .Verifiable();

        await sut.RemoveByDeviceCodeAsync("test");

        storeMock.Verify(m => m.GetAsync(It.Is<PageRequest>(r => r.Filter == "Code eq 'test'")));
        storeMock.Verify(m => m.DeleteAsync(It.Is<string>(r => r == "id")));
    }

    [Fact]
    public async Task StoreDeviceCodeAsync_should_call_store_CreateAsync()
    {
        CreateSut(out Mock<IAdminStore<DeviceCode>> storeMock,
            out DeviceFlowStore sut);

        storeMock.Setup(m => m.CreateAsync(It.IsAny<DeviceCode>()))
            .ReturnsAsync(new DeviceCode())
            .Verifiable();

        await sut.StoreDeviceAuthorizationAsync("test", "test", new ISModels.DeviceCode());

        storeMock.Verify(m => m.CreateAsync(It.IsAny<DeviceCode>()));
    }

    [Fact]
    public async Task UpdateByUserCodeAsync_should_call_store_CreateAsync()
    {
        CreateSut(out Mock<IAdminStore<DeviceCode>> storeMock,
            out DeviceFlowStore sut);

        storeMock.Setup(m => m.UpdateAsync(It.IsAny<DeviceCode>()))
            .ReturnsAsync(new DeviceCode())
            .Verifiable();

        storeMock.Setup(m => m.GetAsync(It.IsAny<PageRequest>()))
            .ReturnsAsync(new PageResponse<DeviceCode>
            {
                Items =
                [
                    new DeviceCode
                    {
                        Id = "id"
                    }
                ]
            })
            .Verifiable();

        await sut.UpdateByUserCodeAsync("test", new ISModels.DeviceCode());

        storeMock.Verify(m => m.GetAsync(It.Is<PageRequest>(r => r.Filter == "UserCode eq 'test'")));
        storeMock.Verify(m => m.UpdateAsync(It.IsAny<DeviceCode>()));

        storeMock.Setup(m => m.GetAsync(It.IsAny<PageRequest>()))
            .ReturnsAsync(new PageResponse<DeviceCode>
            {
                Items = []
            })
            .Verifiable();

        await Assert.ThrowsAsync<InvalidOperationException>(() => sut.UpdateByUserCodeAsync("test", new ISModels.DeviceCode()));
    }

    private static void CreateSut(out Mock<IAdminStore<DeviceCode>> storeMock,
        out DeviceFlowStore sut)
    {
        storeMock = new Mock<IAdminStore<DeviceCode>>();
        var serializerMock = new Mock<IPersistentGrantSerializer>();
        sut = new DeviceFlowStore(storeMock.Object, serializerMock.Object);
    }
}