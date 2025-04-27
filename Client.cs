using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;



class Client
{
    static void Main()
    {

        Console.Title = "CLIENT (Broadcaster)";
        int broadcastPort = 8888;

        string broadcastAddress = "255.255.255.255";




        UdpClient client = new UdpClient();
        client.EnableBroadcast = true;
        IPEndPoint broadcastEP = new IPEndPoint(IPAddress.Parse(broadcastAddress), broadcastPort);

        string message = "DISCOVER_DEVICES";
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);




        client.Send(messageBytes, messageBytes.Length, broadcastEP);


        Console.WriteLine("Broadcast-запрос отправлен Ожидаю ответы...");

        client.Client.ReceiveTimeout = 3000;

        try
        {
            while (true)
            {

                IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref sender);

                string response = Encoding.UTF8.GetString(data);


                if (response.StartsWith("DEVICE_RESPONSE:"))
                    Console.WriteLine("Обнаружено устройство: " + response.Replace("DEVICE_RESPONSE:", ""));
            }
        }
        catch (SocketException)
        {
            Console.WriteLine("Ожидание завершено Поиск окончен");
        }

        client.Close();
    }
}
