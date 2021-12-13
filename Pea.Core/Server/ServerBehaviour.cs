using Pea.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pea.Core.Server
{
    public class ServerBehaviour: IServer
    {
        public void AddModule(IServerModule module)
        {
            throw new NotImplementedException();
        }

        public void AddModuleAndInitialize(IServerModule module)
        {
            throw new NotImplementedException();
        }

        public bool ContainsModule(IServerModule module)
        {
            throw new NotImplementedException();
        }

        public bool InitializeModules()
        {
            throw new NotImplementedException();
        }

        T IServer.GetModule<T>()
        {
            throw new NotImplementedException();
        }

        public List<IServerModule> GetInitializedModules()
        {
            throw new NotImplementedException();
        }

        public List<IServerModule> GetUninitializedModules()
        {
            throw new NotImplementedException();
        }

        public void SetHandler(IPacketHandler handler)
        {
            throw new NotImplementedException();
        }

        public void SetHandler(short opCode, IncommingMessageHandler handler)
        {
            throw new NotImplementedException();
        }

        public IPeer GetPeer(int peerId)
        {
            throw new NotImplementedException();
        }
    }
}
