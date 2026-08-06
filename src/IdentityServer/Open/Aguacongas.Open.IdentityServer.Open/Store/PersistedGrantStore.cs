using Aguacongas.Open.IdentityServer.Store.Entity;
using Open.IdentityServer.Extensions;
using Open.IdentityServer.Models;
using Open.IdentityServer.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Open.IdentityServer.IdentityServerConstants;

namespace Aguacongas.Open.IdentityServer.Store
{
    public class PersistedGrantStore : IPersistedGrantStore
    {
        private readonly IAdminStore<Entity.AuthorizationCode> _authorizationCodeStore;
        private readonly IAdminStore<ReferenceToken> _referenceTokenStore;
        private readonly IAdminStore<Entity.RefreshToken> _refreshTokenStore;
        private readonly IAdminStore<UserConsent> _userConsentStore;

        public PersistedGrantStore(
            IAdminStore<Entity.AuthorizationCode> authorizationCodeStore,
            IAdminStore<ReferenceToken> referenceTokenStore,
            IAdminStore<Entity.RefreshToken> refreshToken,
            IAdminStore<UserConsent> userConsentStore)
        {
            _authorizationCodeStore = authorizationCodeStore ?? throw new ArgumentNullException(nameof(authorizationCodeStore));
            _referenceTokenStore = referenceTokenStore ?? throw new ArgumentNullException(nameof(referenceTokenStore));
            _refreshTokenStore = refreshToken ?? throw new ArgumentNullException(nameof(refreshToken));
            _userConsentStore = userConsentStore ?? throw new ArgumentNullException(nameof(userConsentStore));
        }

        public Task<IEnumerable<PersistedGrant>> GetAllAsync(PersistedGrantFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task<PersistedGrant> GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAllAsync(PersistedGrantFilter filter)
        {
            filter.Validate();

            var grantTypes = FilterGrantTypes(filter);
            var query = GetOdataFilter(filter);

            foreach (var type in grantTypes)
            {
                switch (type)
                {
                    case PersistedGrantTypes.AuthorizationCode:
                        await DeleteAllAsync(query, _authorizationCodeStore).ConfigureAwait(false);
                        break;
                    case PersistedGrantTypes.RefreshToken:
                        await DeleteAllAsync(query, _refreshTokenStore).ConfigureAwait(false);
                        break;
                    case PersistedGrantTypes.ReferenceToken:
                        await DeleteAllAsync(query, _referenceTokenStore).ConfigureAwait(false);
                        break;
                    case PersistedGrantTypes.UserConsent:
                        await DeleteAllAsync(query, _userConsentStore).ConfigureAwait(false);
                        break;
                    default: throw new InvalidOperationException($"Grant type '{type}' unsupported.");
                }
            }
        }

        public Task RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task StoreAsync(PersistedGrant grant)
        {
            throw new NotImplementedException();
        }

        private static string GetOdataFilter(PersistedGrantFilter filter)
        {
            filter.Validate();

            var filterList = new List<string>();

            if (filter.SessionId is not null)
            {
                filterList.Add($"{nameof(IGrant.SessionId)} eq '{filter.SessionId}'");
            }
            if (filter.SubjectId is not null)
            {
                filterList.Add($"{nameof(IGrant.UserId)} eq '{filter.SubjectId}'");
            }

            var clientId = filter.ClientId;
            if (clientId is not null)
            {
                filterList.Add($"{nameof(IGrant.ClientId)} eq '{filter.ClientId}'");
            }

            return string.Join(" and ", filterList);
        }

        private static List<string> FilterGrantTypes(PersistedGrantFilter filter)
        {
            var responseList = new List<string>
            {
                PersistedGrantTypes.AuthorizationCode,
                PersistedGrantTypes.ReferenceToken,
                PersistedGrantTypes.RefreshToken,
                PersistedGrantTypes.UserConsent,
            };

            var type = filter.Type;
            if (type is not null)
            {
                responseList = responseList.Where(gt => gt == type).ToList();
            }

            return responseList;
        }

        private static async Task DeleteAllAsync<T>(string query, IAdminStore<T> store) where T : class, IEntityId
        {
            var response = await store.GetAsync(new PageRequest
            {
                Filter = query,
                Select = nameof(IEntityId.Id)
            }).ConfigureAwait(false);
            foreach (var id in response.Items.Select(e => e.Id))
            {
                await store.DeleteAsync(id);
            }
        }
    }
}
