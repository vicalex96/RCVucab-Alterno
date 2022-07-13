using RabbitMQ.Client.Events;

namespace levantamiento.Conections.rabbit
{
    public interface IConsumerRabbit 
    {

        //public Task<List<string>> StartAsync(CancellationToken cancelToken = default);
        public void configureRabbit (Exchanges exchange, Routings routing, QueueNames queueName);
        public List<string> Consume();

    }
    public enum QueueNames
    {
        incidente,
        solicitud,
        cotizacion,
    }


}