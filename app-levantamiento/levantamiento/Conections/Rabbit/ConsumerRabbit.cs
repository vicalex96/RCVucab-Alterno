using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
namespace levantamiento.Conections.rabbit
{
    public class ConsumerRabbit: IConsumerRabbit
    {
        private Routings _routing;
        private Exchanges _exchange;
        private QueueNames _queueName;
        private ConnectionFactory factory = new ConnectionFactory() {
            HostName = "localhost"
        };

        public ConsumerRabbit (Exchanges exchange, Routings routing, QueueNames queueName)
        {
            _exchange = exchange;
            _routing = routing;
            _queueName = queueName;
        }

        public async Task<List<string>> startReceiverAdmin()
        {
            List<string> messages = new List<string>();
            using(var connection=factory.CreateConnection())
            {
                
                using(var channel=connection.CreateModel())
                {
                    
                    channel.ExchangeDeclare(
                        exchange: _exchange.ToString(),
                        type:ExchangeType.Direct, 
                        durable:true
                        );

                    channel.QueueBind(
                        queue:_queueName.ToString(), 
                        exchange: _exchange.ToString(), 
                        routingKey: _routing.ToString()
                        );

                    Console.WriteLine(" [*] Waiting for messages.");

                    var consumer=new EventingBasicConsumer(channel);
                    messages = await Consumir(consumer, messages);
                    
                    //await Task.Delay(1000);
                    channel.BasicConsume(
                        queue:_queueName.ToString(), 
                        autoAck: true, 
                        consumer: consumer
                        );

                }
            }

            return messages;
        }

        public async Task<List<string>> Consumir(EventingBasicConsumer consumer, List<string> messages) 
        {
            
            consumer.Received +=  (model, ea)=>
            {
                    var body = ea.Body.ToArray();
                    messages.Add(Encoding.UTF8.GetString(body));
            };
            
            return messages;
        }                   


    }


}