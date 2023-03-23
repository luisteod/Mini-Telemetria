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
        byte[] data = new byte[128];
        byte[] data_recv = new byte[128];
        IPEndPoint iped = new IPEndPoint(IPAddress.Any, 3333);
        Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        newsock.Bind(iped);

        while (true)
        {
            IPEndPoint Sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)(Sender);

          
            while (true)
            {
                Console.WriteLine("Esperando pelo cliente");

                // function only pass foward if receives a message
                recv = newsock.ReceiveFrom(data_recv, ref Remote);
                Console.WriteLine("Conectado com : " + Remote.ToString());
                // manda de volta a mensagem enviada pelo cliente para confirmar a conexao
                newsock.SendTo(data_recv, SocketFlags.None, Remote);

                Console.WriteLine("Digite o que deseja : Escrever (A) ou Ler (B) ?");
                string opt = Console.ReadLine();
                
                if (opt == "A")
                {
                    SimpleUdpSrvr _data = new SimpleUdpSrvr();
                    //escreve os dados a serem enviados no buffer
                    _data.value_variable(ref data);
                    //concatena a opcao com os dados 
                    data = (Encoding.ASCII.GetBytes(opt)).Concat(data).ToArray();
                   
                }
                else if (opt == "B")
                {
                    opt = "B0000";
                    data = Encoding.ASCII.GetBytes(opt);
              
                }
                else
                {
                    Console.WriteLine("Digite uma opcao valida");
                    break;
                }
                
                //envio do datagrama
                newsock.SendTo(data,
                              data.Length,
                              SocketFlags.None,
                              Remote);

                data_recv = new byte[128];
                recv = newsock.ReceiveFrom(data_recv, ref Remote);
                Console.WriteLine(Encoding.ASCII.GetString(data_recv, 0, recv));
            }
        }
    }


    private void value_variable(ref byte[] data)
    {
        Console.WriteLine("Escreva o campo que deseje alterar (max 3 caracteres):");
        string xVar = Console.ReadLine();
        // Each char in string needs 2 bytes
        byte[] xVar_by = new byte[3];
        do
        {   
            Console.WriteLine(" bytes contidos na string : " + Encoding.ASCII.GetBytes(xVar).Length +
                          " / bytes disponiveis no buffer : " + xVar_by.Length);

            if (xVar_by.Length < Encoding.ASCII.GetBytes(xVar).Length)
            {
                Console.WriteLine("Tamanho do campo inválido, digite novamente :");
                xVar = Console.ReadLine();
            }

        } while (xVar_by.Length < Encoding.ASCII.GetBytes(xVar).Length);

        xVar_by = Encoding.ASCII.GetBytes(xVar);

        /* Getting the value */
                Console.WriteLine("Escreva o valor do campo (max 1 caracter):");
        string xValue = Console.ReadLine();
        byte[] xValue_by = new byte[1];
        do
        {
            Console.WriteLine(" bytes contidos na string : " + Encoding.ASCII.GetBytes(xValue).Length +
                          " / bytes disponiveis no buffer : " + xValue_by.Length);

            if (xValue_by.Length < Encoding.ASCII.GetBytes(xValue).Length)
            {
                Console.WriteLine("Tamanho do valor inválido , digite novamente :");
                xValue = Console.ReadLine();
            }

            } while (xValue_by.Length < Encoding.ASCII.GetBytes(xValue).Length);

        xValue_by = Encoding.ASCII.GetBytes(xValue);

        // ToArray converts an IEnumerable to Array
        data = xVar_by.Concat(xValue_by).ToArray();

    }





}

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