using System.Security.Cryptography;

namespace Aguacongas.Open.IdentityServer.KeysRotation
{
    public class ECDsaKeyInfo
    {
        public byte[] D { get; set; }
        public ECPoint Q { get; set; }
        public string Curve { get; set; }
    }
}
