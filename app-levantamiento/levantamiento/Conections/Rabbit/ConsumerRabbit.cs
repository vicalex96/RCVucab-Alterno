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

        public void configureRabbit (Exchanges exchange, Routings routing, QueueNames queueName)
        {
            _exchange = exchange;
            _routing = routing;
            _queueName = queueName;
        }

        public List<string> Consume()
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

                    var consumer=new EventingBasicConsumer(channel);
                    consumer.Received +=  (model, ea)=>
                    {
                            var body = ea.Body.ToArray();
                            messages.Add(Encoding.UTF8.GetString(body));
                    };

                    channel.BasicConsume(
                        queue:_queueName.ToString(), 
                        autoAck: false, 
                        consumer: consumer
                        );
                }
            }
            return messages;
        }

    }
}