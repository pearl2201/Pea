using Coldairarrow.DotNettySocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pea.Networking
{
    public class ServerSocketWs : IServerSocket
    {
        public event PeerActionHandler Connected;
        public event PeerActionHandler OnConnected;
        public event PeerActionHandler Disconnected;
        public event PeerActionHandler OnDisconnected;

        private IWebSocketServer theServer;
        public async Task Listen(int port)
        {
            theServer = await SocketBuilderFactory.GetWebSocketServerBuilder(port)
               .OnConnectionClose((server, connection) =>
               {
                   Console.WriteLine($"连接关闭,连接名[{connection.ConnectionName}],当前连接数:{server.GetConnectionCount()}");
               })
               .OnException(ex =>
               {
                   Console.WriteLine($"服务端异常:{ex.Message}");
               })
               .OnNewConnection((server, connection) =>
               {
                   connection.ConnectionName = $"名字{connection.ConnectionId}";
                   Console.WriteLine($"新的连接:{connection.ConnectionName},当前连接数:{server.GetConnectionCount()}");
               })
               .OnRecieve((server, connection, msg) =>
               {
                   Console.WriteLine($"服务端:数据{msg}");
                   connection.Send(msg);
               })
               .OnSend((server, connection, msg) =>
               {
                   Console.WriteLine($"向连接名[{connection.ConnectionName}]发送数据:{msg}");
               })
               .OnServerStarted(server =>
               {
                   Console.WriteLine($"服务启动");
               }).BuildAsync();

            Console.ReadLine();
        }


        public Task Stop()
        {
            theServer.Close();
            return Task.CompletedTask;
        }
    }
}
