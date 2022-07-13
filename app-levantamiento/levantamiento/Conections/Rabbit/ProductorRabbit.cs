
using System.Text;
using RabbitMQ.Client;


namespace levantamiento.Conections.rabbit
{
    public class ProductorRabbit: IProductorRabbit
    {

        public Routings _routing;
        private Exchanges _exchange = Exchanges.levantamiento;
        private ConnectionFactory factory = new ConnectionFactory() 
        {
            HostName =  "localhost" 
        };

        public bool SendMessage(Routings routing, string instruccion, string contenido)
                {
            _routing = routing;
            try
            {
                var comando = GenerateMessage(instruccion,contenido);
                using(var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(
                            exchange: _exchange.ToString(),
                            type: ExchangeType.Direct.ToString(), 
                            durable: true
                            );

                        var body = Encoding.UTF8.GetBytes(comando);

                        IBasicProperties props = channel.CreateBasicProperties();
                        props.ContentType = "text/plain";
                        props.DeliveryMode = 2;

                        channel.BasicPublish(
                            exchange: _exchange.ToString(), 
                            routingKey: _routing.ToString(), 
                            basicProperties: props, 
                            body:body
                            );
                    }
                }
            }catch(Exception)
            {
                Console.WriteLine("ocurrio un error al enviar el mensaje por RabbitMQ");
            }
            return true;
            
        }

        private string GenerateMessage(string keyword, string content)
        {
            return keyword + ":" + content;
        }
        

        
    }


}
