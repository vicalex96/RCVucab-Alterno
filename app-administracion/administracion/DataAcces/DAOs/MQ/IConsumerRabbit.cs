using RabbitMQ.Client.Events;

namespace  administracion.DataAccess.rabbit
{
    public interface IConsumerRabbit 
    {
        public List<string> Consume();

    }

}