// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store.Entity;
using Open.IdentityServer.Models;
using Open.IdentityServer.Stores;
using Open.IdentityServer.Stores.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Aguacongas.Open.IdentityServer.Store
{
    public class UserConsentStore : GrantStore<UserConsent, Consent>, IUserConsentStore
    {
        public UserConsentStore(IAdminStore<UserConsent> store, IPersistentGrantSerializer serializer)
            : base(store, serializer)
        {
        }

        public Task<Consent> GetUserConsentAsync(string subjectId, string clientId)
            => GetAsync(subjectId, clientId);

        public Task RemoveUserConsentAsync(string subjectId, string clientId)
            => RemoveAsync(subjectId, clientId);

        public Task StoreUserConsentAsync(Consent consent)
            => StoreAsync(consent, consent.Expiration);

        protected override string GetClientId(Consent dto)
            => dto?.ClientId;

        protected override string GetSubjectId(Consent dto)
            => dto?.SubjectId;
    }
}
