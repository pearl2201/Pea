using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pea.Core.Server
{
    public class PeerSecurityExtension
    {
        public int PermissionLevel;
        public string AesKey;
        public byte[] AesKeyEncrypted;
        public Guid UniqueGuid;

        public PeerSecurityExtension()
        {

        }
    }
}
