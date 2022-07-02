
using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using administracion.Persistence.Database;

namespace administracion.Conections.rabbit
{
    public enum Routings //Es quien va a recibir el mensaje
    {
        taller,
        proveedor,
        perito,
        administrador
    }

    public enum Exchanges
    {
        gerencia,
        levantamiento,
        repacaciones,
        partes
    }
    
    public class ProductorRabbit
    {
        private Routings _routing;
        private Exchanges _exchange;
        private ConnectionFactory factory = new ConnectionFactory() 
        {
            HostName =  "localhost" 
        };

        public ProductorRabbit (Exchanges exchange, Routings routing)
        {
            _exchange = exchange;
            _routing = routing;
        }
        private string GenerateMessage(string keyword, string content)
        {
            return keyword + ":" + content;
        }


        public bool SendMessage(Routings routing,string instruccion, string contenido)
        {
            try
            {
                var comando = GenerateMessage(instruccion,contenido);
                using(var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(
                            exchange:_exchange.ToString(),
                            type:ExchangeType.Direct, 
                            durable: true
                            );

                        BasicPublish(channel,comando, routing);

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

        //metodo para crear el modelo
        private IModel BasicPublish(IModel channel, string message, Routings routingkey)
        {
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange:"gerencia", 
                routingKey: routingkey.ToString(), 
                basicProperties: null, 
                body:body);
            return channel;
        }

        
    }
}
