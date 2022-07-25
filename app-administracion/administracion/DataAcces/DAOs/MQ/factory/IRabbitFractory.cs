namespace  administracion.DataAccess.rabbit
{
    public interface IFactoryRabbit
    {
        public ProductorRabbit createRabbitProducer(ProducerType producerType);
        public ConsumerRabbit createRabbitConsumer(ConsumerType consumerType);
    }

    public enum QueueNames
    {
        incidentes,
        talleres,
        solicitudes,
        requerimientos,
        proveedores
    }

    public enum Routings 
    //Es quien va a recibir el mensaje
    {
        taller,
        proveedor,
        perito,
        administrador
    }

    public enum Exchanges 
    //es el medio donde se vana dejar los mensajes en la cola
    {
        gerencia,
        levantamiento,
        repacaciones,
        partes
    }

    public enum ProducerType
    {
        ProduceTaller,
        ProcudeIncidente,
        ProduceProveedor,
        ProduceSolicitud,
        ProduceRequerimientos,
    }
    public enum ConsumerType
    {
        ConsumeIncidente,
        ConsumeTaller,
        ConsumeSolicitudes,
        ConsumeRequerimientos,
        ConsumeProveedores
    }


}