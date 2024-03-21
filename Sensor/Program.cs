using Sensor;
using WebSocketSharp.Server;

class Program
{

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Starting server");
            WebSocketServer server = new WebSocketServer("ws://127.0.0.1:9000");
            server.AddWebSocketService<SendData>("/Sensor");
            server.Start();
            Console.ReadKey();
            server.Stop();
        }
        
    }

}