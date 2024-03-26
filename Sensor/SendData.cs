using System;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Sensor
{
    public class SendData : WebSocketBehavior
    {
        private readonly int _periodDefault = 5;
        private readonly int _convertMs = 700;
        private int _period;
        private Random _rnd;
        private bool _isPeriodSet = false;

        public SendData()
        {
            _period = _periodDefault;
            _rnd = new Random();
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine($"Received message from client: {e.Data}");

            if (!_isPeriodSet && int.TryParse(e.Data, out int period))
            {
                lock (this) 
                {
                    _period = period;
                    _isPeriodSet = true;
                    Task.Run(() => GenerateAndSendData());
                }
            }
        }

        private async Task GenerateAndSendData()
        {
            while (_isPeriodSet)
            {
                double temp = _rnd.NextDouble() * (30 - 15) + 15;
                //Sessions.Broadcast(temp.ToString());
                Send(temp.ToString());  
                Console.WriteLine($"Waiting for {_period * _convertMs} milliseconds");
                await Task.Delay(_period * _convertMs);
            }
        }
    }
}
