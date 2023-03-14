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
            //inteiro que indica a qntd de bytes da mensagem recebida
            int recv;
            //cria um array de 1024 posicoes que informa a quantidade de bytes de uma msg em cada posicao
            byte[] data = new byte[1024];
            IPEndPoint iped = new IPEndPoint(IPAddress.Any, 9050);
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            //liga o IP local(endpoint) com o socket
            newsock.Bind(iped); 
            Console.WriteLine("Esperando pelo cliente");

            //esperando qualquer IP e a porta deixa-se a cargo do systema (0) 
            IPEndPoint Sender = new IPEndPoint(IPAddress.Any, 0);
            //guarda o IP do cliente
            EndPoint Remote = (EndPoint)(Sender); 

            //leitura da mensagem recebida pelo socket
            recv = newsock.ReceiveFrom(data, ref Remote);
            Console.WriteLine("Mensgem recebida de {0}:",Remote.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
            
            //envio de uma mensagem para o socket
            string bnv = "bem vindo ao teste do meu servidor";
            data = Encoding.ASCII.GetBytes(bnv);
            newsock.SendTo(data,data.Length,SocketFlags.None, Remote);

            while(true)
            {
                //variavel para guardar os bits recebidos pelo socket (buffer)
                data = new byte[1024];
                recv = newsock.ReceiveFrom(data, ref Remote);
                
                Console.WriteLine(Encoding.ASCII.GetString(data,0,recv));
                newsock.SendTo(data, recv, SocketFlags.None, Remote);
            }



        }
    }
}
