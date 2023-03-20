// Basics :
//https://www.delftstack.com/howto/csharp/csharp-udp-server/
// Srvr Sockets :
//https://blog.pantuza.com/artigos/o-que-sao-e-como-funcionam-os-sockets
// dotnet commands :
// https://dzone.com/refcardz/net-on-linux#:~:text=Thanks%20to%20.,system%2C%20regardless%20of%20operating%20system.
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace mini_telemetria;

public class SimpleUdpSrvr
{

    public static void Udp_Srvr()
    {
        // Inteiro que indica a qntd de bytes da mensagem recebida
        int recv;
        byte[] data = new byte[1024];
        IPEndPoint iped = new IPEndPoint(IPAddress.Any, 3333);
        Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        newsock.Bind(iped);

        while (true)
        {
            IPEndPoint Sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)(Sender);

            /*
            newsock.BeginReceive(data, 0, 1024, SocketFlags.None, new AsyncCallback(this.OnReceive), newsock)
              private void OnReceive(IAsyncResult ar)
                {
                    Socket s1 = (Socket)ar.AsyncState;
                    int x = s1.EndReceive(ar);
                    string message = System.Text.Encoding.ASCII.GetString(buffer, 0, x);
                    Console.WriteLine(message);
                    s1.BeginReceive(buffer, 0, 1024, SocketFlags.None, new AsyncCallback(this.OnReceive), s1);
                }   
            */

            Console.WriteLine("Esperando pelo cliente");

            // function only pass foward if receives a message
            recv = newsock.ReceiveFrom(data, ref Remote);

            Console.WriteLine("Mensgem recebida de {0}:", Remote.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

            string vel = Console.ReadLine();
            data = Encoding.ASCII.GetBytes(vel);
            newsock.SendTo(data, data.Length, SocketFlags.None, Remote);

            data = new byte[1024];
            recv = newsock.ReceiveFrom(data, ref Remote);

            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
            //newsock.SendTo(data, recv, SocketFlags.None, Remote);
        }
    }

    /*
    private byte[] value_variable()
    {
        Console.WriteLine("Escreva o campo que deseje alterar (max 5 caracteres):");
        string xVar = Console.ReadLine();
        byte[] xVar_by = new byte[5];
        if (xVar_by.Length == Encoding.ASCII.GetBytes(xVar).Length)
        { }
        else
            Console.Write("Tamanho do campo inválido");

        Console.WriteLine("Escreva o valor do campo (max 2 caracteres):");
        string xValue = Console.ReadLine();
        byte[] xValue_by = new byte[2];
        if (xValue_by.Length == Encoding.ASCII.GetBytes(xValue).Length)
        { }
        else
            Console.Write("Tamanho do valor inválido");

        byte[] valor = ;

        return valor;
    }
    */




}

