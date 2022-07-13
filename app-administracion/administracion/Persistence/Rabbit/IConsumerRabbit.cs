using RabbitMQ.Client.Events;

namespace administracion.Persistence.rabbit
{
    public interface IConsumerRabbit 
    {
        public List<string> Consume();

    }

}