namespace administracion.Persistence.rabbit
{
    public class FactoryRabbit: IFactoryRabbit
    {
        private Exchanges _exchange;
        private Routings _routing;
        private QueueNames _queueName;

        public ProductorRabbit createRabbitProducer(ProducerType producerType)
        {
            if( producerType == ProducerType.ProcudeIncidente)
                return new  ProductorRabbit (
                    Exchanges.gerencia, 
                    Routings.perito
                );
            if( producerType == ProducerType.ProduceProveedor)
                return new  ProductorRabbit (
                    Exchanges.gerencia, 
                    Routings.proveedor
                );
            if( producerType == ProducerType.ProduceRequerimientos)
                return new  ProductorRabbit (
                    Exchanges.repacaciones, 
                    Routings.proveedor
                );
            if( producerType == ProducerType.ProduceSolicitud)
                return new  ProductorRabbit ( 
                    Exchanges.levantamiento, 
                    Routings.taller 
                );
            if( producerType == ProducerType.ProduceTaller)
                return new  ProductorRabbit (
                    Exchanges.gerencia, 
                    Routings.taller
                );
            
            throw new Exception("el tipo de productor no es valido");
        }

        public ConsumerRabbit createRabbitConsumer(ConsumerType consumerType)
        {
            if( consumerType == ConsumerType.ConsumeIncidente)
                return new  ConsumerRabbit (
                    Exchanges.gerencia, 
                    Routings.perito,
                    QueueNames.incidentes
                );
            if( consumerType == ConsumerType.ConsumeProveedores)
                return new  ConsumerRabbit (
                    Exchanges.gerencia, 
                    Routings.proveedor,
                    QueueNames.proveedores
                );
            if( consumerType == ConsumerType.ConsumeRequerimientos)
                return new  ConsumerRabbit (
                    Exchanges.repacaciones, 
                    Routings.proveedor,
                    QueueNames.requerimientos
                );
            if( consumerType == ConsumerType.ConsumeSolicitudes)
                return new  ConsumerRabbit (
                    Exchanges.levantamiento, 
                    Routings.taller,
                    QueueNames.solicitudes
                );
            if( consumerType == ConsumerType.ConsumeTaller)
                return new  ConsumerRabbit (
                    Exchanges.gerencia, 
                    Routings.taller,
                    QueueNames.talleres
                );
            
            throw new Exception("el tipo de consumidor no es valido");
        }
    }
}