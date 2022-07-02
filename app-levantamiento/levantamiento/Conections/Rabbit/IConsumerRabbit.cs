namespace levantamiento.Conections.rabbit
{
    public interface IConsumerRabbit 
    {

        public Task<List<string>> startReceiverAdmin();
    }
    public enum QueueNames
    {
        incidente,
        solicitud,
        cotizacion,
    }


}