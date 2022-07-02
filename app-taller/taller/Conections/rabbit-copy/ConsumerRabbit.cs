using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using taller.Conections.APIs;
using taller.Persistence.DAOs;

namespace taller2.Conections.rabbit
{
    public class ConsumerRabbit 
    {
        private ConnectionFactory factory = new ConnectionFactory() {
            HostName = "localhost"
        };

        public void startReceiverAdmin(string routing)
        {
            while(true)
            {
                using(var connection=factory.CreateConnection())
                {
                    using(var channel=connection.CreateModel())
                    {
                        
                        //Nota = el parametro exchange no se puede le puede cambiar el valor con varibles externar al metodo por algun motivo. Lanzara una excepcion si se hace asi.
                        channel.ExchangeDeclare(exchange:"administracion2",type:ExchangeType.Direct);

                        var queueName = channel.QueueDeclare().QueueName;

                        channel.QueueBind(queue:queueName, exchange:"administracion2", routingKey:routing);
                        Console.WriteLine(" [*] Waiting for messages.");

                        var consumer=new EventingBasicConsumer(channel);

                        consumer.Received += (model, ea)=>
                        {
                            var body = ea.Body.ToArray();
                            var mensaje = Encoding.UTF8.GetString(body);
                            Console.WriteLine($"[X] Received {mensaje}");
                            CallFunction(mensaje);
                        };

                        channel.BasicConsume(queue:queueName, autoAck: true, consumer: consumer);

                        Console.WriteLine("Press any key to exit...");
                        Console.ReadLine();
                    }
                }
            }
        }
    
        public async Task CallFunction(string menssage)
        {
            List<String> Comando = menssage.Split(":").ToList();
            switch(Comando[0]){
                case "registrar":
                    ServicioAPI API = new ServicioAPI();
                    var data = await API.GetTaller(Guid.Parse(menssage.Split(':')[1]));
                    

                    break;
                case "2":
                    Console.WriteLine("2");
                    break;
                case "3":
                    Console.WriteLine("3");
                    break;
                default:
                    Console.WriteLine("default");
                    break;
            }
        }
    }
}