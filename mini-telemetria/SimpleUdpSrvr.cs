//https://www.delftstack.com/howto/csharp/csharp-udp-server/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace mini_telemetria
{
    public class SimpleUdpSrvr
    {
        public static void Main()
        {
            int recv; // inteiro que indica a qntd de bytes da mensagem ah ser enviada
            byte[] data = new byte[1024]; // cria um array de 1024 posicoes que informa a quantidade de bytes de uma msg em cada posicao
            IPEndPoint iped = new IPEndPoint(IPAddress.Any, 9050);
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            newsock.Bind(iped); // liga o IP local(endpoint) com o socket
            Console.WriteLine("Esperando pelo cliente");

            IPEndPoint Sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)(Sender); // encontra o IP do emissor da msg


        }
    }
}
