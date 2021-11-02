using DotNetty.Buffers;
using dotNetty_kcp;
using fec;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pea.Networking.Servers
{

    public class ServerSocketKcp : IServerSocket, KcpListener
    {
        public event PeerActionHandler Connected;
        public event PeerActionHandler OnConnected;
        public event PeerActionHandler Disconnected;
        public event PeerActionHandler OnDisconnected;

        public Task Listen(int port)
        {
            ChannelConfig channelConfig = new ChannelConfig();
            channelConfig.initNodelay(true, 40, 2, true);
            channelConfig.Sndwnd = 512;
            channelConfig.Rcvwnd = 512;
            channelConfig.Mtu = 512;
            channelConfig.FecDataShardCount = 3;
            channelConfig.FecParityShardCount = 1;
            channelConfig.AckNoDelay = true;
            channelConfig.TimeoutMillis = 10000;
            channelConfig.UseConvChannel = true;
            KcpServer kcpServer = new KcpServer();
            kcpServer.init(Environment.ProcessorCount, this, channelConfig, 20003);
            return Task.CompletedTask;
        }

        public void onConnected(Ukcp ukcp)
        {

        }

        public void handleReceive(IByteBuffer byteBuf, Ukcp ukcp)
        {
            short curCount = byteBuf.GetShort(byteBuf.ReaderIndex);

            ukcp.write(byteBuf);
            if (curCount == -1)
            {
                ukcp.close();
            }
        }

        public void handleException(Exception ex, Ukcp ukcp)
        {
            throw new NotImplementedException();
        }

        public void handleClose(Ukcp ukcp)
        {
            Console.WriteLine(Snmp.snmp.ToString());
            Snmp.snmp = new Snmp();
        }

        public async Task Stop()
        {
            throw new NotImplementedException();
        }
    }
}
