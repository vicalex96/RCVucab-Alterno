namespace levantamiento.Conections.rabbit
{
    
    public interface IProductorRabbit
    {
        public bool SendMessage(Routings routing, string instruccion, string contenido);
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

}
