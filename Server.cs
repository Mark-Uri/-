using System.Net;
using System.Net.Sockets;
using System.Text;

class Server
{
    static void Main()
    {

        Console.Title = "SERVER (Listener)";
        int listenPort = 8888;



        UdpClient listener = new UdpClient(listenPort);
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);



        Console.WriteLine("Сервер запущен Ожидаю broadcast-запросы...");

        while (true)
        {

            byte[] data = listener.Receive(ref groupEP);
            string message = Encoding.UTF8.GetString(data);



            Console.WriteLine($"Получено: '{message}' от {groupEP.Address}");

            if (message == "DISCOVER_DEVICES")
            {
                string response = $"DEVICE_RESPONSE:{Dns.GetHostName()}|{groupEP.Address}";


                byte[] responseBytes = Encoding.UTF8.GetBytes(response);

                listener.Send(responseBytes, responseBytes.Length, groupEP);

                Console.WriteLine("Ответ отправлен");
            }
        }
    }
}
