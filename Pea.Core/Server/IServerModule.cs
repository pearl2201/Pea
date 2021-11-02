using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pea.Core.Server
{
    public interface IServerModule
    {
        IEnumerable<Type> Dependencies { get; }
        IEnumerable<Type> OptionalDependencies { get; }

        /// <summary>
        /// Server, which initialized this module.
        /// Will be null, until the module is initialized
        /// </summary>
        ServerBehaviour Server { get; set; }

        void Initialize(IServer server);
    }
}
