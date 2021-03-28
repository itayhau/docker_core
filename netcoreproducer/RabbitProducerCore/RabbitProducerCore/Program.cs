using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;

namespace RabbitProducerCore
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Wait 30 seconds ...");
                    Thread.Sleep(30 * 1000);
                    var factory = new ConnectionFactory() { HostName = "localhost" };
                    //factory.Port = 5677;
                    factory.HostName = "rabbit1";
                    using (var connection = factory.CreateConnection())
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: "flight_center_requests",
                                             durable: false,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);

                        for (int i = 0; i < 10; i++)
                        {
                            string message = $"{{ timestamp: '{DateTime.Now.ToString()}', resultqueue: '{Guid.NewGuid().ToString()}'" +
            $", methodid: 1, params: {{}} }}";

                            var body = Encoding.UTF8.GetBytes(message);
                            channel.BasicPublish(exchange: "",
                                                 routingKey: "flight_center_requests",
                                                 basicProperties: null,
                                                 body: body);
                            Console.WriteLine(" [x] Sent {0}", message);
                        }
                    }
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"not ready ... {ex}");
                }
            }

            Console.WriteLine(" Press [enter] to exit.");

        }
    }
}
