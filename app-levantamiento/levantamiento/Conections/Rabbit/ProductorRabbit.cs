
using System.Text;
using RabbitMQ.Client;


namespace levantamiento.Conections.rabbit
{
    public class ProductorRabbit: IProductorRabbit
    {

        public Routings _routing;
        public Exchanges _exchange;
        private ConnectionFactory factory = new ConnectionFactory() 
        {
            HostName =  "localhost" 
        };

        public ProductorRabbit (Exchanges exchange, Routings routing)
        {
            _exchange = exchange;
            _routing = routing;
        }

        public bool SendMessage(string instruccion, string contenido)
        {
            try
            {
                var comando = GenerateMessage(instruccion,contenido);
                using(var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(
                            exchange: _exchange.ToString(),
                            type:ExchangeType.Direct, 
                            durable: true
                            );

                        BasicPublish(channel,comando);

                        Console.WriteLine($"[x] Enviando {comando}");
                    }
                    Console.WriteLine("mansaje enviado");
                }
            }catch(Exception ex)
            {
                Console.WriteLine("ocurrio un error al enviar el mensaje por RabbitMQ");
            }
            return true;
            
        }

        private string GenerateMessage(string keyword, string content)
        {
            return keyword + ":" + content;
        }

        //metodo para crear el modelo
        private IModel BasicPublish(IModel channel, string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            IBasicProperties props = channel.CreateBasicProperties();
            props.ContentType = "text/plain";
            props.DeliveryMode = 2;

            channel.BasicPublish(
                exchange: _exchange.ToString(), 
                routingKey: _routing.ToString(), 
                basicProperties: props, 
                body:body
                );
            return channel;
        }

        
    }


}
