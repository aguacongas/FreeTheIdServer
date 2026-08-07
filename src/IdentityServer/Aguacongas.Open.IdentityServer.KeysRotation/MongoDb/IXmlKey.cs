// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
namespace Aguacongas.Open.IdentityServer.KeysRotation.MongoDb
{
    public interface IXmlKey
    {
        string Id { get; set; }

        string Xml { get; set; }
        string FriendlyName { get; set; }
    }
}
